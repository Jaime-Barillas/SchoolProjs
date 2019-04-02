using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a human player engaged in the game.
    /// </summary>
    public class HumanPlayer : Player
    {
        #region Constructors
        public HumanPlayer(string name = "Player") : base(name)
        { }

        #endregion

        #region Methods
        /// <summary>
        /// Prompt the player to take action. The logic for what "prompting" the player means must be defined in the external program.
        /// </summary>
        public override void PromptAction()
        {
            OnPrompt(new GameActionEventArgs());
        }

        #endregion
    }
}
