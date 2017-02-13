using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna.Helpers._2D;

namespace Flee.Classes
{

    #region Enums

    #endregion

    class Soldier:Actor
    {
        protected const float FREQUENCY_MAX = 3f;
        protected const float AMPLITUDE_MAX = 3f;
        protected const float FREQUENCY_MIN = 1f;
        protected const float AMPLITUDE_MIN = 0f;
        protected const float MAX_SPEED = 4f;

        protected const double ACCELERATE_TIMEOUT = 5000; //in milisec
        protected const float ACCELERATE_VALUE = 0.3f;

        #region Fields

        private bool _colorChange;

        private static Random _random;

        private float _frequency;
        private float _ampitude;
        private float _speed;

        private Actor _target;
        private TimeSpan _timeSpan;

        #endregion

        #region Properties
        public bool ColorChange
        {
            get { return _colorChange; }
            set { _colorChange = value; }
        }

        public static Random Random
        {
            get { return _random; }
            set { _random = value; }
        }

        public float Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        public float Ampitude
        {
            get { return _ampitude; }
            set { _ampitude = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public TimeSpan TimeSpan
        {
            get { return _timeSpan; }
            set { _timeSpan = value; }
        }

        #endregion

        #region Constructors
        static Soldier()
        {
            _random=new Random();
        }
        #endregion

        #region Methods
        protected  Vector2 Hunt(Actor hunter)
        {
            if (Target!=null)
            {
              Vector2 direction = _target.Position - hunter.Position;
            if (direction.Length() > 0f)
            {
                direction.Normalize();
            }
            Velocity = direction * Speed;
            }
            else
            {
                Velocity = Vector2.Zero;
            }

            return Velocity;
        }

        protected bool ActorCollision(Actor actor1,Actor actor2)
        {
            if (actor1 != null&&actor2 != null)
                    if (CollisionDetection2D.BoundingCircle(actor1.Position, actor1.Radius, actor2.Position,
                                                                        actor2.Radius))
                        return true;

            return false;
        }

        protected void Stager()
        {
            Position += MovingHelper.Stager(Frequency, Ampitude, Game1.GameTime);
        }

        public void Destroy()
        {
            Actors.Remove(this);
        }
        #endregion
    }
}