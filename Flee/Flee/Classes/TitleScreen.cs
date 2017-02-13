using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flee.Classes
{

    #region Enums

    #endregion

    internal class TitleScreen
    {
        #region Fields

        private static SpriteFont _spritefont;
        private static Texture2D _startScreen;
        private static Texture2D _backGround;

        private static string _text;
        private static string _statusText;
        private static string _numberText;

        private static KeyboardState _oldState;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        static TitleScreen()
        {
            _numberText = "";
            if (GameManager.IsLeveling)
            {
                _text = "Press Space to start the game ";
            }
            else
            {
                _text = "How much hunters to HUNT you: ";
            }
            
            _statusText = _text + " " + _numberText;
            _oldState = new KeyboardState();
        }

        #endregion

        #region Methods

        public static void ShowStartScreen(ContentManager contentManager)
        {
            if (!GameManager.IsLeveling)
            {
            var newState = Keyboard.GetState();
            foreach (var key in newState.GetPressedKeys())
            {
                if (newState.IsKeyDown(key) && _oldState.IsKeyUp(key))
                {
                    if (key.ToString().Length > 1)
                    {
                        if (char.IsNumber(key.ToString()[1]) )
                        {
                            if (_numberText.Length < 2)
                            {
                                _numberText += key.ToString()[1];
                            }
                        }
                    }
                    if (key == Keys.Back)
                    {
                        // _numberText += "Back?";
                        if (_numberText.Length != 0)
                        {
                            _numberText = _numberText.Remove(_numberText.Length - 1, 1);
                        }
                    }
                    if (key == Keys.Enter)
                    {
                        if (_numberText.Length != 0 && int.Parse(_numberText)>0)
                        {
                            GameManager.CreateHunters(int.Parse(_numberText));
                            GameManager.CreateSoldiers((int.Parse(_numberText)/10) +1);
                            foreach (var enemy in GameManager.Enemies)
                            {
                                enemy.LoadTexture(contentManager);
                            }
                            foreach (var ally in GameManager.Allies)
                            {
                                ally.LoadTexture(contentManager);
                            }
                            GameManager.StartGame();
                        }
                    }
                }
            }

            _oldState = newState;
            _statusText = _text + " " + _numberText;
            }


            else
            {
                _text = "Press space to start the game";
                var newState = Keyboard.GetState();
                if (newState.IsKeyDown(Keys.Space))
                {
                    GameManager.CreateHunters(GameManager.ENEMIES_PER_LEVEL*GameManager.Level);
                    GameManager.CreateSoldiers(((GameManager.ENEMIES_PER_LEVEL * GameManager.Level) /10)+ 1);
                    foreach (var enemy in GameManager.Enemies)
                    {
                        enemy.LoadTexture(contentManager);
                    }
                    foreach (var ally in GameManager.Allies)
                    {
                        ally.LoadTexture(contentManager);
                    }
                    GameManager.StartGame();
                }
            }
        }

        public static void ShowEndScreen()
        {
            _statusText = "Game over!! You managed to stay alive: " +
                          Math.Round(GameManager.Stopwatch.Elapsed.TotalSeconds, 1) +
                          " seconds \n\nPress spacebar for a new game";
            var newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Space))
            {
                GameManager.ResetGame();
                _numberText = "";
            }
        }

        public static void ShowVictoryScreen()
        {
            _statusText = "Good work , you managed to survive, how incredible!!!\nPress spacebar for a new game";
            var newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Space))
            {
                GameManager.ResetGame();
                _numberText = "";
            }
        }
        public static void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backGround, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 1);
        }
        public static void ShowNextLevelScreen(ContentManager contentManager)
        {
            _statusText = "Good work , you managed to survive, how incredible!!!\nPress spacebar for the next level";
            var newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Space))
            {
                GameManager.CreateHunters(GameManager.ENEMIES_PER_LEVEL * GameManager.Level);
                GameManager.CreateSoldiers((GameManager.ENEMIES_PER_LEVEL * GameManager.Level / 10) + 1);
                foreach (var enemy in GameManager.Enemies)
                {
                    enemy.LoadTexture(contentManager);
                }
                foreach (var ally in GameManager.Allies)
                {
                    ally.LoadTexture(contentManager);
                }
                GameManager.StartGame();
            }
        }

        public static void Draw(SpriteBatch spriteBatch, Color screenColor, Color textColor,Vector2 textPosition)
        {
            spriteBatch.Draw(_startScreen, Vector2.Zero, null, screenColor,0f,Vector2.Zero,1f,SpriteEffects.FlipHorizontally,1);
            spriteBatch.DrawString(_spritefont, _statusText, textPosition, textColor,0f,Vector2.Zero,1f,SpriteEffects.None,0);
        }

        public static void LoadTextures(ContentManager contentManager)
        {
            _startScreen = contentManager.Load<Texture2D>(@"StartScreen");
            _spritefont = contentManager.Load<SpriteFont>(@"SpriteFont1");
            _backGround = contentManager.Load<Texture2D>(@"IceBackGround");
        }

        #endregion
    }
}