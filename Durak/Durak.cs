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
    }
}
