using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Event data for in-game player actions.
    /// </summary>
    public class GameActionEventArgs : EventArgs
    {
        /// <summary>
        /// The "action index", generally representing the hand index of the card being played.
        /// </summary>
        public int Action { get; }

        /// <summary>
        /// Instantiates a new set of arguments for Durak in-game card events.
        /// This constructor should be used when you want to manipulate, and "return", the Action value.
        /// </summary>
        public GameActionEventArgs()
        {
            Action = Player.NO_ACTION;
        }

        /// <summary>
        /// Instantiates a new set of arguments for Durak in-game card events.
        /// This constructor should be used when the intended value of Action is already known.
        /// </summary>
        /// <param name="card">The "action index", generally representing the hand index of the card being played.</param>
        public GameActionEventArgs(int action)
        {
            Action = action;
        }
    }
}
