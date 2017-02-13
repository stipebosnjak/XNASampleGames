using System;
using Microsoft.Xna.Framework;
using Xna.Helpers;

namespace Frend.Character.Actions.Move
{
    class Wander:Action
    {

        #region Constants
        private const float WANDER_TIME_MS = 1000;
        private const float WANDER_TIME_MS_MIN = 1000;
        private const float WANDER_TIME_MS_MAX = 2000;
        #endregion

        #region Fields
         private TimeOut _timeOut;

        #endregion

        #region Properties
        private bool IsTimeout
        {
            get { return _timeOut.Update(WANDER_TIME_MS_MIN,WANDER_TIME_MS_MAX); }
        }

        #endregion

        #region Constructors
        public Wander()
        {
            _timeOut=new TimeOut(WANDER_TIME_MS);
        }

        #endregion

        #region Methods
      
           protected override void Do()
        {
            Vector2 velocity = MainChar.Velocity;
            if (IsTimeout)
            {
                int randommNumber = Generators.RandomNumber(0, 3);
                Vector2 randomSpeed = Vector2.Zero;
                switch (randommNumber)
                {
                    case 0:
                        randomSpeed = new Vector2(velocity.X, Generators.RandomNumber(-maxSpeed, maxSpeed));
                        break;
                    case 1:
                        randomSpeed = new Vector2(Generators.RandomNumber(-maxSpeed, maxSpeed), currentVelocity.Y);
                        break;
                    case 2:
                        randomSpeed = Vector2.Zero;
                        break;
                }
                return randomSpeed;
            }
            
        }
        
        #endregion

     
    }
}