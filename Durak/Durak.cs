using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Durak
{
    public partial class Durak : Form
    {
        public Durak()
        {
            InitializeComponent();

            btnPlay.MouseEnter     += MouseEnterTextColour;
            btnPlay.MouseLeave     += MouseLeaveTextColour;
            btnOptions.MouseEnter  += MouseEnterTextColour;
            btnOptions.MouseLeave  += MouseLeaveTextColour;
            btnStats.MouseEnter    += MouseEnterTextColour;
            btnStats.MouseLeave    += MouseLeaveTextColour;
            btnExit.MouseEnter     += MouseEnterTextColour;
            btnExit.MouseLeave     += MouseLeaveTextColour;
            btnMainMenu.MouseEnter += MouseEnterTextColour;
            btnMainMenu.MouseLeave += MouseLeaveTextColour;
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
    }
}
