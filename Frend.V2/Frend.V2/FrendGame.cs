using System;
using System.Collections.Generic;
using System.Linq;
using Frend.V2.Character;
using Frend.V2.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Xna.Helpers;

using Xna.Helpers._2D;

namespace Frend.V2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FrendGame : Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        public static int ScreenHeight, ScreenWidth;


        public const int SCREEN_WIDTH = 1280, SCREEN_HEIGHT =1024;
      
        private Tamagotchi _tamagotchi;
        private World _world;
        private Texture2D _backGround;
        private Rectangle _backgroundRect;
        private SpriteFont _spriteFont;
        public Vector2 ScreenCenter { get { return new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2); } }
        private List<Tamagotchi> _tamagothcies;
        private Texture2D  _textureTarget;
        private MouseState _oldMousState;
        public FrendGame()
        {
           
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Resolution.Init(ref _graphics);
            Resolution.SetVirtualResolution(SCREEN_WIDTH, SCREEN_HEIGHT);
            Resolution.SetResolution(_graphics.GraphicsDevice.DisplayMode.Width, _graphics.GraphicsDevice.DisplayMode.Height, false);
            
            //if (IsDebug)
            //{
              // _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
              // _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            //}
            //else
            //{
            //    _graphics.IsFullScreen = true;
            //}
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;
            _tamagothcies=new List<Tamagotchi>();
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {    
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backGround = Content.Load<Texture2D>(@"Textures\Brick");
            _backgroundRect=new Rectangle(0,0,SCREEN_WIDTH,SCREEN_HEIGHT);
            StaticContentLoader.LoadContent(Content);
            _spriteFont = Content.Load<SpriteFont>("Consolas");
            _textureTarget = Content.Load<Texture2D>(@"Textures\target");

            _world =new World();
            _world.ImportMap("map.xml",Content);
            _world.GetWalls();

            _tamagotchi =new Tamagotchi(_world,ScreenCenter,StaticContentLoader.Character2D);
            _tamagotchi.Position=new Vector2(500,500);
            CreateTamagotchisFriends(30);
           
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
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            MouseState mouseState=new MouseState();
       
            BaseGameEntity.UpdateAll(gameTime);

           
             mouseState = Mouse.GetState();

            // Get the mouse state relevant for this frame
          

            // Recognize a single click of the left mouse button
            if (_oldMousState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                Food.Spawn(new Vector2(mouseState.X, mouseState.Y));
                
            }
            _oldMousState = mouseState;
            
          
            if (Input.IsKeyPressed(Keys.Space))
            {
               Food.Spawn(new Vector2(500,500));
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Clear
            GraphicsDevice.Clear(Color.Black);
            //Resoulution draw
            Resolution.BeginDraw();

          
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            _spriteBatch.DrawString(_spriteFont,Food.Foods.Count.ToString()+ "  "+_tamagotchi.Hunger + " " +MovingEntity.MovingEntititiesCount ,ScreenCenter,Color.Black);
            foreach (var tamagothcy in _tamagothcies)
            {
                    _spriteBatch.DrawString(_spriteFont, tamagothcy.Brain.SubGoalsReadOnly[0].Note, tamagothcy.Position, Color.Black);
            }
            //Draws background
            _spriteBatch.Draw(_backGround,_backgroundRect,null,Color.White,0f,Vector2.Zero,SpriteEffects.None,1f);
            //Draws all entities
            BaseGameEntity.DrawAll(_spriteBatch);
            //Draws world
            _world.Draw(_spriteBatch);

            foreach (var debugParameter in SteeringBehaviours.DebugParametars)
            {
                if (debugParameter is Vector2)
                {
                    _spriteBatch.Draw(_textureTarget, (Vector2)debugParameter, null, Color.White, 0f, new Vector2(_textureTarget.Width / 2, _textureTarget.Height / 2), 1f, SpriteEffects.None, 0f);
                }

            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void CreateTamagotchisFriends(int numberOfFriends)
        {
            for (int i = 0; i < numberOfFriends; i++)
            {
                _tamagothcies.Add(new Tamagotchi(_world, new Vector2(Generators.RandomNumber(0, SCREEN_WIDTH), Generators.RandomNumber(0, SCREEN_WIDTH)), StaticContentLoader.Character2D)); 
            }
        }
    }
}

