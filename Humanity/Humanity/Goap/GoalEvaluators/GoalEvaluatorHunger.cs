#region

using Humanity.Actors;
using Xna.Helpers.Goap;

#endregion

namespace Humanity.Goap.GoalEvaluators
{
    internal class GoalEvaluatorHunger : GoalEvaluator<Human>
    {
        public GoalEvaluatorHunger(float characterBias) : base(characterBias)
        {
        }


        public override float CalculateDesirability(Human character)
        {
            float desirability=character.Hunger;
            //var percHunger = _hunger / MAX_HUNGER;
            //_maxSpeed = MAX_SPEED_STARTING * percHunger;
            desirability *= CharacterBias;

            return desirability;
        }

        public override void SetGoal(Human character)
        {
            character.Brain.AddGoalFeed();
        }
    }
}