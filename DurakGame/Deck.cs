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
        // Embedded types

        /// <summary>
        /// Identifiers for deck presets to be passed to the constructor, in place of a Rank array.
        /// </summary>
        public enum Preset
        {
            /// <summary>
            /// 36-card deck used in ordinary Durak: A, 6-10, J, Q, K
            /// </summary>
            DurakStandard,
            /// <summary>
            /// Full 52-card deck
            /// </summary>
            FullDeck,
            /// <summary>
            /// 20-card deck for short games: A, 10, J, Q, K
            /// </summary>
            SmallDeck,
        }
        
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
        /// Parameterized constructor. Creates a deck of cards based on rank.
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
        /// Alternate parameterized constructor. Creates a deck of cards based on one of several built-in presets.
        /// </summary>
        /// <param name="preset">The deck preset to be created.</param>
        public Deck(Preset preset) : this(PresetToRankArray(preset)) { }

        /// <summary>
        /// Default constructor. Creates a 36-card deck for standard Durak.
        /// </summary>
        public Deck() : this(Preset.DurakStandard) { }

        // Methods

        /// <summary>
        /// Conversion method for turning deck presets into rank arrays. Used by parameterized constructor.
        /// </summary>
        /// <param name="preset">The deck preset to be used.</param>
        /// <returns>An equivalent array of Ranks.</returns>
        private static Rank[] PresetToRankArray(Preset preset)
        {
            switch (preset)
            {
                case Preset.DurakStandard:
                    return new Rank[]
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
                    };
                case Preset.FullDeck:
                    return new Rank[]
                    {
                        Rank.Ace,
                        Rank.Two,
                        Rank.Three,
                        Rank.Four,
                        Rank.Five,
                        Rank.Six,
                        Rank.Seven,
                        Rank.Eight,
                        Rank.Nine,
                        Rank.Ten,
                        Rank.Jack,
                        Rank.Queen,
                        Rank.King
                    };
                case Preset.SmallDeck:
                    return new Rank[]
                    {
                        Rank.Ace,
                        Rank.Ten,
                        Rank.Jack,
                        Rank.Queen,
                        Rank.King
                    };
                default:
                    throw new ArgumentException("Invalid deck preset");
            }
        }

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
