using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xna.Helpers;
using Xna.Helpers._2D;

namespace Flee.Classes
{

    #region Enums

    #endregion

    internal class Player : Actor
    {
        private const int ACCELERATION = 1;
        private const int DEACCELERATION = 5;

        private const int MAX_SPEED = 10;

        private const double HEAL_TIME = 1200;
        private static TimeSpan _healTime;

        #region Fields
       
        private TimeOut timeout;
        private bool _isEvadingEnemies;
        private string _status;
        private float _rotation;
        private bool _isHealing;
        private float _health;

        private SpriteFont _spriteFont;

        private Ally _ally;

        #endregion

        #region Properties

        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }

        #endregion

        #region Constructors

        public Player()
        {
            _isEvadingEnemies = false;

            _rotation = MathHelper.ToRadians(0);
            Position = new Vector2(Game1.SCREEN_WIDTH/2, Game1.SCREEN_HEIGHT/2);
            Color = Color.White;
            Velocity = Vector2.Zero;
            timeout = new TimeOut(HEAL_TIME);
            _healTime = TimeSpan.FromMilliseconds(HEAL_TIME);
            if (GameManager.IsPlayerHuman)
            {
                _health = 100;
            }
            else
            {
                _health = 200;
            }

            if (GameManager.IsDebug)
            {
                _health = 5000;
            }
        }

        #endregion

        #region Methods

        #region XNA

        public override void LoadTexture(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(@"Textures\Player_1");
            _spriteFont = contentManager.Load<SpriteFont>("SpriteFont1");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ShowHealth(spriteBatch);
            ShowEnemiesRemaining(spriteBatch);
            spriteBatch.Draw(Texture, Position, null, Color, _rotation, Origin, 1f, SpriteEffects.None, 0);
        }

        public override void Update()
        {
            Move();

            ProcessCollisions();

            CheckStatus();

            base.Update();
        }

        private void CheckInput()
        {


            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                // _rotation = MathHelper.ToRadians(270);
                Velocity = new Vector2(MathHelper.Clamp(Velocity.X - ACCELERATION, -MAX_SPEED, 0), Velocity.Y);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Velocity = new Vector2(MathHelper.Clamp(Velocity.X + ACCELERATION, 0, MAX_SPEED), Velocity.Y);
                //  _rotation = MathHelper.ToRadians(90);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Velocity = new Vector2(Velocity.X, MathHelper.Clamp(Velocity.Y - ACCELERATION, -MAX_SPEED, 0));
                //   _rotation = MathHelper.ToRadians(0);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Velocity = new Vector2(Velocity.X, MathHelper.Clamp(Velocity.Y + ACCELERATION, 0, MAX_SPEED));
                //  _rotation = MathHelper.ToRadians(180);
            }

            if (keyboardState.GetPressedKeys().Length == 0)
            {
                if (Velocity.X > 0)
                {
                    Velocity = new Vector2(Velocity.X - MathHelper.Clamp(DEACCELERATION, 0, Velocity.X), Velocity.Y);
                }
                if (Velocity.Y > 0)
                {
                    Velocity = new Vector2(Velocity.X, Velocity.Y - MathHelper.Clamp(DEACCELERATION, 0, Velocity.Y));
                }
                if (Velocity.X < 0)
                {
                    Velocity = new Vector2(Velocity.X + MathHelper.Clamp(DEACCELERATION, -Velocity.X, 0), Velocity.Y);
                }
                if (Velocity.Y < 0)
                {
                    Velocity = new Vector2(Velocity.X, Velocity.Y + MathHelper.Clamp(DEACCELERATION, -Velocity.Y, 0));
                }
            }
        }
        

        #endregion

        #region  Mechanics

        private void Move()
        {
            if (GameManager.IsPlayerHuman) CheckInput();
            else
            {
                EvadeEnemies();
                if (!_isEvadingEnemies) HealSoldiers();
                Stager();
            }

            ManageRotation();
            Position += Velocity;
        }

        public void CheckHeal()
        {
            if (_ally != null)
            {
                // _healTime -= Game1.GameTime.ElapsedGameTime;
                _ally.Color = new Color(_ally.Color.R - 3, _ally.Color.G, _ally.Color.B - 3);
                _isHealing = true;
                if (timeout.Update()) //_healTime < TimeSpan.Zero
                {
                    _ally.Heal();
                    Audio.PlaySoldierHealed();
                    _ally = null;
                }
            }
           
        }

        private void ManageRotation()
        {
            if (Velocity.X == 0 && Velocity.Y == 0)
            {
                _rotation = MathHelper.ToRadians(-90);
            }
            else
            {
                _rotation = Rotation.TurnToFace(Position, Position + Velocity, _rotation, 0.4f);
            }
        }

        #endregion

        #region Collisions

        private void CheckEnemyCollision()
        {
            int enemiesCollided = 0;

            foreach (var enemy in GameManager.Enemies)
            {
                if (CollisionDetection2D.BoundingCircle(this.Position, this.Radius, enemy.Position,
                                                                    enemy.Radius))
                {
                    _health -= 0.5f;
                    Color = Color.Red;
                    enemiesCollided++;
                }
            }
            if (enemiesCollided == 0)
            {
                Color = Color.White;
            }
        }

        private void ProcessCollisions()
        {
            if (CheckWallCollision())
            {
                if (GameManager.IsPlayerHuman)
                {
                    ProcessWallCollision();
                    Velocity = Vector2.Zero;
                }
                else
                {
                    ProcessWallCollision();
                    Velocity *= -1;
                }
            }

            CheckEnemyCollision();

            if (CheckAllyCollison())
            {
                if (_ally.AllyState == AllyState.Wounded)
                {
                    CheckHeal();
                }
                else
                {
                    CheckAllyBump();
                }
            }
            else
            {
                MakeSoldiersWhite();
                timeout.RestartTimer();
                _ally = null;
                _isHealing = false;
            }
        }

        private void CheckAllyBump()
        {
            _ally.Position += this.Velocity;
            _ally = null;
        }

        private bool CheckAllyCollison()
        {
            foreach (var ally in GameManager.Allies)
            {
                if (
                    CollisionDetection2D.BoundingCircle(this.Position, this.Radius, ally.Position,
                                                                    ally.Radius) && ally.AllyState == AllyState.Wounded)
                {
                    _ally = ally;
                    return true;
                }
                if (
                    CollisionDetection2D.BoundingCircle(this.Position, this.Radius, ally.Position,
                                                                    ally.Radius) && ally.AllyState == AllyState.Healthy)
                {
                    _ally = ally;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region AI

        private void Stager()
        {
            Vector2 offset = new Vector2();
            offset.X = (float) (Math.Sin(Game1.GameTime.TotalGameTime.TotalSeconds*5)*3);
            offset.Y = (float) (Math.Cos(Game1.GameTime.TotalGameTime.TotalSeconds*2)*3);
            Position += offset;
        }

        private void EvadeEnemies()
        {
            float evadeRange=20f;
            if (_isHealing)
            {
                evadeRange = 1.3f;
            }

            if (_isEvadingEnemies)
            {
                bool isThereEnemyClose = false;

                foreach (var enemy in GameManager.Enemies)
                {
                    if (Vector2.Distance(enemy.Position, this.Position) < this.Radius*evadeRange)
                    {
                        isThereEnemyClose = true;
                    }
                    if (!isThereEnemyClose)
                    {
                        _isEvadingEnemies = false;
                    }
                }
            }
            else
            {
                foreach (var enemy in GameManager.Enemies)
                {
                    if (Vector2.Distance(enemy.Position, this.Position) < this.Radius*4f)
                    {
                        Vector2 direction = enemy.Position - this.Position;
                        direction.Normalize();
                        direction *= -12;
                        Velocity = direction;
                        _isEvadingEnemies = true;
                    }
                }
            }
        }

        private void HealSoldiers()
        {
            float minimumDistance = 2000;
            foreach (var ally in GameManager.Allies)
            {
                if (ally.AllyState == AllyState.Wounded)
                {
                    if (Vector2.Distance(ally.Position, this.Position) < minimumDistance)
                    {
                        _ally = ally;
                    }
                }
            }

            if (_ally != null)
            {
                Vector2 direction = _ally.Position - this.Position;
                direction.Normalize();
                Velocity = direction*8;
            }
        }

        #endregion

        private void MakeSoldiersWhite()
        {
            foreach (var actor in GameManager.Allies)
            {
                if (actor.AllyState == AllyState.Wounded)
                {
                    actor.Color = Color.White;
                }
            }
        }

        private void ShowHealth(SpriteBatch spriteBatch)
        {
            string temp = "";
            if (GameManager.IsDebug)
            {
                temp = timeout.StopWatch.Elapsed + "\n" + timeout.TimeSpan + " " + " DEBUG MODE "+_isHealing;
            }
            spriteBatch.DrawString(_spriteFont,
                                   "Health : " + Math.Round(_health, 0) + "  " + _status + "\n" + temp,
                                   new Vector2(5, 5),
                                   Color.MistyRose);
        }

        private void CheckStatus()
        {
            if (_health <= 25)
            {
                _status = "Critical damage detected !!";
            }
            else if (_health <= 50)
            {
                _status = "Average damage detected";
            }
            else if (_health <= 75)
            {
                _status = "Light damage detected";
            }
            else if (_health <= 100)
            {
                _status = "No significiant damage detected";
            }
        }

        private void ShowEnemiesRemaining(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, "Enemies remaining: " + GameManager.Enemies.Count,
                                   new Vector2(5, Game1.SCREEN_HEIGHT - 58), Color.DarkSlateBlue);
        }

        #endregion
    }
}