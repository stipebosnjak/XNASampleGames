#region

using System;
using Frend.V2.Character;
using Frend.V2.Items;
using Xna.Helpers.Goap;

#endregion

namespace Frend.V2.Goap.GoalEvaluators
{
    internal class GoalEvaluatorHunger : GoalEvaluator<Tamagotchi>
    {
        public GoalEvaluatorHunger(float characterBias) : base(characterBias)
        {
        }


        public override float CalculateDesirability(Tamagotchi character)
        {
            float desirability=character.Hunger;
            //var percHunger = _hunger / MAX_HUNGER;
            //_maxSpeed = MAX_SPEED_STARTING * percHunger;
            desirability *= CharacterBias;

            return desirability;
        }

        public override void SetGoal(Tamagotchi character)
        {
            character.Brain.AddGoalFeed();
        }
    }
}