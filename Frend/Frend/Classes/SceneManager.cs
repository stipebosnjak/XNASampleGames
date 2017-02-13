#region

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Frend.Classes
{

    #region Enums

    #endregion

    public static class SceneManager
    {
        #region Fields

        

        #region Scenes

        private static Scene _firstScene;

        #endregion

        #endregion

        #region Properties

        public static Vector2 Center
     {
         get { return new Vector2(FrendGame.SCREEN_WIDTH / 2, FrendGame.SCREEN_HEIGHT / 2); }
    }

        public static Scene FirstScene
        {
            get { return _firstScene; }
            set { _firstScene = value; }
        }

        public static Scene CurrentScene { get; set; }

        public static List<Scene> Scenes { get; set; }

        #endregion

        #region Constructors

        static SceneManager()
        {
            Scenes = new List<Scene>();
            _firstScene = new Scene();
        }

        #endregion

        #region Methods

        public static void Initialize()
        {
            InitializeFirstScene();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentScene.Texture, new Rectangle(0, 0, FrendGame.SCREEN_WIDTH, FrendGame.SCREEN_HEIGHT),
                             null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            spriteBatch.DrawString(CurrentScene.UpperLeftFont,CurrentScene.UpperLeftText,new Vector2(40,30),Color.Chocolate);
        }


        private static void InitializeFirstScene()
        {
            _firstScene.Texture = StaticContentLoader.Scene;

            var kitchen = new PointOfInterest("Kitchen", new Vector2(FrendGame.SCREEN_WIDTH/4,FrendGame.SCREEN_HEIGHT/4));
            var livingRoom = new PointOfInterest("Living room",new Vector2(FrendGame.SCREEN_WIDTH/1.33f,FrendGame.SCREEN_HEIGHT/4));
            var bedroom = new PointOfInterest("Bedroom",new Vector2(FrendGame.SCREEN_WIDTH/4,FrendGame.SCREEN_HEIGHT/1.33f));
            var bathroom = new PointOfInterest("Bathroom",new Vector2(FrendGame.SCREEN_WIDTH/1.33f,FrendGame.SCREEN_HEIGHT/1.33f));

            _firstScene.PointsOfInterest.Add(kitchen.Name, kitchen);
            _firstScene.PointsOfInterest.Add(livingRoom.Name, livingRoom);
            _firstScene.PointsOfInterest.Add(bedroom.Name, bedroom);
            _firstScene.PointsOfInterest.Add(bathroom.Name, bathroom);
            Scenes.Add(_firstScene);
        }

        #endregion
    }
}