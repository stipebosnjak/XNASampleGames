using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Xna.Helpers._2D;

namespace Humanity.Actors
{
    class Man:Human
    {
        public Man(Vector2 startingPosition, Texture2D texture2D) : base( startingPosition, texture2D)
        {
            Position = startingPosition;
            Color = Color.White;
            Texture = StaticContentLoader.ManTexture2D;
        }
    }
}
