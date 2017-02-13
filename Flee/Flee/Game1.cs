using System;
using System.Collections.Generic;
using System.Linq;
using Flee.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Xna.Helpers;
using Camera2D = Flee.Classes.Camera2D;
using Xna.Helpers._2D;

namespace Flee
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public const int SCREEN_WIDTH = 1366;
        public const int SCREEN_HEIGHT = 768;

        public static GameTime GameTime;

        private AnimatedTexture anitex;

        public Camera2D cam = new Camera2D();
      

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
                            {
                            //    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width
                                PreferredBackBufferHeight = SCREEN_HEIGHT,
                                PreferredBackBufferWidth = SCREEN_WIDTH
                                
                            };
            _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //TargetElapsedTime=new TimeSpan(0,0,0,0,600);
            //IsFixedTimeStep = true;
            anitex=new AnimatedTexture(Vector2.Zero,0,1,0.5f);
            cam.Pos = new Vector2(SCREEN_WIDTH/2,SCREEN_HEIGHT/2);
            _player = new Player();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            TitleScreen.LoadTextures(Content);
            anitex.Load(Content, @"Textures\DinosaurStrip", 5, 10);
           // anitex.Play();
            foreach (var actor in Actor.Actors)
            {
                actor.LoadTexture(Content);
            }
            Audio.LoadSounds(Content);
            Audio.PlaySong();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (cam.SelfUpdating)
            {
                cam.Pos = GameManager.Player.Position;
            }
            else
            {
                cam.Pos = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
            }
            cam.CheckInput();

        
            GameTime = gameTime;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            switch (GameManager.GameState)
            {
                case GameState.StartScreen:
                    TitleScreen.ShowStartScreen(Content);
                    break;
                case GameState.Playing:
                    GameManager.UpdateActors();
                    anitex.UpdateFrame(elapsed);
                    break;

                case GameState.GameOver:
                    TitleScreen.ShowEndScreen();
                    break;
                case GameState.Victory:
                    TitleScreen.ShowVictoryScreen();
                    break;
                case GameState.NextLevel:
                    TitleScreen.ShowNextLevelScreen(Content);
                   break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.BackToFront,
                               BlendState.AlphaBlend,
                               null,
                               null,
                               null,
                               null,
                               cam.GetTransformation(GraphicsDevice));

            switch (GameManager.GameState)
            {
                case GameState.StartScreen:
                    TitleScreen.Draw(_spriteBatch, Color.White, Color.Snow, new Vector2(0, 2));
                    break;
                case GameState.Playing:
                    //DINOSAUR
                     // anitex.DrawFrame(_spriteBatch,new Vector2(100,100));
                   TitleScreen.DrawBackground(_spriteBatch);
                   GameManager.DrawActors(_spriteBatch);
                    break;
                case GameState.GameOver:
                    TitleScreen.Draw(_spriteBatch, Color.DarkRed, Color.BlueViolet,new Vector2(GameManager.ScreenCenter.X/4,GameManager.ScreenCenter.Y));
                    break;
                case GameState.Victory:
                    TitleScreen.Draw(_spriteBatch, Color.DarkOliveGreen, Color.YellowGreen, new Vector2(GameManager.ScreenCenter.X / 4, GameManager.ScreenCenter.Y));
                    break;
                case GameState.NextLevel:
                    TitleScreen.Draw(_spriteBatch, Color.WhiteSmoke, Color.YellowGreen, new Vector2(GameManager.ScreenCenter.X / 4, GameManager.ScreenCenter.Y));
                    break;

            }
          
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}