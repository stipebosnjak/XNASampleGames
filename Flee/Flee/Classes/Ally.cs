using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flee.Classes
{

    #region Enums

    internal enum AllyState
    {
        Wounded,Healthy
    }
    #endregion

    class Ally:Soldier
    {
        #region Fields

        private AllyState _allyState;
        private int _enemiesToKill;
        #endregion

        #region Properties

        public AllyState AllyState
        {
            get { return _allyState; }
            set { _allyState = value; }
        }

        #endregion

        #region Constructors
        public Ally()
        {
            _enemiesToKill = 10;
            _allyState = AllyState.Wounded;
            Color = Color.White;
            Position = new Vector2(Random.Next(0, Game1.SCREEN_WIDTH), Random.Next(0, Game1.SCREEN_HEIGHT));
            TimeSpan = TimeSpan.FromMilliseconds(ACCELERATE_TIMEOUT);

            //generate frequency and amplitude
            Ampitude = Xna.Helpers.Generators.RandomNumber(AMPLITUDE_MIN, AMPLITUDE_MAX);
            Frequency = Xna.Helpers.Generators.RandomNumber(FREQUENCY_MIN, FREQUENCY_MAX);
            Speed = Xna.Helpers.Generators.RandomNumber(1, MAX_SPEED);
        }       
        
        #endregion

        #region Methods
       
        public override void LoadTexture(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(@"Textures\Enemy");
        }
        public override void Update()
        {
            if (_allyState==AllyState.Healthy)
            {
                Move();

                CheckEnemiesToKill();
            }


            base.Update();
        }

        public void Heal()
        {
             Color = Color.LightGreen;
            _allyState = AllyState.Healthy;
        }

        private void Move()
        {
            FindNearestEnemy();

            Hunt(this);

            CheckCollision();

            Position += Velocity;
            
            Stager();
        }

        private void CheckCollision()
        {
            if (ActorCollision(this,Target))
            {
                ((Enemy)Target).Destroy();
                Audio.PlayEnemyKilled();
                _enemiesToKill--;
            }
        }

        private void CheckEnemiesToKill()
        {
            if (_enemiesToKill<1)
            {
                this.Destroy();
            }

        }

        private void FindNearestEnemy()
        {
            float minDistance = Game1.SCREEN_WIDTH;
            Actor target=null;

            foreach (var enemy in GameManager.Enemies)
            {
                if (Vector2.Distance(enemy.Position, this.Position) < minDistance)
                {
                    minDistance = Vector2.Distance(enemy.Position, this.Position);
                    target = enemy;
                }
            }
            Target = target;
        }
        #endregion
    }
}