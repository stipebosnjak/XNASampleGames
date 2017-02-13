using Frend.V2.Character;
using Xna.Helpers.Goap;

namespace Frend.V2.Goap.Goals
{
    class GoalWander :Goal<Tamagotchi>
    {
        public GoalWander(Tamagotchi character, GoalType type) : base(character, type)
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
