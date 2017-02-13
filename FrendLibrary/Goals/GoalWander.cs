using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frend.Classes;

namespace FrendLibrary.Goals
{
    class GoalWander:Goal
    {
        public GoalWander(Character character, GoalType type)
            : base(character, type)
        {
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
            CharOwner.MovingHelper.Wander(CharOwner.Position);
            base.Activate();
        }
        public override GoalStatus Process()
        {
            ActivateIfInactive();

            return base.Process();
        }
        public override void Terminate()
        {
            //shutdown wander behaviour
            base.Terminate();
        }
        
    }
}
