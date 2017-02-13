#region

using System.Xml;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Humanity
{

    #region Enums

    #endregion

    public static class StaticContentLoader
    {
        #region Fields

        public static Texture2D ManTexture2D;
        public static Texture2D WomanTexture2D;
        public static Texture2D Food;
        public static Texture2D Poop;


        public static Texture2D Scene;
        public static SpriteFont Spritefont;
        public static XmlDocument Texts;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public static void LoadContent(ContentManager contentManager)
        {
            // Texts = contentManager.Load<XmlDocument>(@"Resources\Texts");
            ManTexture2D = contentManager.Load<Texture2D>(@"Textures\Mars_symbol");
            WomanTexture2D = contentManager.Load<Texture2D>(@"Textures\Venus_symbol");
            //Character2DContent = contentManager.Load<Texture2D>(@"Textures\Char_Content");
            //Character2DUnHappy = contentManager.Load<Texture2D>(@"Textures\Char_UnHappy");
            //Character2DTounge = contentManager.Load<Texture2D>(@"Textures\Char_Tounge");
            //Scene = contentManager.Load<Texture2D>(@"Textures\Living_space");
            //Spritefont = contentManager.Load<SpriteFont>(@"Textures\Consolas");
            //Poop = contentManager.Load<Texture2D>(@"Textures\poop_resized");
        }

        #endregion
    }
}