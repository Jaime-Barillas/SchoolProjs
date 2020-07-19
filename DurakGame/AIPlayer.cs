using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    /// <summary>
    /// Represents a generic computer player engaged in the game. Specific AI behavior needs to be defined in child classes that extend this class.
    /// </summary>
    public abstract class AIPlayer : Player
    {
        #region Properties
        /// <summary>
        /// Weighted list of potential options for AI player action.
        /// Key: Card index (or NO_ACTION).
        /// Value: Weight.
        /// </summary>
        protected Dictionary<int, int> DecisionMatrix { get; set; }

        #endregion

        #region Constructors
        public AIPlayer(string name = "AI Player") : base(name)
        {
            DecisionMatrix = new Dictionary<int, int>();
            InitializeMatrix();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Prompt the AI to take an action.
        /// </summary>
        /// <returns>The "action index"</returns>
        public override void PromptAction()
        {
            // AI player is prompted based on a decision selected from its decision matrix.
            OnPrompt(new GameActionEventArgs(GetDecision()));
        }

        /// <summary>
        /// Clears the decision matrix.
        /// </summary>
        /// <param name="actionRequired">If false, the "no action" decision will be added to the matrix once cleared.</param>
        public void InitializeMatrix(bool actionRequired = false)
        {
            DecisionMatrix.Clear();
            if (!actionRequired)
            {
                DecisionMatrix.Add(NO_ACTION, 1);
            }
        }

        /// <summary>
        /// Recalculates decision matrix weights based on some logic defined by child classes.
        /// </summary>
        protected abstract void RecalculateWeights();

        /// <summary>
        /// Adds a decision to the decision matrix.
        /// </summary>
        /// <param name="index">The index of the card (in hand, etc) to potentially be chosen.</param>
        protected void AddDecisionToMatrix(int index)
        {
            DecisionMatrix.Add(index, 0);
            RecalculateWeights();
        }

        /// <summary>
        /// Adds multiple decisions to the decision matrix.
        /// </summary>
        /// <param name="indices">An array of card indexes to potentially be chosen.</param>
        public void AddDecisionsToMatrix(int[] indices)
        {
            foreach (int index in indices)
            {
                DecisionMatrix.Add(index, 0);
            }
            RecalculateWeights();
        }

        /// <summary>
        /// Selects a random decision from the AI player's weighted decision matrix.
        /// </summary>
        /// <returns>An integer representing the index of a card in the player's hand, or -1 if the chosen action is to do nothing (giving up the attack/defense).</returns>
        protected virtual int GetDecision()
        {
            int sumOfWeights = 0;           // Sum of weights of all decisions
            Random rnd = new Random();
            int randomValue;                // A random value used to select a decision
            int returnValue = NO_ACTION;    // The decision returned
            bool decisionReached = false;

            // Bypass all decision logic and return the "do nothing" decision if the decision matrix is empty.
            if (DecisionMatrix.Count == 0)
            {
                return NO_ACTION;
            }

            // The sum of weights is calculated by consecutively adding each weight value.
            foreach (int weight in DecisionMatrix.Values)
            {
                sumOfWeights += weight;
            }

            // A random number is chosen, ranging between 1 and the sum of weights.
            randomValue = rnd.Next(sumOfWeights) + 1;

            foreach (int decision in DecisionMatrix.Keys)
            {
                // For each possible decision, its weight is subtracted from the random value.
                randomValue -= DecisionMatrix[decision];
                // A decision is reached when the random value hits zero for the first time.
                if (!decisionReached && randomValue <= 0)
                {
                    returnValue = decision;
                    decisionReached = true;
                }
            }

            return returnValue;
        }

        #endregion
    }
}
