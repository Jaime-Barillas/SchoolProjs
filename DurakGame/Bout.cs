using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a currently ongoing bout in the game.
    /// </summary>
    public class Bout
    {
        #region Properties
        /// <summary>
        /// The game in which the bout is taking place.
        /// </summary>
        public Game Game { get; }

        /// <summary>
        /// The player currently attacking.
        /// </summary>
        public Player Attacker { get; private set; }

        /// <summary>
        /// The player currently being attacked.
        /// </summary>
        public Player Defender { get; private set; }

        /// <summary>
        /// The player who has won the game. Initially null.
        /// </summary>
        public Player Winner { get; set; } = null;

        /// <summary>
        /// The cards "on the table" that have been played as attacks so far.
        /// </summary>
        public List<Card> AttackCardsPlayed { get; private set; }

        /// <summary>
        /// The cards "on the table" that have been used to defend against attacks.
        /// </summary>
        public List<Card> DefenseCardsPlayed { get; private set; }

        /// <summary>
        /// The number of attacks allowed during this bout.
        /// </summary>
        public int MaximumAttacks { get; private set; } = 6;

        #endregion

        #region Events
        /// <summary>
        /// Fires when the bout makes a report. Useful for the game log.
        /// </summary>
        public event EventHandler<GameLogEventArgs> Report;
        protected virtual void OnReport(GameLogEventArgs l)
        {
            Report?.Invoke(this, l);
        }
        /// <summary>
        /// Fires when the bout is concluded.
        /// </summary>
        public event EventHandler<GameLogEventArgs> End;
        protected virtual void OnEnd(GameLogEventArgs l)
        {
            End?.Invoke(this, l);
        }

        #endregion

        #region Constructors
        public Bout(Game game, Player attacker, Player defender)
        {
            this.Game = game;
            Attacker = attacker;
            Defender = defender;

            AttackCardsPlayed = new List<Card>();
            DefenseCardsPlayed = new List<Card>();

            this.End += Destroy;

            // Limit number of allowed attacks if the defender's hand is small.
            if (MaximumAttacks > Defender.Hand.Count)
            {
                MaximumAttacks = Defender.Hand.Count;
            }

            // Subscribe event handlers to attacker/defender events
            Attacker.Attack += OnNewAttack;
            Attacker.Prompt += OnPromptAttacker;
            Attacker.Concede += OnConcede;
            Attacker.HandEmpty += OnHandEmpty;

            Defender.Defend += OnNewDefend;
            Defender.Prompt += OnPromptDefender;
            Defender.Concede += OnConcede;
            Defender.HandEmpty += OnHandEmpty;
        }

        #endregion

        #region Methods
        /// <summary>
        /// "Destroys" the bout once it concludes.
        /// </summary>
        private void Destroy(object sender, EventArgs e)
        {
            // Unsubscribe event handlers
            Attacker.Attack -= OnNewAttack;
            Attacker.Prompt -= OnPromptAttacker;
            Attacker.Concede -= OnConcede;
            Attacker.HandEmpty -= OnHandEmpty;

            Defender.Defend -= OnNewDefend;
            Defender.Prompt -= OnPromptDefender;
            Defender.Concede -= OnConcede;
            Defender.HandEmpty -= OnHandEmpty;
        }

        /// <summary>
        /// Determines whether a card can currently be played as a valid attack.
        /// </summary>
        /// <param name="card">The card to be played.</param>
        /// <returns>True if the card can be played as an attack; false otherwise.</returns>
        public virtual bool IsValidAttack(Card card)
        {
            bool isValid = false;

            // Any card is valid for the first attack.
            if (AttackCardsPlayed.Count == 0)
            {
                isValid = true;
            }
            else
            {
                foreach (Card playedCard in AttackCardsPlayed)
                {
                    // A card is a valid attack if its rank matches the rank of a card that has already been played.
                    if (playedCard.Rank == card.Rank)
                    {
                        isValid = true;
                    }
                }
                foreach (Card playedCard in DefenseCardsPlayed)
                {
                    // A card is a valid attack if its rank matches the rank of a card that has already been played.
                    if (playedCard.Rank == card.Rank)
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Determines whether a card can currently be played as a valid defense.
        /// </summary>
        /// <param name="card">The card to be played.</param>
        /// <returns>True if the card is a valid defense; false otherwise.</returns>
        public virtual bool IsValidDefense(Card card)
        {
            bool isValid = false;

            if (card.CanDefendAgainst(AttackCardsPlayed.Last(), Game.TrumpSuit))
            {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Progresses the game loop within the bout.
        /// </summary>
        public virtual void Continue()
        {
            // Attacker's "turn".
            if (AttackCardsPlayed.Count <= DefenseCardsPlayed.Count)
            {
                OnReport(new GameLogEventArgs(string.Format("Attacker's turn - {0}", Attacker.Name)));
                // Rebuild AI player decision matrix based on current game state
                if (Attacker is AIPlayer)
                {
                    (Attacker as AIPlayer).InitializeMatrix(AttackCardsPlayed.Count == 0);  // The player MUST play at least one attack
                    List<int> validAttacks = new List<int>();
                    foreach(Card card in Attacker.Hand)
                    {
                        if (IsValidAttack(card))
                        {
                            validAttacks.Add(Attacker.Hand.IndexOf(card));
                        }
                    }
                    (Attacker as AIPlayer).AddDecisionsToMatrix(validAttacks.ToArray());
                }

                // Prompt attacker to select an option.
                Attacker.PromptAction();
            }
            // Defender's "turn".
            else
            {
                OnReport(new GameLogEventArgs(string.Format("Defender's turn - {0}", Defender.Name)));
                // Rebuild AI player decision matrix based on current game state
                if (Defender is AIPlayer)
                {
                    (Defender as AIPlayer).InitializeMatrix();
                    List<int> validDefends = new List<int>();
                    foreach(Card card in Defender.Hand)
                    {
                        if (card.CanDefendAgainst(AttackCardsPlayed.Last(), this.Game.TrumpSuit))
                        {
                            validDefends.Add(Defender.Hand.IndexOf(card));
                        }
                    }
                    (Defender as AIPlayer).AddDecisionsToMatrix(validDefends.ToArray());
                }

                Defender.PromptAction();
            }

            // Check if "end of bout" conditions have been met
            // If the defender has defended up to the maximum number of attacks, they win
            if (DefenseCardsPlayed.Count >= MaximumAttacks)
            {
                Winner = Defender;
            }
            // If a winner has been declared, the bout is concluded.
            if (Winner != null)
            {
                OnEnd(new GameLogEventArgs(string.Format("{0} won the bout!", Winner.Name)));
                // If the defender lost, they have to pick up all of the cards.
                if (Winner == Attacker)
                {
                    Defender.TakeCards(AttackCardsPlayed.Union(DefenseCardsPlayed).ToArray(), "the table");
                }

                // Remove all cards from the table.
                AttackCardsPlayed.Clear();
                DefenseCardsPlayed.Clear();
            }
        }
        
        /// <summary>
        /// Event handler for when the attacker is prompted to act.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ArgumentException">The attacker attempts to make an illegal attack.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The attacker attempts to select a card index that does not exist in their hand.</exception>
        private void OnPromptAttacker(object sender, GameActionEventArgs e)
        {
            if (e.Action == Player.NO_ACTION)
            {
                (sender as Player).GiveUpAttack();
            }
            else if (e.Action >= 0 && e.Action < (sender as Player).Hand.Count)
            {
                if (IsValidAttack((sender as Player).Hand[e.Action]))
                {
                    (sender as Player).AttackWith(e.Action);
                }
                else
                {
                    throw new ArgumentException(string.Format("Cannot attack with the {0}!", (sender as Player).Hand[e.Action].ToString()));
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Invalid hand index {0} for player {1}.", e.Action, (sender as Player).Name));
            }
        }

        /// <summary>
        /// Event handler for when the defender is prompted to act.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ArgumentException">The defender attempts to make an illegal defense.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The defender attempts to select a card index that does not exist in their hand.</exception>
        private void OnPromptDefender(object sender, GameActionEventArgs e)
        {
            if (e.Action == Player.NO_ACTION)
            {
                (sender as Player).GiveUpDefense();
            }
            else if (e.Action >= 0 && e.Action < (sender as Player).Hand.Count)
            {
                if (IsValidDefense((sender as Player).Hand[e.Action]))
                {
                    (sender as Player).DefendWith(e.Action);
                }
                else
                {
                    throw new ArgumentException(string.Format("Cannot defend with the {0}!", (sender as Player).Hand[e.Action].ToString()));
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("Invalid hand index {0} for player {1}.", e.Action, (sender as Player).Name));
            }
        }

        /// <summary>
        /// Event handler for attacker's Attack event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidOperationException">The chosen card is not a valid attack for the current game state.</exception>
        private void OnNewAttack(object sender, GameActionEventArgs e)
        {
            if (IsValidAttack((sender as Player).Hand[e.Action]))
            {
                // Add the attack card to the pile.
                AttackCardsPlayed.Add((sender as Player).Hand[e.Action]);
            }
            else
            {
                throw new InvalidOperationException((sender as Player).Hand[e.Action] + " is not a valid attack!");
            }
        }

        /// <summary>
        /// Event handler for defender's Defend event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidOperationException">The chosen card is not a valid defense for the current game state.</exception>
        private void OnNewDefend(object sender, GameActionEventArgs e)
        {
            if ((sender as Player).Hand[e.Action].CanDefendAgainst(AttackCardsPlayed.Last(), this.Game.TrumpSuit))
            {
                // Add the defense card to the pile.
                DefenseCardsPlayed.Add((sender as Player).Hand[e.Action]);
            }
            else
            {
                throw new InvalidOperationException((sender as Player).Hand[e.Action] + " is not a valid defense!");
            }
        }

        /// <summary>
        /// Event handler for when a player concedes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConcede(object sender, GameLogEventArgs e)
        {
            if (sender == Attacker)
            {
                Winner = Defender;
            }
            else if (sender == Defender)
            {
                Winner = Attacker;
            }
        }

        /// <summary>
        /// Event handler for when a player empties their hand during a bout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHandEmpty(object sender, GameLogEventArgs e)
        {
            if (sender == Attacker)
            {
                Winner = Attacker;
            }
            else if (sender == Defender)
            {
                Winner = Defender;
            }
        }

        #endregion
    }
}
