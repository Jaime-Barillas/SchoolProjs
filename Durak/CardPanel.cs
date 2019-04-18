using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Durak
{
    public class CardPanel : Panel
    {
        // BUFFER is used when determining whether the cards should be squished
        // together to make them fit in the panel. (helps avoid a little math
        // using the padding property)
        public int CardPadding { get; set; } = 5;
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }
        public Color LabelColour
        {
            get { return label.ForeColor; }
            set { label.ForeColor = value; }
        }
        public Font LabelFont
        {
            get { return label.Font; }
            set { label.Font = value; }
        }

        private Label label;
        private int totalChildrenWidth = 0;

        public CardPanel()
        {
            label = new Label();
            label.AutoSize = true;
            label.SizeChanged += (s, e) =>
            {
                // Position the label.
                label.Left = (int)(this.Width * 0.15);
                label.Top = (this.Height / 2) - (label.Height / 2);
            };

            this.Controls.Add(label);
            this.ControlAdded += CardPanel_ControlAdded;
            this.ControlRemoved += CardPanel_ControlRemoved;
        }

        /// <summary>
        /// Reset the card panel to its initial state. Use this instead of Clear().
        /// </summary>
        public void Reset()
        {
            this.Controls.Clear();
            this.Controls.Add(label);
        }

        /// <summary>
        /// Reposition the cards within the panel.
        /// </summary>
        private void RepositionCards()
        {
            Control currentChild;
            int xPos = 0;

            // Reposition each child.
            // TODO: Only reposition **all** children when we need to squish
            // the cards together.
            for (int child = 0; child < this.Controls.Count; child++)
            {
                // Skip the label since it is not a card.
                if (this.Controls[child] == label) continue;

                currentChild = this.Controls[child];
                currentChild.Left = xPos;

                // If the total width of the children exceeds the width of this control
                // minus a buffer then we need to start squishing the cards in.
                if (totalChildrenWidth + (CardPadding * this.Controls.Count) > this.Width)
                {
                    // Try to position each card such that it is visible onscreen.
                    xPos += (this.Width - currentChild.Width) / this.Controls.Count;
                }
                else
                {
                    // Otherwise xPos can go up by the width of a card + padding.
                    xPos += currentChild.Width + CardPadding;
                }
            }
        }

        /// <summary>
        /// Subtracts the removed control from the total children width.
        /// </summary>
        private void CardPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            // Ignore the label.
            if (e.Control != label)
            {
                totalChildrenWidth -= e.Control.Width;
                RepositionCards();
            }
        }

        /// <summary>
        /// Accumulates the total width of it's children, will position the
        /// added controls.
        /// </summary>
        private void CardPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            // Ignore the label.
            if (e.Control != label)
            {
                totalChildrenWidth += e.Control.Width;
                RepositionCards();
                label.SendToBack();
            }
        }
    }
}
