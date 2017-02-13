using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutoLaserCannon
{

    #region Enums

    #endregion

    class LaserCannon :GameItem
    {
        #region Fields

        private Rectangle _srGun;
        private Rectangle _srStation;
        private float _rotation;

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LaserCannon()
        {
            Position=new Vector2(Game1.SCREEN_WIDTH/2,Game1.SCREEN_HEIGHT/2);
            _srGun=new Rectangle(0,0,45,13);
            _srStation=new Rectangle(0,17,45,32);
        }
        #endregion

        #region Methods
        protected override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.GunTexture,Position,_srGun,Color.White,_rotation,new Vector2(0,_srGun.Height/2),1f,SpriteEffects.None,0);
            spriteBatch.Draw(Textures.GunTexture, new Vector2(Position.X-22,Position.Y), _srStation, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
        protected override void Update(GameTime gameTime)
        {
        }
        #endregion
    }
}