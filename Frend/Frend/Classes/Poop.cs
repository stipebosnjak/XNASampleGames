using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frend.Classes
{

    #region Enums

    #endregion

    class Poop:GameItem
    {
        #region Fields

        private static List<Poop> _poops;
        private float _scale;
        private bool _beingCleaned;
      
        #endregion

        #region Properties

        public static List<Poop> Poops
        {
            get { return _poops; }
            set { _poops = value; }
        }

        
        #endregion

        #region Constructors
        static Poop()
        {
            _poops=new List<Poop>();
        }

        public Poop(Vector2 position)
        {
            Position = position;
            Texture = StaticContentLoader.Poop;
            _scale = 1f;
            _poops.Add(this);
           
        }
        #endregion

        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,Position, null,Color.White,0f,Origin,_scale,SpriteEffects.None,0.2f);
        }

        public override void Update()
        {
            if (_beingCleaned)
            {
                _scale -= 0.01f;
            }
            if (_scale<0.1f)
            {
                _poops.Remove(this);
            }
        }

        public void Clean()
        {
            _beingCleaned = true;
        }

        #endregion
    }
}