using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a stack of cards in play during a game, including the trump revealed at the start of the game.
    /// </summary>
    public class Talon
    {
        // Properties
        private Deck deck;
        /// <summary>
        /// Trump card revealed at the start of the game.
        /// </summary>
        public Card Trump { get; }

        private bool isTrumpDrawn = false;

        /// <summary>
        /// The suit of the trump revealed at the start of the game.
        /// </summary>
        public Suit TrumpSuit
        {
            get
            {
                return Trump.Suit;
            }
        }

        /// <summary>
        /// Number of cards remaining in the talon.
        /// </summary>
        public int Size
        {
            get
            {
                // Returns size of the deck, +1 if the trump hasn't been drawn yet.
                return deck.Size + (isTrumpDrawn ? 0 : 1);
            }
        }

        // Constructors
        /// <summary>
        /// Parameterized constructor. Creates a talon out of a deck of cards.
        /// </summary>
        /// <param name="deck">The deck to use as a talon.</param>
        public Talon(Deck deck)
        {
            this.deck = deck;
            this.Trump = this.deck.Draw();
        }

        /// <summary>
        /// Alternate parameterized constructor. Creates a talon based on a deck preset.
        /// </summary>
        /// <param name="preset">The deck preset to be created and used as a talon.</param>
        public Talon(Deck.Preset preset) : this(new Deck(preset)) { }

        /// <summary>
        /// Default constructor. Uses a default (36-card) deck to create the talon.
        /// </summary>
        public Talon() : this(new Deck()) { }

        // Methods
        /// <summary>
        /// Draws a card, removing it from the talon. The trump will be drawn when there are no cards remaining in the deck.
        /// </summary>
        /// <returns>The drawn card.</returns>
        public Card Draw()
        {
            if (deck.Size > 0)
            {
                return deck.Draw();
            }
            else
            {
                if (!isTrumpDrawn)
                {
                    isTrumpDrawn = true;
                    return Trump;
                }
                else
                {
                    throw new InvalidOperationException("Talon is empty.");
                }
            }
        }
    }
}
