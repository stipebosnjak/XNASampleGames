using System;
using System.Collections.Generic;
using System.Linq;
using Humanity.Actors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Xna.Helpers;
using Xna.Helpers._2D;

namespace Humanity
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SimulationGame : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        public static int ScreenHeight, ScreenWidth;


        public const int SCREEN_WIDTH = 1280, SCREEN_HEIGHT = 1024;
        private World _world;
        private Texture2D _backGround;
        private Rectangle _backgroundRect;
        private SpriteFont _spriteFont;
        public Vector2 ScreenCenter { get { return new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2); } }
        private List<Human> _humans;
        private Texture2D _textureTarget;
        private MouseState _oldMousState;


        public SimulationGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Resolution.Init(ref _graphics);
            Resolution.SetVirtualResolution(SCREEN_WIDTH, SCREEN_HEIGHT);
            Resolution.SetResolution(_graphics.GraphicsDevice.DisplayMode.Width, _graphics.GraphicsDevice.DisplayMode.Height, false);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;
            _humans = new List<Human>();
          
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
           // _backGround = Content.Load<Texture2D>(@"Textures\Brick");
            _backgroundRect = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);
            StaticContentLoader.LoadContent(Content);
            _spriteFont = Content.Load<SpriteFont>("Consolas");
        

            _world = new World();
            _world.ImportMap("map.xml", Content);
            _world.GetWalls();
            World.CurrentWorld = _world;
            _humans.Add(new Man(new Vector2(500), StaticContentLoader.ManTexture2D));
            //_tamagotchi = new Tamagotchi(_world, ScreenCenter, StaticContentLoader.ManTexture2D);
            //_tamagotchi.Position = new Vector2(500, 500);
            //CreateTamagotchisFriends(50);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            BaseGameEntity.UpdateAll(gameTime);


            MouseState mouseState = Mouse.GetState();

            // Get the mouse state relevant for this frame


            // Recognize a single click of the left mouse button
            if (_oldMousState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                if (Generators.RandomNumber(1,3)==2)
                {
                    _humans.Add(new Man(new Vector2(mouseState.X, mouseState.Y), StaticContentLoader.ManTexture2D));
                }
                else
                {
                    _humans.Add(new Woman(new Vector2(mouseState.X, mouseState.Y), StaticContentLoader.WomanTexture2D));
                }
            

            }
            _oldMousState = mouseState;


            if (Input.IsKeyPressed(Keys.Space)||mouseState.LeftButton==ButtonState.Pressed)
            {
                if (Generators.RandomNumber(1, 3) == 2)
                {
                    _humans.Add(new Man(new Vector2(mouseState.X, mouseState.Y), StaticContentLoader.ManTexture2D));
                }
                else
                {
                    _humans.Add(new Woman(new Vector2(mouseState.X, mouseState.Y), StaticContentLoader.WomanTexture2D));
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {


            
            Resolution.BeginDraw();
            GraphicsDevice.Clear(Color.White); 

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            _spriteBatch.DrawString(_spriteFont," " + MovingEntity.MovingEntititiesCount +"  "+_humans[_humans.Count-1].Position.ToString(), ScreenCenter, Color.Black);
            //foreach (var tamagothcy in _humans)
            //{
            //    _spriteBatch.DrawString(_spriteFont, tamagothcy.Brain.SubGoalsReadOnly[0].Note, tamagothcy.Position, Color.Black);
            //}
            //Draws background
           // _spriteBatch.Draw(_backGround, _backgroundRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            //Draws all entities
            BaseGameEntity.DrawAll(_spriteBatch);
            //Draws world
       //     _world.Draw(_spriteBatch);

            //foreach (var debugParameter in SteeringBehaviours.DebugParametars)
            //{
            //    if (debugParameter is Vector2)
            //    {
            //        _spriteBatch.Draw(_textureTarget, (Vector2)debugParameter, null, Color.White, 0f, new Vector2(_textureTarget.Width / 2, _textureTarget.Height / 2), 1f, SpriteEffects.None, 0f);
            //    }

            //}
           
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
