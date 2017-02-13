#region

using System;

using Frend.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Frend
{
    /// <summary>
    ///   This is the main type for your game
    /// </summary>
    public class FrendGame : Game
    {
        public const int SCREEN_WIDTH = 1024;
        public const int SCREEN_HEIGHT = 768;
        public static GameTime GameTime;
     
        public static float Fps=30;
        private GraphicsDeviceManager _graphics;
        private Character _mainChar;

        private SpriteBatch _spriteBatch;

    

        public FrendGame()
        {
            IsFixedTimeStep = true;
            
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        ///   Allows the game to perform any initialization it needs to before starting to run.
        ///   This is where it can query for any required services and load any non-graphic
        ///   related content.  Calling base.Initialize will enumerate through any components
        ///   and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        ///   LoadContent will be called once per game and is the place to load
        ///   all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StaticContentLoader.LoadContent(Content);
            SceneManager.Initialize();
            SceneManager.CurrentScene = SceneManager.FirstScene;
            _mainChar = new Character();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        ///   UnloadContent will be called once per game and is the place to unload
        ///   all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///   Allows the game to run logic such as updating the world,
        ///   checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name = "gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / Fps);
            GameTime = gameTime;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            // TODO: Add your update logic here
            Global.UpdateAll();
            base.Update(gameTime);
        }

        /// <summary>
        ///   This is called when the game should draw itself.
        /// </summary>
        /// <param name = "gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Global.DrawAll(_spriteBatch);
            SceneManager.Draw(_spriteBatch);

            _spriteBatch.End();
            // TODO: Add your drawing code here);

            base.Draw(gameTime);
        }

        public static void SetFps(float fps)
        {
            Fps = fps;
        }
    }
}