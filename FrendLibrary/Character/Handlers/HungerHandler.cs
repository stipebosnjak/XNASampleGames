using Frend.Classes;

namespace Frend.Character.Handlers
{
    internal class HungerHandler
    {
        #region Enums

        internal enum State
        {
            Hungry,
            Fed,
            VeryHungry
        }

        #endregion

        #region Constants
        private const float GETTING_HUNGRY_NUMBER = 0.05f;
        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods

        private void Handler()
        {
            if (_hunger > 0)
            {
                _hunger -= GETTING_HUNGRY_NUMBER;
            }
            else
            {
                _hunger = 0;
            }
            var percHunger = _hunger / MAX_HUNGER;
            _maxSpeed = MAX_SPEED_STARTING * percHunger;

            switch (_hungerStatus)
            {
                case HungerStatus.Hungry:
                    Texture = StaticContentLoader.Character2DContent;
                    break;
                case HungerStatus.Fed:
                    Texture = StaticContentLoader.Character2D;
                    break;
                case HungerStatus.VeryHungry:
                    Texture = StaticContentLoader.Character2DUnHappy;
                    break;
            }
        }

        public void Feed()
        {
            
        }
        #endregion
    }
}
