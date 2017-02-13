using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Xna.Helpers;
using Xna.Helpers._2D;

namespace Frend.V2.Items
{
    public class Food : BaseGameEntity
    {
        private static TimeOut _respawnTimer;
        private static List<Food> _foods;

        public Food(Vector2 position)
        {
            Texture = StaticContentLoader.Food;
            Position = position;
            Color = Microsoft.Xna.Framework.Color.White;
            _foods.Add(this);
            _respawnTimer =new TimeOut(5000);
        }

        static Food()
        {
            _foods=new List<Food>();
        }

        public static ReadOnlyCollection<Food> Foods
        {
            get
            {
                List<Food> foods=new List<Food>();
                foreach (var food in BaseGameEntities)
                {
                    if (food is Food)
                    {
                        foods.Add((Food) food);
                    }
                }
                return foods.AsReadOnly();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_respawnTimer.Update(false))
            {
                Remove();
            }
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Position,null,Color.White,0f,Origin,1f,SpriteEffects.None,0);
            base.Draw(spriteBatch);
        }

       public static void Spawn(Vector2 position)
       {
           new Food(position);
       }

        public void Remove()
        {
            BaseGameEntities.Remove(this);
            _foods.Remove(this);
        }
     }
}
