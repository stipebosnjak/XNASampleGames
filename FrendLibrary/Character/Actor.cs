﻿#region

using System.Text;
using Frend.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xna.Helpers;

#endregion

#region Enums

internal enum HungerStatus
{
    Hungry,
    Fed,
    VeryHungry
}

internal enum Status
{
    JustStarted,
    Interested,
    Dancing,
    GoingToMiddle,
    NotInterested,
    NoMorePoi,
    OnTarget,
    Wander,
    MovingFromWall,
    Disgusted
}

#endregion

namespace Frend.Character
{
    public class Actor : GameItem
    {
        //debug
       
        private const int MAX_HUNGER = 100;
        private const int FEED_AMOUNT = 20;
       
        private const float STARTING_STOP_TIMER_MS = 500;
       
      
        private const float STARTING_AMPLITUDE = 2;
        private const float STARTING_FREQUENCY = 2;

       

        #region Fields

      

        #region Timers

       
        private float _pauseBetweenDanceRa;
        private float _pauseBetweenMoveMsRa;
        private TimeOut _timeoutBetweenMove;
        private TimeOut _wanderTimeout;
        private float _poopTimeoutTime;
        private TimeOut _poopTimeout;
        private TimeOut _stopTimeout;
        

        #endregion

        private PointOfInterest _currentInterest;
        private Collections.DictionaryV2<string, PointOfInterest> _currentInterests;
        private float _hunger;
        private HungerStatus _hungerStatus;
        private float _maxSpeed;
        private MouseState _mouseStateCurrent;
        private MouseState _mouseStatePrevious;
        private readonly SpriteFontText _overHeadText;
        private readonly float _rotation;
        private float _scale;
        private Sides _side;
        private int _speedMultiplier;
        private Status _status;
        private Poop _closestPoop;
        #endregion

        #region Properties

        public Vector2 Velocity { get; set; }
        
        

        public bool IsNearWall
        {
            get
            {
                return CollisionDetection2D.NearestDistanceToWall(Position, FrendGame.SCREEN_WIDTH,
                                                                  FrendGame.SCREEN_HEIGHT,
                                                                  50, out _side);
            }
        }

        public bool IsNearPoop
        {
            get
            {
                bool result = false;
                foreach (var poop in Poop.Poops)
                {
                    if (!IsNearTarget(poop.Position, Texture.Width*2)) continue;
                    _closestPoop = poop;
                    result = true;
                    break;
                }
                return result;

            }
        }
        #endregion

        #region Constructors

        public Actor()
        {
            Position = SceneManager.Center;
            Velocity = Vector2.Zero;
            Texture = StaticContentLoader.Character2D;
            _overHeadText = new SpriteFontText(Color.DarkSlateBlue);
            _scale = 1f;
            _rotation = 0f;
            _mouseStatePrevious = new MouseState();
            InitiliazeTimers();
            InitiliazeStatuses();
            InitializeInterests();
            InitializeUnSorted();
        }

        #endregion

        #region Methods

        #region Xna

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, _rotation, Origin, 1f, SpriteEffects.None, 0f);
        }

        public override void Update()
        {
            Text();
            Progress();
            CheckRules();
            Move();
            Input();
        }

        #endregion

        #region AI

        #region Main

        private void Progress()
        {
            StatusHandler();

            HungerHandler();
        }

        private void CheckRules()
        {
            if (_currentInterest != null)
            {
                _status = Status.Interested;
            }
            else if (_currentInterest == null)
            {
                _status = Status.Wander;
            }
            else if (_currentInterests.Count == 0)
            {
                _currentInterests = SceneManager.CurrentScene.PointsOfInterest;

                // _status = Status.Dancing;
            }
            if (_currentInterest != null && IsNearTarget(_currentInterest.Position, 8))
            {
                _status = Status.OnTarget;
            }
            else if (IsNearWall && _status == Status.Wander)
            {
                _status = Status.MovingFromWall;
            }
            else if (!IsNearWall && _status == Status.MovingFromWall)
            {
                _status = Status.Wander;
            }
            else if (IsNearPoop && _status == Status.Wander)
            {
                _status = Status.Disgusted;
            }
            else if (!IsNearPoop && _status == Status.Disgusted)
            {
                _status = Status.Wander;
            }
            //Hunger rules
            if (_hunger < 20f)
            {
                _hungerStatus = HungerStatus.VeryHungry;
            }
            else if (_hunger < 60f)
            {
                _hungerStatus = HungerStatus.Hungry;
            }
            else
            {
                _hungerStatus = HungerStatus.Fed;
            }

            
        }

        private void Text()
        {
            _overHeadText.Position = new Vector2(Position.X, Position.Y - Texture.Height/2 - 10);
            var overheadText = new StringBuilder();
            string newLine = "\n";
            string separator = "";
            //Arrange in order you want to overhead text like like

            overheadText.Append(StatusTextHandler());
            //separate
            overheadText.Append(newLine);
            //separate
            overheadText.Append(HungerTextHandler());
            //separate
            overheadText.Append(newLine);
            //separate
            overheadText.Append(InputTextHandler());

            _overHeadText.Text = overheadText.ToString();
        }

        private void Move()
        {
            MoveHandler();
        }

        #endregion

        #region Handlers

        private void MoveHandler()
        {
            Position += Velocity;
        }


        private void StatusHandler()
        {
            switch (_status)
            {
                case Status.Wander:
                    Velocity = Wander();
                    _currentInterest = IsNearPoi(200);
                    ReleasePoop();
                    break;
                case Status.JustStarted:
                    //     FindPointOfInterest();
                    break;
                case Status.Interested:
                    Velocity = CalculateVelocity(_currentInterest.Position);
                    break;
                case Status.NotInterested:
                    FindPointOfInterest();
                    break;
                case Status.GoingToMiddle:
                    Velocity = CalculateVelocity(SceneManager.Center);
                    break;
                case Status.Dancing:
                    Velocity = Dance();
                    break;
                case Status.OnTarget:
                    Velocity = Vector2.Zero;
                    TimeOnTarget();
                    break;
                case Status.NoMorePoi:
                    break;

                case Status.MovingFromWall:
                    Velocity = Generators.RandomizedOppositeDirection(_side, _maxSpeed);
                    break;

                case Status.Disgusted:
                    Velocity = -VelocityToTarget(_closestPoop.Position);
                    break;
            }
        }

        private string StatusTextHandler()
        {
            string statusText = "";
            switch (_status)
            {
                case Status.JustStarted:
                    statusText = "Just started";
                    break;
                case Status.Interested:
                    statusText = "I am going to " + _currentInterest.Name;
                    break;
                case Status.NotInterested:
                    statusText = "Not Interested";
                    break;
                case Status.GoingToMiddle:
                    statusText = "Going To Middle";
                    break;
                case Status.Dancing:
                    statusText = "Dancing";
                    break;
                case Status.OnTarget:
                    statusText = "I am in the  " + _currentInterest.Name;
                    break;
                case Status.NoMorePoi:
                    statusText = "Nothing more interests me";
                    break;
                case Status.Wander:
                    statusText = "I am wandering around";
                    break;
                case Status.MovingFromWall:
                    statusText = "Phew almost hit " + _side + " Wall";
                    break;
            }
            return statusText;
        }

        private string HungerTextHandler()
        {
            var statusText = "";
            SceneManager.CurrentScene.UpperLeftText = "Hunger: " + _hunger + "\n Debug: maxspeed " + _maxSpeed + "  " +
                                                      Velocity;
            switch (_hungerStatus)
            {
                case HungerStatus.Fed:
                    statusText = "I am fed";
                    break;
                case HungerStatus.Hungry:
                    statusText = "I am hungry";
                    break;
                case HungerStatus.VeryHungry:
                    statusText = "I am very hungry";
                    break;
            }
            return statusText;
        }

        #endregion

       

      
        private PointOfInterest IsNearPoi(float range)
        {
            foreach (var pointOfInterest in _currentInterests.Dictionary)
            {
                if (IsNearTarget(pointOfInterest.Value.Position, range))
                {
                    return pointOfInterest.Value;
                }
            }
            return null;
        }

      

       

        #region Actions


        private void FindPointOfInterest()
        {
            if (_currentInterest != null || _currentInterests.Count == 0) return;

            _currentInterest = _currentInterests.GetRandomObject();
        }

        private void TimeOnTarget()
        {
            if (!_timeoutBetweenMove.Update()) return;
            _currentInterests.Remove(_currentInterest.Name);
            _currentInterest = null;
        }

        private void Feed(float amount)
        {
            _hunger += amount;
        }

        private void ReleasePoop()
        {
            if (_poopTimeout.Update())
            {
                  new Poop(this.Position);
            }
        }

        
      
        private void ResetInterests()
        {
            _currentInterests = SceneManager.CurrentScene.PointsOfInterest;
        }

        private Vector2 ProcessWallCollision()
        {
            return CollisionDetection2D.ProcessWallCollision(Position, Texture, FrendGame.SCREEN_WIDTH,
                                                             FrendGame.SCREEN_HEIGHT);
        }

        private bool CheckWallCollision()
        {
            return CollisionDetection2D.CheckWallCollision(Position, Texture, FrendGame.SCREEN_WIDTH,
                                                           FrendGame.SCREEN_HEIGHT);
        }

        #endregion

        #endregion

        #region Input

        private void Input()
        {
            InputHandler();
        }

        private void InputHandler()
        {
            if (IsMouseClickedOnChar)
            {
                if (_hunger >= 100) return;

                if (MAX_HUNGER - _hunger < FEED_AMOUNT)
                {
                    Feed(MAX_HUNGER - _hunger);
                }
                else
                {
                    Feed(FEED_AMOUNT);
                }
            }
        }

        private string InputTextHandler()
        {
            var outString="";
            if (IsMouseHoverOnChar)
            {
                outString= "Click to feed me!!!";
            }
            return outString;
        }

        #endregion

        #region Initializers

        private void InitiliazeTimers()
        {
            _poopTimeout=new TimeOut(_poopTimeoutTime);
            _stopTimeout=new TimeOut(STARTING_STOP_TIMER_MS);
        }

        private void InitiliazeStatuses()
        {
            _status = Status.Wander;
            _side = Sides.None;
        }

        private void InitializeInterests()
        {
            _currentInterests = new Collections.DictionaryV2<string, PointOfInterest>();
            _currentInterests = SceneManager.CurrentScene.PointsOfInterest;
        }

        private void InitializeUnSorted()
        {
            _hunger = MAX_HUNGER;
           
        }

        #endregion

        #endregion

        #region Obsolete - For future reference

        private Vector2 VelocityToTarget(Vector2 target)
        {
            Vector2 calc = Vector2.Subtract(target, Position);
            float distance = Vector2.Distance(target, Position);
            if (distance < 1)
                return Vector2.Zero;

            calc.Normalize();
            return calc*_speedMultiplier;
        }

        //private void Move()
        //{
        //    Vector2 directionToPoi = Vector2.Zero;

        //    if (!IsNearTarget(_targetPosition,4))
        //    {
        //        if (_currentInterest != null)
        //        {
        //            directionToPoi = DirectionToTarget(_targetPosition);
        //        }
        //        else
        //        {
        //            if (_isDancing)
        //            {
        //                _rotation = (float) (FrendGame.GameTime.TotalGameTime.TotalSeconds*2);
        //                directionToPoi = Dance();
        //            }
        //            else
        //            {
        //                _targetPosition = new Vector2(FrendGame.SCREEN_WIDTH/2, FrendGame.SCREEN_HEIGHT/2);
        //                directionToPoi = DirectionToTarget(_targetPosition);
        //            }
        //            //if (IsInTarget(new Vector2(FrendGame.SCREEN_WIDTH/2, FrendGame.SCREEN_HEIGHT/2)))
        //            //{
        //            //    directionToPoi = Vector2.Zero;
        //            //}
        //        }
        //    }
        //    // Vector2 limitedDirectionVector = new Vector2(MathHelper.Clamp(directionToPoi.X, -MAX_SPEED, MAX_SPEED), MathHelper.Clamp(directionToPoi.Y, -MAX_SPEED, MAX_SPEED));
        //    Velocity = directionToPoi;
        //    Position += Velocity;

        //    // Position = Position + Xna.Helpers.MovingHelper.Stager(0.5f, 0.5f, Game1.GameTime);
        //}

        //private void CheckRules()
        //{
        //    if (_currentInterest != null)
        //    {
        //        if (IsOnTarget(_targetPosition))
        //        {
        //            _overHeadText.Text = "I am in " + _currentInterest.Name;
        //            if (!_timeoutBetweenMove.StopWatch.IsRunning)
        //            {
        //                _timeoutBetweenMove.StopWatch.Start();
        //            }
        //            if (_timeoutBetweenMove.Update())
        //            {
        //                _timeoutBetweenMove.RestartTimer();
        //                //    SceneManager.CurrentScene.PointsOfInterest.Remove(_currentInterest);
        //                _currentInterest = null;
        //            }
        //        }
        //        else
        //        {
        //            _overHeadText.Text = "I am going to " + _currentInterest.Name;
        //        }
        //    }
        //    else
        //    {
        //        bool hasText = true;
        //        string outText = "";
        //        TextTimeout.PlayText(ref hasText, ref outText, "Finish");
        //        if (hasText)
        //        {
        //            _overHeadText.Text = outText;
        //        }
        //        else
        //        {
        //            _isDancing = true;

        //            _overHeadText.Text = "No more conversation I am rolling";
        //        }
        //    }
        //}

        #endregion
    }
}