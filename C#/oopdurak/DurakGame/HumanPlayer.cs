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
        #region Events
        /// <summary>
        /// Fired when the player accepts input regarding which decision they should make.
        /// </summary>
        public event EventHandler<GameActionEventArgs> AcceptInput;
        protected virtual void OnAcceptInput(GameActionEventArgs a)
        {
            AcceptInput?.Invoke(this, a);
        }

        #endregion

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
            GameActionEventArgs choice = new GameActionEventArgs();

            // Get input from human player
            OnAcceptInput(choice);

            // Use the same GameActionEventArgs when moving to the prompt, to maintain the Action value
            OnPrompt(choice);
        }

        #endregion
    }
}
