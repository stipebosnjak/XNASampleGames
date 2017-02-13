using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xna.Helpers;
namespace Frend.Classes
{
    public class Scene
    {
        private Texture2D _texture;
        private Collections.DictionaryV2<string ,PointOfInterest> _pointsOfInterest;
        private SpriteFont _upperLeftFont;
        private string _upperLeftText;
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Collections.DictionaryV2<string, PointOfInterest> PointsOfInterest
        {
            get { return _pointsOfInterest; }
            set { _pointsOfInterest = value; }
        }

        public SpriteFont UpperLeftFont
        {
            get { return _upperLeftFont; }
            
        }

        public string UpperLeftText
        {
            get { return _upperLeftText; }
            set { _upperLeftText = value; }
        }

        public Scene()
        {
            Texture = StaticContentLoader.Scene;
            _pointsOfInterest=new Collections.DictionaryV2<string, PointOfInterest>();
            _upperLeftFont = StaticContentLoader.Spritefont;
            _upperLeftText = "";
        }

       
    }
}
