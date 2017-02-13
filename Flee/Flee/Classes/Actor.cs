using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Xna.Helpers;
using Xna.Helpers._2D;

namespace Flee.Classes
{

    #region Enums

    #endregion

    internal class Actor
    {
        #region Fields

        private static List<Actor> _actors;

        
        private Vector2 _position;
        private Vector2 _velocity;
        private Texture2D _texture;
        private Color _color;

        #endregion

        #region Properties
        public static List<Actor> Actors
        {
            get { return _actors; }
            set { _actors = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

       
        public Vector2 Origin
        {
            get
            {return new Vector2(Texture.Width/2,Texture.Height/2);}
        }

        public float Radius
        {
            get { return Math.Max(Texture.Height/2, Texture.Width/2); }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        #endregion

        #region Constructors

        static Actor()
        {
            _actors = new List<Actor>();
        }

        public Actor()
        {
            _actors.Add(this);
        }

        #endregion

        #region Methods

        public virtual void LoadTexture(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>("");
        }

        public virtual void Update()
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null,_color,0f,Origin,1f,SpriteEffects.None,0);
        }
        protected virtual void ProcessWallCollision()
        {
          Position=  CollisionDetection2D.ProcessWallCollision( _position,Texture,Game1.SCREEN_WIDTH,Game1.SCREEN_HEIGHT);
        }

        protected bool CheckWallCollision()
        {
            return CollisionDetection2D.CheckWallCollision(Position, Texture, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT);
        }

        #endregion
    }
}