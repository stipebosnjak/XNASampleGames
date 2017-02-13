using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frend.Classes
{
    public class Global
    {
        private static List<Global> _gameItems;

        static Global()
        {
            _gameItems=new List<Global>();
        }

        public Global()
        {
            _gameItems.Add(this);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void Update()
        {

        } 
        public static void DrawAll(SpriteBatch spriteBatch)
        {
            for (int i = _gameItems.Count - 1; i >= 0; i--)
            {
                _gameItems[i].Draw(spriteBatch);
            }
        }
        public static void UpdateAll()
        {
            for (int i = _gameItems.Count - 1; i >= 0; i--)
            {
              _gameItems[i].Update();  
            }
        }

    }
}
