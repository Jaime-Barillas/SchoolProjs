using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// A wrapper class that contains all information about the current game state.
    /// </summary>
    public class Game
    {
        #region Properties
        /// <summary>
        /// The players participating in the game.
        /// </summary>
        public List<Player> Players { get; private set; }

        /// <summary>
        /// The number of active players in the game.
        /// </summary>
        public int ActivePlayers { get; private set; }

        /// <summary>
        /// The talon used for the game.
        /// </summary>
        public Talon Talon { get; }

        /// <summary>
        /// The game's current bout.
        /// </summary>
        public Bout CurrentBout { get; private set; } = null;

        /// <summary>
        /// The trump suit used during this game.
        /// </summary>
        public Suit TrumpSuit { get; }

        /// <summary>
        /// The poor fool who lost the game. Initially null.
        /// </summary>
        public Player Fool { get; private set; } = null;

        /// <summary>
        /// The minimum hand size a player must draw up to (unless the talon is empty).
        /// </summary>
        public static int MINIMUM_HAND_SIZE = 6;

        #endregion

        #region Events
        /// <summary>
        /// Fired when the game ends.
        /// </summary>
        public event EventHandler<GameLogEventArgs> End;
        protected virtual void OnEnd(GameLogEventArgs l)
        {
            End?.Invoke(this, l);
        }

        /// <summary>
        /// Fired when a new bout replaces the old one.
        /// </summary>
        public event EventHandler<GameLogEventArgs> NewBout;
        protected virtual void OnNewBout(GameLogEventArgs l)
        {
            NewBout?.Invoke(this, l);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a new Durak game.
        /// </summary>
        /// <param name="deck">The deck to use as the talon.</param>
        /// <param name="players">The players involved in the game.</param>
        public Game(Deck deck, List<Player> players)
        {
            // Throw an exception if the deck is too small
            if (deck.Size < (players.Count * MINIMUM_HAND_SIZE))
            {
                throw new ArgumentException(string.Format("Deck only contains {0} cards! A game with {1} players needs a deck with at least {2} cards.", 
                    deck.Size, players.Count, (players.Count * MINIMUM_HAND_SIZE)));
            }
            this.Talon = new Talon(deck);
            this.Players = players;
            ActivePlayers = 0;
            // Count the active players, just in case we were passed an inactive player (for some reason)
            foreach (Player player in Players)
            {
                if (player.IsActive)
                {
                    ActivePlayers++;
                }
                // Subscribe to their HandEmpty event
                // (it really wasn't worth making a method for this)
                player.HandEmpty += delegate (object sender, GameLogEventArgs e) { ActivePlayers--; };
            }
            TrumpSuit = Talon.Trump.Suit;

            // Players draw their opening hands
            foreach (Player player in Players)
            {
                List<Card> openingHand = new List<Card>();
                for (int i = 0; i < MINIMUM_HAND_SIZE; i++)
                {
                    openingHand.Add(Talon.Draw());
                }
                player.TakeCards(openingHand);
            }

            // Set up the initial bout.
            NextBout();
        }

        /// <summary>
        /// Instantiates a new Durak game with one human player and one simple AI player.
        /// </summary>
        /// <param name="deck">The deck to use as the talon.</param>
        public Game(Deck deck) : this(deck, new List<Player>(new Player[] { new HumanPlayer(), new SimpleAIPlayer() }))
        { }

        /// <summary>
        /// Instantiates a new Durak game using a standard 36-card deck, with one human player and one simple AI player.
        /// </summary>
        public Game() : this(new Deck())
        { }

        #endregion

        #region Methods
        /// <summary>
        /// Fetches the player next in the turn order after a given player.
        /// </summary>
        /// <param name="player">The player to compare to.</param>
        /// <returns>The next active player in the turn order.</returns>
        public Player PlayerAfter(Player player)
        {
            if (!Players.Contains(player))
            {
                throw new ArgumentException(string.Format("{0} is not part of this game!", player.Name));
            }
            else
            {
                int index = Players.IndexOf(player);
                do
                {
                    // Move to next index
                    if (index == Players.Count - 1)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                    
                    // Return if this player is active (skipping over those that aren't)
                    if (Players[index].IsActive)
                    {
                        return Players[index];
                    }
                } while (index != Players.IndexOf(player));
                // If we looped all the way around back to the original player, then they're clearly the only person still in the game!
                throw new InvalidOperationException(string.Format("{0} is the only player still in the game...", player.Name)); // replace with "Game.End" event?
            }
        }

        /// <summary>
        /// Fetches the player previous in the turn order before a given player.
        /// </summary>
        /// <param name="player">The player being compared to.</param>
        /// <returns>The previous active player.</returns>
        public Player PlayerBefore(Player player)
        {
            if (!Players.Contains(player))
            {
                throw new ArgumentException(string.Format("{0} is not part of this game!", player.Name));
            }
            else
            {
                int index = Players.IndexOf(player);
                do
                {
                    // Move to previous index
                    if (index == 0)
                    {
                        index = Players.Count - 1;
                    }
                    else
                    {
                        index--;
                    }

                    // Return if this player is active (skipping over those that aren't)
                    if (Players[index].IsActive)
                    {
                        return Players[index];
                    }
                } while (index != Players.IndexOf(player));
                // If we looped all the way around back to the original player, then they're clearly the only person still in the game!
                throw new InvalidOperationException(string.Format("{0} is the only player still in the game...", player.Name)); // replace with "Game.End" event?
            }
        }

        /// <summary>
        /// Sets up the next bout.
        /// </summary>
        public void NextBout()
        {
            Player nextAttacker = null; // The player attacking in the next bout

            // If CurrentBout is null, then we're setting up the game's first bout
            if (CurrentBout == null)
            {
                // Determine who goes first
                // (implement this later; Players[0] goes first for now)
                nextAttacker = Players[0];
            }
            // Otherwise, the next bout's attacker is based on the results of the previous bout (before we replace it)
            else
            {
                if (CurrentBout.Winner == CurrentBout.Attacker)
                {
                    // If the defender loses, they don't get to attack
                    nextAttacker = PlayerAfter(CurrentBout.Defender);
                }
                else if (CurrentBout.Winner == CurrentBout.Defender)
                {
                    nextAttacker = CurrentBout.Defender;
                }
            }

            // Set up the bout
            CurrentBout = new Bout(this, nextAttacker, PlayerAfter(nextAttacker));
            OnNewBout(new GameLogEventArgs(string.Format("A new bout begins: {0} attacks {1}!", CurrentBout.Attacker.Name, CurrentBout.Defender.Name)));
        }

        /// <summary>
        /// Advances the primary game loop.
        /// </summary>
        public void Continue()
        {
            // If there's only one player remaining, then the game is over.
            if (ActivePlayers == 1)
            {
                // Assuming we haven't already figured out who the fool is (no need to do it twice!)
                if (Fool == null)
                {
                    // Iterate through the players to figure out which one was the fool.
                    foreach (Player player in Players)
                    {
                        if (player.IsActive)
                        {
                            Fool = player;
                        }
                    }
                }

                OnEnd(new GameLogEventArgs(string.Format("The game has ended! {0} is the fool!", Fool.Name)));
            }
            // If the Fool is still null, the game continues.
            else
            {
                // If the current bout is still ongoing...
                if (CurrentBout.Winner == null)
                {
                    // Advance the bout
                    CurrentBout.Continue();
                }
                else
                {
                    // All players replenish their hands, starting with the previous bout's attacker
                    Player replenishingPlayer = CurrentBout.Attacker;
                    for (int i = 0; !Talon.IsEmpty && i < ActivePlayers; i++ )
                    {
                        int cardsToDraw = MINIMUM_HAND_SIZE - replenishingPlayer.Hand.Count;
                        if (cardsToDraw > 0)
                        {
                            List<Card> cardsToAdd = new List<Card>();
                            for (int j = 0; !Talon.IsEmpty && j < cardsToDraw; j++)
                            {
                                cardsToAdd.Add(Talon.Draw());
                            }
                            replenishingPlayer.TakeCards(cardsToAdd);
                        }

                        // Replenishing moves counterclockwise (opposite to turn order)
                        replenishingPlayer = PlayerBefore(replenishingPlayer);
                    }
                    
                    // Move to the next bout
                    NextBout();
                }
            }
        }

        #endregion
    }
}
