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
    public partial class frmResetStats : Form
    {
        public frmResetStats()
        {
            InitializeComponent();

            btnOk.MouseEnter     += MouseEnterTextColour;
            btnCancel.MouseEnter += MouseEnterTextColour;
            btnOk.MouseLeave     += MouseLeaveTextColour;
            btnCancel.MouseLeave += MouseLeaveTextColour;
        }

        /// <summary>
        /// [Get/Set] The default player name displayed. This will become the new
        /// name for the player.
        /// </summary>
        public string PlayerName
        {
            get { return txtPlayerName.Text; }
            set { txtPlayerName.Text = value; }
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
        /// Close the form to cancel the stat reset.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Close the form after setting the dialog result to ok so that the
        /// stats get reset.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
