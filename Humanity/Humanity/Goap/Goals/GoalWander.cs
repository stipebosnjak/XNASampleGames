using Humanity.Actors;
using Xna.Helpers.Goap;


namespace Humanity.Goap.Goals
{
    class GoalWander : Goal<Human>
    {
        public GoalWander(Human character, GoalType type)
            : base(character, type)
        {
            Note = "Wander";
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
            CharOwner.SteeringBehaviour.WanderOn();
            base.Activate();
        }
        public override GoalStatus Process()
        {
            ActivateIfInactive();
            return base.Process();
        }
        public override void Terminate()
        {
            CharOwner.SteeringBehaviour.WanderOff();
            base.Terminate();
        }
        
    }
}
