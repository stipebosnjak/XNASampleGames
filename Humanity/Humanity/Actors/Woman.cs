using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Humanity.Actors
{
    class Woman:Human
    {
        public Woman(Vector2 startingPosition, Texture2D texture2D) : base(startingPosition, texture2D)
        {
            Position = startingPosition;
            Texture = StaticContentLoader.WomanTexture2D;
        }
    }
}
