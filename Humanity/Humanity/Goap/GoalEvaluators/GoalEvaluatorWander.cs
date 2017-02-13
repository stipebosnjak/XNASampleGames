using Humanity.Actors;
using Xna.Helpers.Goap;

namespace Humanity.Goap.GoalEvaluators
{
    class GoalEvaluatorWander : Xna.Helpers.Goap.GoalEvaluator<Human>
    {
        public GoalEvaluatorWander(float characterBias)
            : base(characterBias)
        {
        }

        public override float CalculateDesirability(Human character)
        {
            float desirability = 0.05f;

            desirability *= CharacterBias;

            return desirability;

        }

        public override void SetGoal(Human character)
        {
            character.Brain.AddGoalWander();
        }
    }
}
    

