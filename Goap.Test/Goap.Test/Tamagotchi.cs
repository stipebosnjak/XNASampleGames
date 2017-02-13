using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xna.Helpers;
using Xna.Helpers.AI;

namespace Goap.Test
{
    class Tamagotchi : MovingEntity
    {
       
        


        public Tamagotchi()
        {
            World = Game1.WorldGotchi;
            Position = new Vector2(Generators.RandomNumber(0,Game1.SCREEN_WIDTH),Generators.RandomNumber(0,Game1.SCREEN_HEIGHT));
            Color = Color.Violet;
            SteeringBehaviour.WallDetectionLenght = 100f;
            SteeringBehaviour.WallWeightCollision = 10f;
            SteeringBehaviour.WanderDistance = 3.5f;
            SteeringBehaviour.WanderRadius = 3f;
            SteeringBehaviour.WanderJitter = 1;
            SteeringBehaviour.WallColisionOn();
            SteeringBehaviour.WanderOn();

            Heading = Generators.RandomVector2(-1f, 1f);
            Texture = StaticContentLoader.Character2DTounge;
            Scale = 1f;
            Mass = 3f;
            MaxSpeed=3f;
            MaxForce = 2f;
            Rotation = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            
            Vector2 steeringForce = SteeringBehaviour.Calculate();
            CurrentForce = steeringForce;
            Vector2 acceleration = steeringForce / Mass;
           
            Velocity += acceleration;//orig * gametime;

            
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

            //Position = CollisionDetection2D.ProcessWallCollision(Position, Texture, Game1.SCREEN_WIDTH,
            //                                                     Game1.SCREEN_HEIGHT);


            this.EnforceNonPenetrationConstraint();
            base.Update(gameTime);
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Position,null,Color,Rotation,Origin,Scale,SpriteEffects.None,0f);
            base.Draw(spriteBatch);
        }

    }
}
