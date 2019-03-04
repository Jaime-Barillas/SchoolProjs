using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a standard playing card
    /// </summary>
    public class Card
    {
        // Properties
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }

        // Constructors
        public Card(Rank rank, Suit suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        // Methods

        /// <summary>
        /// Determines whether a card may be used to defend against an attack.
        /// </summary>
        /// <param name="attack">The attack card being defended against.</param>
        /// <param name="trumpSuit">The current game's trump suit.</param>
        /// <returns>True if this card trumps or outranks the attack; false otherwise.</returns>
        public bool CanDefendAgainst(Card attack, Suit trumpSuit)
        {
            bool canDefend; // Return value

            if (this.Suit == trumpSuit)
            {
                // Trump always defends against non-trump
                if (attack.Suit != trumpSuit)
                {
                    canDefend = true;
                }
                // Both cards are trump
                else
                {
                    if (attack.Rank == Rank.Ace)
                    {
                        canDefend = false;
                    }
                    else if (this.Rank == Rank.Ace)
                    {
                        canDefend = true;
                    }
                    else
                    {
                        // If this card has a higher rank, it can defend; otherwise, it can't
                        canDefend = (this.Rank.CompareTo(attack.Rank) > 0);
                    }
                }
            }
            else if (this.Suit == attack.Suit)
            {
                if (attack.Rank == Rank.Ace)
                {
                    canDefend = false;
                }
                else if (this.Rank == Rank.Ace)
                {
                    canDefend = true;
                }
                else
                {
                    // If this card has a higher rank, it can defend; otherwise, it can't
                    canDefend = (this.Rank.CompareTo(attack.Rank) > 0);
                }
            }
            // Different suit (non-trump), cannot use to defend
            else
            {
                canDefend = false;
            }

            return canDefend;
        }

        /// <summary>
        /// Retrieves card name as string.
        /// </summary>
        /// <returns>The card's name.</returns>
        public override string ToString()
        {
            return this.Rank + " of " + this.Suit;
        }
    }
}
