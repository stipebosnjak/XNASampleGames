using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Xna.Helpers;
using Xna.Helpers.AI;
using Xna.Helpers.Characters;

namespace Goap.Test
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {   public const int SCREEN_WIDTH1 = 1366;
        public const int SCREEN_HEIGHT2 = 768;

        public static int SCREEN_WIDTH;
        public static int SCREEN_HEIGHT;


        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private Texture2D _texture,_textureTarget;
      //  Xna.Helpers.Goap.Goals.GoalThink
        private Tamagotchi _tamagotchi;
      //  private Input _input;
        private SpriteFont _spriteFont;
        public static string Text="";
        public static Vector2 Origin,TargetPosition;
        public static World WorldGotchi;
        public static Vector2 CenterOfScreen
        {
            get {return new Vector2(SCREEN_WIDTH/2,SCREEN_HEIGHT/2);}
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
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
           
            SCREEN_HEIGHT = GraphicsDevice.Viewport.Height;
            SCREEN_WIDTH = GraphicsDevice.Viewport.Width;
            WorldGotchi = new World();
            Wall wallUp = new Wall(new Vector2(0, 0), new Vector2(Game1.SCREEN_WIDTH, 0));
            Wall wallDown = new Wall(new Vector2(Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), new Vector2(0, Game1.SCREEN_HEIGHT));
            Wall wallLeft = new Wall(new Vector2(0, Game1.SCREEN_HEIGHT), new Vector2(0, 0));
            Wall wallRight = new Wall(new Vector2(Game1.SCREEN_WIDTH, 0), new Vector2(Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT));
            WorldGotchi.Walls.Add(wallUp);
            WorldGotchi.Walls.Add(wallDown);
            WorldGotchi.Walls.Add(wallLeft);
            WorldGotchi.Walls.Add(wallRight);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {  
            StaticContentLoader.LoadContent(Content);

          
          // _input = new Input(_tamagotchi);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            _textureTarget = Content.Load<Texture2D>("target");
            _tamagotchi = new Tamagotchi(WorldGotchi,CenterOfScreen,StaticContentLoader.Character2DTounge);
            _tamagotchi.Color = Color.Black;
            _tamagotchi.SteeringBehaviour.WallColisionOn();
          //  CreateTamagotchisFriends(50);
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            //_input.CheckInput();
            //Text = _input.Text;

            BaseGameEntity.UpdateAll(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlueViolet);
            
            _spriteBatch.Begin();
            BaseGameEntity.DrawAll(_spriteBatch);

            foreach (var debugParameter in SteeringBehaviours.DebugParameters)
            {
                if (debugParameter is Vector2)
                {
                    _spriteBatch.Draw(_textureTarget, (Vector2)debugParameter, null, Color.White, 0f, new Vector2(_textureTarget.Width / 2, _textureTarget.Height / 2), 0.5f, SpriteEffects.None, 0f);
                }
           
            }
           
            _spriteBatch.DrawString(_spriteFont,Text,new Vector2(5,0),Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void CreateTamagotchisFriends(int numberOfFriends)
        {
            for (int i = 0; i < numberOfFriends; i++)
            {
               // new Tamagotchi();
            }
        }
    }
}
