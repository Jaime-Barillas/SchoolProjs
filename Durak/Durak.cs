using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DurakGame;

namespace Durak
{
    public partial class Durak : Form
    {
        Game game;
        HumanPlayer player;
        Player aiPlayer;
        List<PictureBox> playerHand;
        List<PictureBox> aiHand;
        int selectedCard = -1;
        bool isAttacking;

        Statistics stats;

        public Durak()
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
            game = new Game();
            player = (HumanPlayer)game.Players[0];
            aiPlayer = game.Players[1];

            // GUI setup
            picTalon.Image = Assets.CardBackside;
            picTrumpCard.Image = Assets.Cards[game.Talon.Trump.Suit][game.Talon.Trump.Rank];

            playerHand = new List<PictureBox>();
            foreach (Card card in player.Hand)
            {
                PictureBox cardImage = new PictureBox();
                cardImage.Image = Assets.Cards[card.Suit][card.Rank];
                cardImage.Size = new Size(150, 225);
                cardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                cardImage.Scale(new SizeF(0.5f, 0.5f));

                cardImage.Click += CardImage_Click;

                playerHand.Add(cardImage);
                flpPlayerHand.Controls.Add(cardImage);
            }

            aiHand = new List<PictureBox>();
            foreach (Card card in aiPlayer.Hand)
            {
                PictureBox cardImage = new PictureBox();
                cardImage.Image = Assets.CardBackside;
                cardImage.Size = new Size(150, 225);
                cardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                cardImage.Scale(new SizeF(0.5f, 0.5f));

                aiHand.Add(cardImage);
                flpAIHand.Controls.Add(cardImage);
            }

            // Subscribe to events.
            player.Attack += AddPlayedCard;
            player.Defend += AddPlayedCard;
            aiPlayer.Attack += AddPlayedCard;
            aiPlayer.Defend += AddPlayedCard;

            // TODO: sub to events in new bout.
            game.CurrentBout.Report += PrepForTurn;
            game.CurrentBout.End += (bout, e) => flpActiveCards.Controls.Clear();
            game.End += Game_End;

            player.AcceptInput += (p, e) => { btnGameConcede.Enabled = true; e.Action = selectedCard; };
            btnGameConcede.Enabled = true;

            game.Continue();
        }

        /// <summary>
        /// Add the played card to the center field.
        /// </summary>
        private void AddPlayedCard(object sender, GameActionEventArgs e)
        {
            if (sender is AIPlayer && e.Action != Player.NO_ACTION)
            {
                // Change the card image from the backside to the frontside.
                Card card = aiPlayer.Hand[e.Action];
                aiHand[e.Action].Image = Assets.Cards[card.Suit][card.Rank];

                // Add the card to the center field and remove from the list of images.
                flpActiveCards.Controls.Add(aiHand[e.Action]);
                aiHand.RemoveAt(e.Action);
            }
            else if (e.Action != Player.NO_ACTION)
            {
                flpActiveCards.Controls.Add(playerHand[e.Action]);
                playerHand.RemoveAt(e.Action);
            }
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
                // The button should be enabled always when defending.
                btnGameConcede.Enabled = true;

                // Enable clicking only valid cards.
                for (int cardIndex = 0; cardIndex < player.Hand.Count; cardIndex++)
                {
                    playerHand[cardIndex].Enabled = bout.IsValidDefense(player.Hand[cardIndex]);
                }
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
                flpActiveCards.Controls.Add(playerHand[selectedCard]);
            }

            // Continue turns within the game until it is the human player's turn.
            do
            {
                game.Continue();
            } while (game.CurrentBout.ActingPlayer != player) ;
        }

        /// <summary>
        /// Return to the main menu after displaying a message.
        /// </summary>
        private void Game_End(object sender, GameLogEventArgs e)
        {
            tlpGameScreen.Hide();
            tlpMainMenu.Show();
        }
    }
}
