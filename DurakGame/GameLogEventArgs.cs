using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Event data for Durak console output events.
    /// </summary>
    public class GameLogEventArgs : EventArgs
    {
        /// <summary>
        /// Message to be displayed to the game log.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Instantiates a new set of event arguments for Durak console output events.
        /// </summary>
        /// <param name="message">The message to display in the game log.</param>
        public GameLogEventArgs(string message)
        {
            Message = message;
        }
    }
}
