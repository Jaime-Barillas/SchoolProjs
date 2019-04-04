using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a simple AI player engaged in the game. This AI's internal logic simply selects an action purely at random.
    /// </summary>
    public class SimpleAIPlayer : AIPlayer
    {
        #region Properties
        private static int difficulty;
        /// <summary>
        /// The likelihood of the SimpleAIPlayer playing a card. Lower values cause the AI to give up the attack/defense more often.
        /// </summary>
        public static int Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Difficulty must be a positive integer.");
                }
                else
                {
                    difficulty = value;
                }
            }
        }

        #endregion

        #region Constructors
        public SimpleAIPlayer(string name = "AI Player") : base(name)
        {
            Difficulty = 2;
        }

        #endregion

        #region Methods
        /// <summary>
        /// "Recalculates" weights by weighting all potential decisions equally, causing the AI to act entirely at random without any strategy.
        /// </summary>
        protected override void RecalculateWeights()
        {
            foreach (int decision in DecisionMatrix.Keys.ToList())
            {
                if (decision == NO_ACTION)
                {
                    DecisionMatrix[decision] = 1;
                }
                else
                {
                    DecisionMatrix[decision] = Difficulty;
                }
            }
        }

        #endregion
    }
}
