using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AutoLaserCannon
{

    #region Enums

    #endregion

    class GameItem
    {
        #region Fields

        private Vector2 _position;

        private static List<GameItem> _gameItems;
        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public static List<GameItem> GameItems
        {
            get { return _gameItems; }
            set { _gameItems = value; }
        }

        #endregion

        #region Constructors
        public GameItem()
        {
            _gameItems.Add(this);
        }
        static GameItem()
        {
            _gameItems=new List<GameItem>();
        }
        #endregion

        #region Methods
        protected virtual void Draw(SpriteBatch spriteBatch)
        {
            

        }
        protected virtual void Update(GameTime gameTime)
        {


        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var gameItem in _gameItems)
            {
                gameItem.Draw(spriteBatch);
            }

        }
        public static void UpdateAll(GameTime gameTime)
        {
            foreach (var gameItem in _gameItems)
            {
                gameItem.Update(gameTime);
            }

        }
        #endregion
    }
}