using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a player engaged in the game.
    /// </summary>
    public abstract class Player
    {
        // Properties
        /// <summary>
        /// Whether the player is still in the game. A player ceases to be active when they shed all of their cards.
        /// </summary>
        public bool IsActive { get; private set; } = true;

        /// <summary>
        /// The name identifying the player in the game log.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Cards currently held by the player.
        /// </summary>
        public List<Card> Hand { get; }

        // Events
        /// <summary>
        /// Fired when the player sheds the last card from their hand.
        /// </summary>
        public event EventHandler<GameLogEventArgs> HandEmpty;
        public event EventHandler<GameLogEventArgs> Attack;
        public event EventHandler<GameLogEventArgs> Defend;

        // Constructors
        /// <summary>
        /// Default constructor. Instantiates a new Player object.
        /// </summary
        /// <param name="name">The name identifying the player.</param>
        public Player(string name = "Player")
        {
            Name = name;
        }

        // Methods
        /// <summary>
        /// Adds a card to the player's hand.
        /// </summary>
        /// <param name="newCard">The card to add.</param>
        public void TakeCard(Card newCard)
        {
            Hand.Add(newCard);
        }

        /// <summary>
        /// Adds multiple cards to the player's hand.
        /// </summary>
        /// <param name="newCards">Array of cards to add.</param>
        public void TakeCards(Card[] newCards)
        {
            Hand.AddRange(newCards);
        }

        /// <summary>
        /// Adds multiple cards to the player's hand.
        /// </summary>
        /// <param name="newCards">List of cards to add.</param>
        public void TakeCards(List<Card> newCards)
        {
            TakeCards(newCards.ToArray());
        }

        /// <summary>
        /// Plays a card as an attack from the player's hand.
        /// </summary>
        /// <param name="index">The index of the card being played.</param>
        /// <returns>The card being played.</returns>
        public Card AttackWith(int index)
        {
            if (index >= 0 && index < Hand.Count)
            {
                Attack?.Invoke(this, new GameLogEventArgs(string.Format("{0} attacks with the {1}.", Name, Hand[index])));
                
                // Perform normal "play a card" logic and return the result.
                return PlayCard(index);
            }
            else
            {
                throw new ArgumentOutOfRangeException("{0} is not a valid index for any card in the player's hand.", index.ToString());
            }
        }

        /// <summary>
        /// Plays a card as a defense against an attack, from the player's hand.
        /// </summary>
        /// <param name="index">Index of the card being played.</param>
        /// <returns>The card being played.</returns>
        public Card DefendWith(int index)
        {
            if (index >= 0 && index < Hand.Count)
            {
                Defend?.Invoke(this, new GameLogEventArgs(string.Format("{0} defends with the {1}.", Name, Hand[index])));

                // Perform normal "play a card" logic and return the result.
                return PlayCard(index);
            }
            else
            {
                throw new ArgumentOutOfRangeException("{0} is not a valid index for any card in the player's hand.", index.ToString());
            }
        }

        /// <summary>
        /// Plays a card from the player's hand, removing it from their hand.
        /// </summary>
        /// <param name="index">Index of the card being played.</param>
        /// <returns>The card being played.</returns>
        private Card PlayCard(int index)
        {
            if (index >= 0 && index < Hand.Count)
            {
                Card playedCard = Hand[index];
                Hand.Remove(playedCard);

                // If the player's hand is now empty, they get to leave the game.
                if (Hand.Count == 0)
                {
                    IsActive = false;
                    HandEmpty?.Invoke(this, new GameLogEventArgs(string.Format("{0} shed the last card from their hand!", Name)));
                }

                return playedCard;
            }
            else
            {
                throw new ArgumentOutOfRangeException("{0} is not a valid index for the player's hand.", index.ToString());
            }
        }

        
    }
}
