using Frend.V2.Character;
using Xna.Helpers.Goap;

namespace Frend.V2.Goap.GoalEvaluators
{
    class GoalEvaluatorWander :GoalEvaluator<Tamagotchi>
    {
        public GoalEvaluatorWander(float characterBias)
            : base(characterBias)
        {
        }

        public override float CalculateDesirability(Tamagotchi character)
        {
            float desirability = 0.05f;

            desirability *= CharacterBias;

            return desirability;

        }

        public override void SetGoal(Tamagotchi character)
        {
            character.Brain.AddGoalWander();
        }
    }
}
    

