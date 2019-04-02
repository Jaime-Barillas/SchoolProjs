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
        #region Constructors
        public SimpleAIPlayer(string name = "AI Player") : base(name)
        { }

        #endregion

        #region Methods
        /// <summary>
        /// "Recalculates" weights by weighting all potential decisions equally, causing the AI to act entirely at random without any strategy.
        /// </summary>
        protected override void RecalculateWeights()
        {
            foreach (int decision in DecisionMatrix.Keys.ToList())
            {
                DecisionMatrix[decision] = 1;
            }
        }

        #endregion
    }
}
