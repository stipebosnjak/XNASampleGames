using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humanity.Goap.Goals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xna.Helpers;
using Xna.Helpers.Goap;
using Xna.Helpers._2D;


namespace Humanity.Actors
{
    public abstract class Human : MovingEntity
     {
        private Goap.Goals.GoalThink _brain;
        private float _hunger;
        private float _age;


        private TimeOut _tooHungry;
       // public List<Food> _cantReachFood;
        public TextHelper TextHelper { get; set; }

        public GoalThink Brain
        {
            get { return _brain; }
        }

        public float Hunger
        {
            get { return _hunger; }
            set { _hunger = value; }
        }

        public bool IsHungry { get; set; }
     
        public Human(Vector2 startingPosition, Texture2D texture2D)
        {
            Position = startingPosition;
            Color = Color.White;
            _hunger =0f;
            Texture = texture2D;
            Scale = 1f;
            Mass = 3f;
            MaxSpeed=Generators.RandomNumber(1f,4f);
            MaxForce = 10f;
            Rotation = 0f;
            Heading = Generators.RandomVector2(-1f, 1f);
            SteeringBehaviour.WanderDistance = 1.2f;
            SteeringBehaviour.WanderRadius = 0.8f;
            SteeringBehaviour.WanderJitter = 1;

            SteeringBehaviour.WallColisionOn();

           SteeringBehaviour.SeparationOn();
            SteeringBehaviour.CohesionOn();
           SteeringBehaviour.AlignmentOn();
           //SteeringBehaviour.WanderOn();

            SteeringBehaviour.WallDetectionLenght = 100f;
            SteeringBehaviour.WallWeightCollision = 0.1f;
            SteeringBehaviour.SeparationWeight = 20f;
            SteeringBehaviour.CohesionWeight = 0.1f;
            SteeringBehaviour.AlignmentWeight =2f;
            SteeringBehaviour.ViewDistance = 150;
           // _cantReachFood=new List<Food>();
             _tooHungry=new TimeOut(6000);
             _brain = new GoalThink(this, Goal<Human>.GoalType.Composite);
        }

        public override void Update(GameTime gameTime)
        {
            Brain.Process();

            Brain.Arbitrate();

            UpdateMovement();

            ManageHunger();
            
            //this.EnforceNonPenetrationConstraint();

            if (_tooHungry.Update())
            {
             //   _cantReachFood.Clear();
            }
            base.Update(gameTime);
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Position,null,Color,Rotation,Origin,Scale,SpriteEffects.None,0f);
            base.Draw(spriteBatch);
        }


        private void UpdateMovement()
        {
            Vector2 steeringForce = SteeringBehaviour.Calculate();
            CurrentForce = steeringForce;
            Vector2 acceleration = steeringForce / Mass;

            Velocity += acceleration;//orig * gametime;
            if (acceleration==Vector2.Zero)
            {
                Velocity = Vector2.Zero;
            }

            float velLenght = Velocity.Length();
            if (velLenght > MaxSpeed)
            {
                Velocity = Vector2.Normalize(Velocity);
                Velocity *= MaxSpeed;
            }

            Position += Velocity;//orig * gametime

            if (Velocity.Length() > 0.0000001f)
            {
                Heading = Vector2.Normalize(Velocity);
            }

        }


        private void ManageHunger()
        {
            _hunger += 0.00005f;
            MathHelper.Clamp(_hunger, 0, 1f);
        }

       

    }
}
