using Microsoft.Xna.Framework;
using Xna.Helpers;

namespace Frend.Character.Actions.Move
{



    class Dance:Action
    {
       

        #region Constants
        private const float DANCE_SWITCH_TIME_MS = 3000;
        #endregion

        #region Fields
        private float _amplitude;
        private float _frequency;
        private TimeOut _danceTimeOut;
        #endregion

        #region Constructors
        public Dance()
        {
            _amplitude = Generators.RandomNumber(-3f, 4f);
            _frequency = Generators.RandomNumber(-3f, 4f);
            _danceTimeOut=new TimeOut(DANCE_SWITCH_TIME_MS);
        }
        #endregion

        #region Methods

        protected override void Do()
        {
            if (_danceTimeOut.Update())
            {
                _amplitude = Generators.RandomNumber(-3f, 4f);
                _frequency = Generators.RandomNumber(-3f, 4f);
            }
            var offset = MovingHelper.Stager(_frequency, _amplitude, FrendGame.GameTime);
            MainChar.Velocity += offset;

        }
        #endregion
    }
}