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
        #region Properties
        private Deck deck;

        /// <summary>
        /// Trump card revealed at the start of the game.
        /// </summary>
        public Card Trump { get; }

        /// <summary>
        /// Number of cards remaining in the talon.
        /// </summary>
        public int Size
        {
            get
            {
                // Returns size of the deck, +1 if the trump hasn't been drawn yet.
                return deck.Size + (IsEmpty ? 0 : 1);
            }
        }

        /// <summary>
        /// Flag representing whether there are any cards remaining in the talon, including the trump card.
        /// </summary>
        public bool IsEmpty { get; private set; }

        #endregion

        #region Events
        /// <summary>
        /// Fires when the last card is drawn from the talon.
        /// </summary>
        public event EventHandler<GameLogEventArgs> TalonEmpty;

        #endregion

        #region Constructors
        /// <summary>
        /// Parameterized constructor. Creates a talon out of a deck of cards.
        /// </summary>
        /// <param name="deck">The deck to use as a talon.</param>
        public Talon(Deck deck)
        {
            if (deck.Size < 1)
            {
                throw new ArgumentException("A deck must contain at least one card to be used as a talon.");
            }
            this.deck = deck;
            this.Trump = this.deck.Draw();
            this.IsEmpty = false;
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

        #endregion

        #region Methods
        /// <summary>
        /// Draws a card, removing it from the talon. The trump will be drawn when there are no cards remaining in the deck.
        /// </summary>
        /// <returns>The drawn card.</returns>
        public Card Draw()
        {
            // If there are any cards in the deck, draw one.
            if (deck.Size > 0)
            {
                return deck.Draw();
            }
            // If the deck is empty...
            else
            {
                // ...but the trump card remains, "draw" that.
                if (!IsEmpty)
                {
                    IsEmpty = true;
                    // Fire an event signaling that the final card was drawn.
                    TalonEmpty?.Invoke(this, new GameLogEventArgs("The last card has been drawn from the talon. The talon is now empty."));
                    return Trump;
                }
                // If the trump card is gone too, this deck shouldn't be drawn from.
                else
                {
                    throw new InvalidOperationException("Cannot draw from empty talon.");
                }
            }
        }

        #endregion
    }
}
