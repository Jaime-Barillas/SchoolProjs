using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a deck of playing cards.
    /// </summary>
    public class Deck
    {
        // Properties

        private List<Card> cards = new List<Card>();

        /// <summary>
        /// Number of cards remaining in the deck.
        /// </summary>
        public int Size
        {
            get
            {
                return cards.Count;
            }
        }

        // Constructors

        /// <summary>
        /// Parameterized constructor. Creates a nonstandard deck of cards based on rank.
        /// </summary>
        /// <param name="ranks">An array containing the ranks that are to appear in the deck.</param>
        public Deck(Rank[] ranks)
        {
            // For each rank given...
            foreach (Rank rank in ranks)
            {
                // For each suit (from all possible suits)...
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    cards.Add(new Card(rank, suit));
                }
            }

            Shuffle();
        }

        /// <summary>
        /// Default constructor. Creates a 36-card deck for standard Durak.
        /// </summary>
        public Deck() : this(new Rank[] 
        {
            Rank.Ace,
            Rank.Six,
            Rank.Seven,
            Rank.Eight,
            Rank.Nine,
            Rank.Ten,
            Rank.Jack,
            Rank.Queen,
            Rank.King
        }) { /* Empty method body */ }

        // Methods

        /// <summary>
        /// Randomly shuffles the cards in the deck.
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();

            // Fischer-Yates shuffle
            for (int pos = cards.Count - 1; pos >= 1; pos--)
            {
                int newPos = rng.Next(pos + 1);
                Card temp = cards[newPos];
                cards[newPos] = cards[pos];
                cards[pos] = temp;
            }
        }

        /// <summary>
        /// Draws a card, removing it from the deck.
        /// </summary>
        /// <returns>The drawn card.</returns>
        public Card Draw()
        {
            if (this.Size > 0)
            {
                Card drawn = cards.ElementAt(cards.Count - 1);
                cards.Remove(drawn);
                return drawn;
            }
            else
            {
                throw new InvalidOperationException("Deck is empty.");
            }
        }
    }
}
