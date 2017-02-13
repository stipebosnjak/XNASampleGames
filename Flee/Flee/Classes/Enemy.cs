using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Xna.Helpers;

namespace Flee.Classes
{

    #region Enums

    #endregion

    internal class Enemy : Soldier
    {
        #region Fields
       
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Enemy()
        {
            ColorChange = true;
            //set position
            Position = new Vector2(Random.Next(0, Game1.SCREEN_WIDTH), Random.Next(0, Game1.SCREEN_HEIGHT));
            //set speed
            Speed = Xna.Helpers.Generators.RandomNumber(1, MAX_SPEED);
            //lock-on target/player
            Target = GameManager.Player;
            //initialize time out
            TimeSpan = TimeSpan.FromMilliseconds(ACCELERATE_TIMEOUT);
            Color = Microsoft.Xna.Framework.Color.DarkRed;

            //generate frequency and amplitude
            Ampitude = Xna.Helpers.Generators.RandomNumber(AMPLITUDE_MIN, AMPLITUDE_MAX);
            Frequency = Xna.Helpers.Generators.RandomNumber(FREQUENCY_MIN, FREQUENCY_MAX);
        }

        #endregion

        #region Methods

        #region XNA

        public override void LoadTexture(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(@"Textures\Enemy_1");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            Move();

            ProcessCollisions();

            ChangeColor();

            Accelerate();

            base.Update();
        }

        #endregion

        private void ProcessCollisions()
        {
            if (CheckWallCollision())
            {
                ProcessWallCollision();
                if (!ActorCollision(this, GameManager.Player))
                {
                    //Choose if you want enemy to be killed from a wall hit 
                    // Destroy();
                }
            }
        }

        private void CheckClearPath()
        {
            //foreach (var enemy in GameManager.Enemies)
            //    {
            //        if (enemy != this)
            //        {
            //            //if (Xna.Helpers.CollisionDetection2D.BoundingCircle(enemy1.Position,enemy1.Radius,enemy))
            //            //{

            //            //}
            //            if (ActorCollision(enemy,this))
            //            {
            //                if (enemy.Velocity.Length() > this.Velocity.Length())
            //                {
            //                    float tempSpeed;
            //                    tempSpeed = this.Speed;
            //                    this.Speed += enemy.Speed/2;
            //                    enemy.Speed -= tempSpeed;
            //                }

            //            }

            //        }
            //    }


            foreach (var enemy in GameManager.Enemies)
            {
                if (enemy != this)
                {
                    if (Vector2.Distance(enemy.Position, this.Position) < this.Radius*2.5f)
                    {
                        Vector2 direction = enemy.Position - this.Position;
                        direction.Normalize();
                        direction *= -1;
                        Velocity += direction*2;
                    }
                    //if (Xna.Helpers.CollisionDetection2D.BoundingCircle(enemy.Position, enemy.Radius, expectedPosition, Radius))
                    //{
                    //}
                }
            }
        }

        private void Accelerate()
        {
            
            TimeSpan -= Game1.GameTime.ElapsedGameTime;
            if (TimeSpan < TimeSpan.Zero)
            {
                Speed += ACCELERATE_VALUE;
                TimeSpan = TimeSpan.FromMilliseconds(ACCELERATE_TIMEOUT);
                
               
            }
        }
        
        private void Move()
        {
            Vector2 expectedPosition;
            Hunt(this);
            // expectedPosition = Position + Velocity;

            CheckClearPath();

            Position += Velocity;

            Stager();
        }

        private void ChangeColor()
        {
            if (ColorChange)
            {
                Color = new Color(Color.R-1,Color.G, Color.B,Color.A-1);
                if (Color.A < 1)
                {
                    ColorChange = !ColorChange;
                }
            }
            else
            {
                Color = new Color(Color.R+1, Color.G, Color.B,Color.A+1);
                if (Color.A > 254)
                {
                    ColorChange = !ColorChange;
                }
            }
        }

        #endregion
    }
}