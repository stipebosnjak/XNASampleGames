using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AutoLaserCannon
{

    #region Enums

    #endregion

    class Textures
    {
        #region Fields

        private static Texture2D _gunTexture;

        #endregion

        #region Properties

        public static Texture2D GunTexture
        {
            get { return _gunTexture; }
            set { _gunTexture = value; }
        }

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public static void LoadTextures(ContentManager contentManager)
        {
            _gunTexture = contentManager.Load<Texture2D>("LaserCannon_full");
        }


        #endregion
    }
}