using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DurakGame;

namespace Durak
{
    public partial class frmDurak : Form
    {
        private Game game;
        private HumanPlayer player;
        private Player aiPlayer;
        private List<PictureBox> playerHand;
        private List<PictureBox> aiHand;
        private int selectedCard = Player.NO_ACTION;
        private bool isAttacking;

        // Statistics and logs
        private Statistics stats;
        private const string LOG_FILE = "./assets/log.txt";

        public frmDurak()
        {
            InitializeComponent();

            btnPlay.MouseEnter          += MouseEnterTextColour;
            btnPlay.MouseLeave          += MouseLeaveTextColour;
            btnOptions.MouseEnter       += MouseEnterTextColour;
            btnOptions.MouseLeave       += MouseLeaveTextColour;
            btnStats.MouseEnter         += MouseEnterTextColour;
            btnStats.MouseLeave         += MouseLeaveTextColour;
            btnExit.MouseEnter          += MouseEnterTextColour;
            btnExit.MouseLeave          += MouseLeaveTextColour;
            btnMainMenu.MouseEnter      += MouseEnterTextColour;
            btnMainMenu.MouseLeave      += MouseLeaveTextColour;
            btnHelp.MouseEnter          += MouseEnterTextColour;
            btnHelp.MouseLeave          += MouseLeaveTextColour;
            btnMainMenuStats.MouseEnter += MouseEnterTextColour;
            btnMainMenuStats.MouseLeave += MouseLeaveTextColour;
            btnStatsReset.MouseEnter    += MouseEnterTextColour;
            btnStatsReset.MouseLeave    += MouseLeaveTextColour;
            btnGameConcede.MouseEnter   += MouseEnterTextColour;
            btnGameConcede.MouseLeave   += MouseLeaveTextColour;
            btnGameConcede.Click        += CardImage_Click;
        }

        /// <summary>
        /// Load all the necessary resources for the game.
        /// </summary>
        private void Durak_Load(object sender, EventArgs e)
        {
            // Load card skins
            Assets.Load();

            foreach (string cardSkin in Assets.AvailableSkins())
            {
                cmbCardSkins.Items.Add(cardSkin);
            }

            // Display a preview of the cards.
            picCardSkinPreview.Image = Assets.Cards[Suit.Clubs][Rank.Ace];
            cmbCardSkins.SelectedIndex = cmbCardSkins.Items.IndexOf("Big Numbers"); // Manualy set combo box text.

            cmbCardSkins.SelectedIndexChanged += CmbCardSkins_SelectedIndexChanged;


            // Load stats
            stats = Statistics.Read();
            UpdateStats();

            // Setup logger.
            Trace.Listeners.Add(new TextWriterTraceListener(LOG_FILE));
            // Buffer from previous logs.
            Trace.WriteLine("\n========================================");

            btnStatsReset.Click += btnStatsReset_Click;
        }

        /// <summary>
        /// Change the text colour when the mouse enters the button.
        /// </summary>
        private void MouseEnterTextColour(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.Crimson;
        }

        /// <summary>
        /// Change the text colour when the mouse enters the button.
        /// </summary>
        private void MouseLeaveTextColour(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.Black;
        }

        /// <summary>
        /// Start the game.
        /// </summary>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            SetupGameScreen();
            tlpMainMenu.Hide();
            tlpGameScreen.Show();
        }

        /// <summary>
        /// Display the options menu.
        /// </summary>
        private void btnOptions_Click(object sender, EventArgs e)
        {
            tlpMainMenu.Hide();
            tlpOptionMenu.Show();
        }

        /// <summary>
        /// Display game statistics.
        /// </summary>
        private void btnStats_Click(object sender, EventArgs e)
        {
            tlpMainMenu.Hide();
            tlpStatsMenu.Show();
        }

        /// <summary>
        /// Exit the program on btn click.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Return to the main menu from the options menu.
        /// </summary>
        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            tlpOptionMenu.Hide();
            tlpMainMenu.Show();
        }

        /// <summary>
        /// Load the help files in an external web browser.
        /// </summary>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            string documentationFile = System.IO.Path.GetFullPath("../../../docs/user-documentation.md.html");
            System.Diagnostics.Process.Start(documentationFile);
        }

        /// <summary>
        /// Return to the main menu from the stats screen.
        /// </summary>
        private void btnMainMenuStats_Click(object sender, EventArgs e)
        {
            tlpStatsMenu.Hide();
            tlpMainMenu.Show();
        }

        /// <summary>
        /// Load the card skins and set the preview whenever the user selects
        /// a different skin.
        /// </summary>
        private void CmbCardSkins_SelectedIndexChanged(object sender, EventArgs e)
        {
            Assets.LoadCardSkin((string)cmbCardSkins.SelectedItem);
            picCardSkinPreview.Image = Assets.Cards[Suit.Clubs][Rank.Ace];
        }

        /// <summary>
        /// Update the stats screen with the current statistics.
        /// </summary>
        private void UpdateStats()
        {
            lblPlayerName.Text = stats.PlayerName;
            lblNumberOfGames.Text = stats.NumberOfGames.ToString();
            lblNumberOfWins.Text = stats.Wins.ToString();
            lblNumberOfLosses.Text = stats.Losses.ToString();

            stats.Save();
        }

        /// <summary>
        /// Reset the stats for the player. Also provides a way to change the player name.
        /// </summary>
        private void btnStatsReset_Click(object sender, EventArgs e)
        {
            frmResetStats resetForm = new frmResetStats();

            resetForm.ShowDialog(this);
            if (resetForm.DialogResult == DialogResult.OK)
            {
                stats.PlayerName = resetForm.PlayerName;
                stats.NumberOfGames = 0;
                stats.Wins = 0;
                stats.Losses = 0;

                UpdateStats();
            }
        }

        /// <summary>
        /// Sets up the game screen and the game.
        /// </summary>
        private void SetupGameScreen()
        {
            // Game creation
            Deck deck;
            if (rdbSmallDeck.Checked)
            {
                deck = new Deck(Deck.Preset.SmallDeck);
            }
            else if (rdbStandardDeck.Checked)
            {
                deck = new Deck(Deck.Preset.DurakStandard);
            }
            else
            {
                deck = new Deck(Deck.Preset.FullDeck);
            }
            game = new Game(deck);
            player = (HumanPlayer)game.Players[0];
            aiPlayer = game.Players[1];

            // Log game start.
            Trace.Write($"Game Started for player {stats.PlayerName} on {DateTime.Now} with a ");
            Trace.WriteLineIf(rdbSmallDeck.Checked, "Small Deck");
            Trace.WriteLineIf(rdbStandardDeck.Checked, "Standard Deck");
            Trace.WriteLineIf(rdbLargeDeck.Checked, "Large Deck");
            Trace.WriteLine($"Trump Card: {game.Talon.Trump.Rank} of {game.Talon.Trump.Suit}");

            // GUI setup
            picTalon.Image = Assets.CardBackside;
            picTrumpCard.Image = Assets.Cards[game.Talon.Trump.Suit][game.Talon.Trump.Rank];

            // Fill player hands.
            Trace.WriteLine("Initial Hands\n-------------");
            playerHand = new List<PictureBox>();
            FillPlayerHand(player);

            aiHand = new List<PictureBox>();
            FillPlayerHand(aiPlayer);
            Trace.WriteLine("-------------");

            // Subscribe to events.
            player.Attack += AddPlayedCard;
            player.Defend += AddPlayedCard;
            player.PickUp += (s, e) => FillPlayerHand(player);
            aiPlayer.Attack += AddPlayedCard;
            aiPlayer.Defend += AddPlayedCard;
            aiPlayer.PickUp += (s, e) => FillPlayerHand(aiPlayer);

            game.CurrentBout.Report += PrepForTurn;
            game.NewBout += Game_NewBout;
            game.End += Game_End;

            player.AcceptInput += (p, e) =>
            {
                // After the first card is chosen we can enable the concede button.
                // It should be disabled on the first attack.
                btnGameConcede.Enabled = true;
                e.Action = selectedCard;
            };

            game.Continue();
        }

        /// <summary>
        /// Fill the player's hand with cards. Refills the layout panel and the picbox list.
        /// </summary>
        /// <param name="player">The player whose hand was refilled with cards.</param>
        private void FillPlayerHand(Player playerToFill)
        {
            List<PictureBox> hand;
            CardPanel cardDisplay;

            // Grab the specified player's corresponding
            // list of card images and display container...
            if (playerToFill == player)
            {
                hand = playerHand;
                cardDisplay = cpPlayerHand;

                Trace.Write(stats.PlayerName + "'s new hand: ");
            }
            else
            {
                hand = aiHand;
                cardDisplay = cpAIHand;

                Trace.Write("AI Player's new hand: ");
            }

            // Clear and repopulate them.
            hand.Clear();
            cardDisplay.Reset();
            foreach (Card card in playerToFill.Hand)
            {
                PictureBox cardImage = new PictureBox();
                cardImage.Size = new Size(150, 225);
                cardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                if (playerToFill == player)
                {
                    cardImage.Image = Assets.Cards[card.Suit][card.Rank];
                }
                else
                {
                    cardImage.Image = Assets.CardBackside;
                }

                cardImage.Scale(new SizeF(0.5f, 0.5f)); // Make the card a bit smaller so it fits nicely.
                cardImage.Click += CardImage_Click;  // Add click functionality.

                hand.Add(cardImage);
                cardDisplay.Controls.Add(cardImage);

                Trace.Write(card.Rank + " of " + card.Suit);
                Trace.WriteIf(card != playerToFill.Hand.Last(), ", ");
            }

            // Move to next line.
            Trace.WriteLine("");
        }

        /// <summary>
        /// Determine whether the user is attacking or defending, disable cards they can't use.
        /// </summary>
        private void PrepForTurn(object sender, GameLogEventArgs e)
        {
            Bout bout = (Bout)sender;
            isAttacking = bout.Attacker == player;

            // When attacking, the concede button is disabled on the first turn, then enabled on
            // subsequent turns.
            if (isAttacking)
            {
                cpActiveCards.LabelText = "Attack!";

                if (bout.AttackCardsPlayed.Count == 0)
                {
                    btnGameConcede.Enabled = false;
                }
                else
                {
                    btnGameConcede.Enabled = true;
                }

                // Enable clicking only valid cards.
                for (int cardIndex = 0; cardIndex < player.Hand.Count; cardIndex++)
                {
                    playerHand[cardIndex].Enabled = bout.IsValidAttack(player.Hand[cardIndex]);
                }
            }
            else
            {
                cpActiveCards.LabelText = "Defend!";

                // The button should always be enabled when defending.
                btnGameConcede.Enabled = true;

                // Enable clicking only valid cards.
                // We need to be sure that the next player to go is the user
                // otherwise we could end up in a situation where the we try
                // to find out if a card can defend when there are no attacking
                // cards.
                if (bout.ActingPlayer == player)
                {
                    for (int cardIndex = 0; cardIndex < player.Hand.Count; cardIndex++)
                    {
                        playerHand[cardIndex].Enabled = bout.IsValidDefense(player.Hand[cardIndex]);
                    }
                }
            }
        }

        /// <summary>
        /// Add the played card to the center field.
        /// </summary>
        private void AddPlayedCard(object sender, GameActionEventArgs e)
        {
            if (sender == aiPlayer && e.Action != Player.NO_ACTION)
            {
                // Change the card image from the backside to the frontside.
                Card card = aiPlayer.Hand[e.Action];
                aiHand[e.Action].Image = Assets.Cards[card.Suit][card.Rank];

                // Add the card to the center field and remove from the list of images.
                cpActiveCards.Controls.Add(aiHand[e.Action]);
                aiHand.RemoveAt(e.Action);

                Trace.WriteIf(isAttacking, "AI Player defends with: ");
                Trace.WriteIf(!isAttacking, "AI Player attacks with: ");
                Trace.WriteLine(card.Rank + " of " + card.Suit);
            }
            else if (e.Action != Player.NO_ACTION)
            {
                Card card = player.Hand[e.Action];

                cpActiveCards.Controls.Add(playerHand[e.Action]);
                playerHand.RemoveAt(e.Action);

                Trace.WriteIf(isAttacking, stats.PlayerName + " attacks with: ");
                Trace.WriteIf(!isAttacking, stats.PlayerName + " defends with: ");
                Trace.WriteLine(card.Rank + " of " + card.Suit);
            }
        }

        /// <summary>
        /// Advance one turn of the game per click.
        /// </summary>
        private void CardImage_Click(object sender, EventArgs e)
        {
            selectedCard = playerHand.IndexOf(sender as PictureBox);

            if (selectedCard >= 0 && selectedCard < playerHand.Count)
            {
                cpActiveCards.Controls.Add(playerHand[selectedCard]);
            }

            // Continue turns within the game until it is the human player's turn.
            do
            {
                game.Continue();
            } while (game.CurrentBout.ActingPlayer != player && !game.IsOver) ;
        }

        /// <summary>
        /// Sub to the correct events whenever a new bout happens.
        /// </summary>
        private void Game_NewBout(object sender, GameLogEventArgs e)
        {
            // A bug in the game lib makes it so that the first bout does not
            // fire a "bout end" event, so we clear the playing field on
            // "new bout" instead of "bout end".
            cpActiveCards.Reset();

            selectedCard = Player.NO_ACTION;

            // Sub to the new bout's events.
            game.CurrentBout.Report += PrepForTurn;

            Trace.WriteLine("New Bout\n--------");
        }

        /// <summary>
        /// Return to the main menu after displaying a message.
        /// </summary>
        private void Game_End(object sender, GameLogEventArgs e)
        {
            // Clear everything.
            cpActiveCards.Reset();
            cpPlayerHand.Reset();
            cpAIHand.Reset();
            playerHand.Clear();
            aiHand.Clear();

            // Update stats.
            stats.NumberOfGames++;
            if (game.Fool == player)
            {
                stats.Losses++;

                MessageBox.Show("You're the fool!");
                Trace.WriteLine($"Game End! {stats.PlayerName} loses");
            }
            else
            {
                stats.Wins++;

                MessageBox.Show("You win!");
                Trace.WriteLine($"Game End! {stats.PlayerName} wins");
            }
            UpdateStats();

            // Print log.
            Trace.Flush();

            // Back to the main menu!
            tlpGameScreen.Hide();
            tlpMainMenu.Show();
        }
    }
}
