using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Goap.Test
{
    class Input
    {
        private Tamagotchi _movingEntity;
        public const float FineTuner = 0.09f;
        public Input(Tamagotchi movingEntity)
        {
            _movingEntity = movingEntity;
        }

        public string Text;
        public void CheckInput()
        {
            if (Xna.Helpers.Input.IsKeyPressed(Keys.A))
                _movingEntity.Mass+=FineTuner;
            if (Xna.Helpers.Input.IsKeyPressed(Keys.Y))
                _movingEntity.Mass -= FineTuner;

            if (Xna.Helpers.Input.IsKeyPressed(Keys.S))
                _movingEntity.MaxForce += FineTuner;
            if (Xna.Helpers.Input.IsKeyPressed(Keys.X))
                _movingEntity.MaxForce -= FineTuner;

            if (Xna.Helpers.Input.IsKeyPressed(Keys.D))
                _movingEntity.MaxSpeed += FineTuner;
            if (Xna.Helpers.Input.IsKeyPressed(Keys.C))
                _movingEntity.MaxSpeed -= FineTuner;

            if (Xna.Helpers.Input.IsKeyPressed(Keys.F))
                _movingEntity.SteeringBehaviour.WallDetectionLenght += FineTuner;
            if (Xna.Helpers.Input.IsKeyPressed(Keys.V))
                _movingEntity.SteeringBehaviour.WallDetectionLenght -= FineTuner;

            if (Xna.Helpers.Input.IsKeyPressed(Keys.G))
                _movingEntity.SteeringBehaviour.WallWeightCollision += FineTuner;
            if (Xna.Helpers.Input.IsKeyPressed(Keys.B))
                _movingEntity.SteeringBehaviour.WallWeightCollision -= FineTuner;

            if (Xna.Helpers.Input.IsKeyPressed(Keys.H))
                _movingEntity.SteeringBehaviour.WanderDistance += FineTuner;
            if (Xna.Helpers.Input.IsKeyPressed(Keys.N))
                _movingEntity.SteeringBehaviour.WanderDistance -= FineTuner;

            if (Xna.Helpers.Input.IsKeyPressed(Keys.J))
                _movingEntity.SteeringBehaviour.WanderRadius += FineTuner;
            if (Xna.Helpers.Input.IsKeyPressed(Keys.M))
                _movingEntity.SteeringBehaviour.WanderRadius -= FineTuner;

            if (Xna.Helpers.Input.IsKeyPressed(Keys.R))
                _movingEntity.Position = Game1.CenterOfScreen;

            StringBuilder stringBuilder=new StringBuilder();
            stringBuilder.Append("Mass: " + _movingEntity.Mass+Environment.NewLine);
            stringBuilder.Append("MaxForce: " + _movingEntity.MaxForce + Environment.NewLine);
            stringBuilder.Append("MaxSpeed: " + _movingEntity.MaxSpeed + Environment.NewLine);
            stringBuilder.Append("SteeringBehaviours.WallDetectionLenght: " + _movingEntity.SteeringBehaviour.WallDetectionLenght + Environment.NewLine);
            stringBuilder.Append("SteeringBehaviours.WallWeightCollision: " + _movingEntity.SteeringBehaviour.WallWeightCollision + Environment.NewLine);
            stringBuilder.Append("SteeringBehaviours.WanderDistance: " + _movingEntity.SteeringBehaviour.WanderDistance + Environment.NewLine);
            stringBuilder.Append("SteeringBehaviours.WanderRadius: " + _movingEntity.SteeringBehaviour.WanderRadius + Environment.NewLine);
            Text = stringBuilder.ToString();
        }
    }
}
