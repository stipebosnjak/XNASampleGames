#region

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Frend.Classes
{

    #region Enums

    #endregion

    public class GameItem : Global
    {
        #region Fields

        #endregion

        #region Properties

        protected Vector2 Origin
        {
            get { return new Vector2(Texture.Width / 2, Texture.Height / 2); }
        }

       protected Texture2D Texture { get; set; }

        public Vector2 Position { get; protected set; }

        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion
    }
}