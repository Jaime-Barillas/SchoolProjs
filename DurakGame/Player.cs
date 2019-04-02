using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a generic player engaged in the game.
    /// </summary>
    public abstract class Player
    {
        #region Properties
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

        /// <summary>
        /// Value representing a decision of "no action". In the context of attacking/defending, this means giving up the attack/defense.
        /// </summary>
        public static readonly int NO_ACTION = -1;

        #endregion

        #region Events
        /// <summary>
        /// Fired when the player is prompted to take action.
        /// </summary>
        public event EventHandler<GameActionEventArgs> Prompt;
        /// <summary>
        /// Consumes the Prompt event.
        /// </summary>
        /// <param name="a"></param>
        /// <returns>An "action index" representing the action the player chose to take when prompted.</returns>
        protected virtual void OnPrompt(GameActionEventArgs a)
        {
            Prompt?.Invoke(this, a);
        }

        /// <summary>
        /// Fires when the player picks up one or more cards.
        /// </summary>
        public event EventHandler<GameLogEventArgs> PickUp;
        protected virtual void OnPickUp(GameLogEventArgs l)
        {
            PickUp?.Invoke(this, l);
        }

        /// <summary>
        /// Fired when the player sheds the last card from their hand.
        /// </summary>
        public event EventHandler<GameLogEventArgs> HandEmpty;
        protected virtual void OnHandEmpty(GameLogEventArgs l)
        {
            IsActive = false;
            HandEmpty?.Invoke(this, l);
        }

        /// <summary>
        /// Fired when the player plays a card as attacker.
        /// </summary>
        public event EventHandler<GameActionEventArgs> Attack;
        /// <summary>
        /// Fired when the player plays a card as attacker; used to pipe messages to the game log.
        /// </summary>
        public event EventHandler<GameLogEventArgs> AttackLog;
        protected virtual void OnAttack(GameActionEventArgs a, GameLogEventArgs l)
        {
            Attack?.Invoke(this, a);
            AttackLog?.Invoke(this, l);
        }

        /// <summary>
        /// Fired when the player plays a card as defender.
        /// </summary>
        public event EventHandler<GameActionEventArgs> Defend;
        /// <summary>
        /// Fired when the player plays a card as defender; used to pipe messages to the game log.
        /// </summary>
        public event EventHandler<GameLogEventArgs> DefendLog;
        protected virtual void OnDefend(GameActionEventArgs a, GameLogEventArgs l)
        {
            Defend?.Invoke(this, a);
            DefendLog?.Invoke(this, l);
        }

        /// <summary>
        /// Fired when the player chooses not to play a card.
        /// </summary>
        public event EventHandler<GameLogEventArgs> Concede;
        protected virtual void OnConcede(GameLogEventArgs l)
        {
            Concede?.Invoke(this, l);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor. Instantiates a new Player object.
        /// </summary
        /// <param name="name">The name identifying the player.</param>
        public Player(string name = "Player")
        {
            Name = name;
            Hand = new List<Card>();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Adds a card to the player's hand.
        /// </summary>
        /// <param name="newCard">The card to add.</param>
        public void TakeCard(Card newCard)
        {
            Hand.Add(newCard);
            OnPickUp(new GameLogEventArgs(string.Format("{0} adds the {1} to their hand.", Name, newCard.ToString())));
        }

        /// <summary>
        /// Adds multiple cards to the player's hand.
        /// </summary>
        /// <param name="newCards">Array of cards to add.</param>
        public void TakeCards(Card[] newCards)
        {
            if (newCards.Length > 0)
            {
                Hand.AddRange(newCards);
                OnPickUp(new GameLogEventArgs(string.Format("{0} picks up {1} card(s).", Name, newCards.Length)));
            }
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
        /// Prompt the player to take an action.
        /// </summary>
        public abstract void PromptAction();

        /// <summary>
        /// Plays a card as an attack from the player's hand.
        /// </summary>
        /// <param name="index">The index of the card being played.</param>
        /// <returns>The card being played.</returns>
        public Card AttackWith(int index)
        {
            if (index >= 0 && index < Hand.Count)
            {
                // Call event handlers, if any.
                OnAttack(new GameActionEventArgs(index), new GameLogEventArgs(string.Format("{0} attacks with the {1}.", Name, Hand[index])));
                
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
                // Call event handlers, if any.
                OnDefend(new GameActionEventArgs(index), new GameLogEventArgs(string.Format("{0} defends with the {1}.", Name, Hand[index])));
                
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
                    OnHandEmpty(new GameLogEventArgs(string.Format("{0} shed the last card from their hand!", Name)));
                }

                return playedCard;
            }
            else
            {
                throw new ArgumentOutOfRangeException("{0} is not a valid index for the player's hand.", index.ToString());
            }
        }

        /// <summary>
        /// Causes the player to cease attacking, either by choice or due to having no valid attacks.
        /// </summary>
        public void GiveUpAttack()
        {
            OnConcede(new GameLogEventArgs(string.Format("{0} gave up the attack.", Name)));
        }
        
        /// <summary>
        /// Causes the player to cease defending, either by choice or due to having no valid defense.
        /// </summary>
        public void GiveUpDefense()
        {
            OnConcede(new GameLogEventArgs(string.Format("{0} gave up the defense.", Name)));
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
