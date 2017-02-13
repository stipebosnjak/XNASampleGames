using Humanity.Actors;
using Microsoft.Xna.Framework;
using Xna.Helpers;

namespace Humanity.Goap.Goals
{
    class GoalFeed:Xna.Helpers.Goap.Goal<Human>
    {
        
        private TimeOut _feedFailTimeout;

        public GoalFeed(Human character, GoalType type)
            : base(character, type)
        {
            Note = "Feed";
            _feedFailTimeout=new TimeOut(3000);
        }

        public override void Activate()
        {
            GoalStatusP = GoalStatus.Active;
            CharOwner.IsHungry = true;
            
            Start();
           
            base.Activate();
        }

        public override GoalStatus Process()
        {
            ActivateIfInactive();
          
            

            if (!_feedFailTimeout.IsStopWatchRunning )
            {
                GoalStatusP = GoalStatus.Failed;
            }
            //else if (Vector2.Distance(_closestFood.Position,CharOwner.Position)<10)
            //{
            //    this.GoalStatusP = GoalStatus.Completed;
            //}

            return base.Process();
        }

        public override void Terminate()
        {
           // if (_closestFood!=null)
          
            if (GoalStatusP==GoalStatus.Completed)
            {
            // CharOwner.IsHungry = false;
            // _closestFood.Remove();
            //_closestFood = null;
            //CharOwner.Hunger = 0f;
            }
            if (GoalStatusP==GoalStatus.Failed)
            {
               // CharOwner._cantReachFood.Add(_closestFood);
            }
            base.Terminate();
        }

        private void Start()
        {
            //_closestFood = CharOwner.FindFood();
            //if (_closestFood != null)
            //{
            //    CharOwner.SteeringBehaviour.SeekOn(_closestFood.Position);
            //    _feedFailTimeout.StartTimer();
            //}
        }

    }
}
