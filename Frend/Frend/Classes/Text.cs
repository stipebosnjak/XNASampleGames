#region

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Frend.Classes
{

    #region Enums

    #endregion

    internal class SpriteFontText : Global
    {
        #region Fields

        private Color _color;
        private Vector2 _position;
        private SpriteFont _spriteFont;
        private string _text;

        #endregion

        #region Properties

        public SpriteFont SpriteFont
        {
            get { return _spriteFont; }
            set { _spriteFont = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Origin
        {
            get
            {
                Vector2 vector2 = _spriteFont.MeasureString(Text)/2;
                return vector2;
            }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        #endregion

        #region Constructors

        public SpriteFontText(Color color)
        {
            _color = color;
            _spriteFont = StaticContentLoader.Spritefont;
            _text = "";
        }

        #endregion

        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _text, _position, _color, 0f, Origin, 1f, SpriteEffects.None, 0.1f);
        }

        #endregion
    }
}