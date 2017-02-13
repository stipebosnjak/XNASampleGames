using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flee.Classes
{

    #region Enums

    internal enum GameState
    {
        Playing,
        StartScreen,
        GameOver,
        Victory,
        NextLevel
    }

    #endregion

    //todo: 
    internal class GameManager
    {

        public const int ENEMIES_PER_LEVEL = 5;

        public static bool IsLeveling = true;
        public static bool IsDebug =false;
        public static bool IsPlayerHuman = false;


        #region Fields

        private static int _level;

        private static GameState _gameState;
        private static Random _random;
        private static Stopwatch _stopwatch;

        #endregion

        #region Properties
        
        public static int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public static Vector2 ScreenCenter
        {
            get
            {
                return new Vector2(Game1.SCREEN_WIDTH/2, Game1.SCREEN_HEIGHT/2);
            }
        }

        public static List<Enemy> Enemies
        {
            get
            {
                List<Enemy> listToReturn = new List<Enemy>();
                foreach (Actor actor in Actor.Actors)
                {
                    if (actor is Enemy)
                    {
                        listToReturn.Add((Enemy) actor);
                    }
                }
                return listToReturn;
            }
        }

        public static List<Ally> Allies
        {
            get
            {
                List<Ally> listToReturn = new List<Ally>();
                foreach (Actor actor in Actor.Actors)
                {
                    if (actor is Ally)
                    {
                        listToReturn.Add((Ally) actor);
                    }
                }
                return listToReturn;
            }
        }

        public static Player Player
        {
            get
            {
                foreach (var actor in Actor.Actors)
                {
                    if (actor is Player)
                    {
                        return (Player) actor;
                    }
                }
                return null;
            }
        }

        public static GameState GameState
        {
            get { return _gameState; }
            set { _gameState = value; }
        }

        public static Stopwatch Stopwatch
        {
            get { return _stopwatch; }
            set { _stopwatch = value; }
        }

        #endregion

        #region Constructors

        static GameManager()
        {
            _gameState = GameState.StartScreen;
            _level = 1;
            _random = new Random();
            _stopwatch = new Stopwatch();
        }

        #endregion

        #region Methods

        #region Game Mechanics

        public static void UpdateActors()
        {
            for (int index = 0; index < Actor.Actors.Count; index++)
            {
                var actor = Actor.Actors[index];
                actor.Update();
            }
            CheckGameRules();
        }

        public static void DrawActors(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < Actor.Actors.Count; index++)
            {
                var actor = Actor.Actors[index];
                actor.Draw(spriteBatch);
            }
        }

        #endregion

        private static void CheckGameRules()
        {
            if (Player.Health <= 0)
            {
                _gameState = GameState.GameOver;
                _level = 1;
                _stopwatch.Stop();
            }

            if (Enemies.Count == 0)
            {
                if (IsLeveling)
                {
                    _level++;
                    _gameState = GameState.NextLevel;
                    GameManager.ResetGame();
                }
                else
                {
                    _gameState = GameState.Victory;
                    
                }
                
                _stopwatch.Stop();
            }
        }

        private static void ClearEnemies()
        {
            for (int i = Actor.Actors.Count - 1; i >= 0; i--)
            {
                Actor actor = Actor.Actors[i];
                if (actor is Enemy)
                {
                    Actor.Actors.Remove(actor);
                }
            }
        }

        private static void ClearAllies()
        {
            for (int i = Actor.Actors.Count - 1; i >= 0; i--)
            {
                Actor actor = Actor.Actors[i];
                if (actor is Ally)
                {
                    Actor.Actors.Remove(actor);
                }
            }
        }

        public static void ResetGame()
        {
            Player.Health = 100;
            if (IsDebug)
            {
                Player.Health = 50000;
            }
            
            Player.Position=new Vector2(Game1.SCREEN_WIDTH/2,Game1.SCREEN_HEIGHT/2);
            ClearEnemies();
            ClearAllies();
            if (IsLeveling)
            {
                _gameState = GameState.NextLevel;
            }
            else
            {
                _gameState = GameState.StartScreen;
            }
        }

        public static void StartGame()
        {
            GameState = GameState.Playing;
            _stopwatch.Start();
        }

        public static void CreateHunters(int numberOfEnemies)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                new Enemy();
            }
        }

        public static void CreateSoldiers(int numberOfSoldiers)
        {
            for (int i = 0; i < numberOfSoldiers; i++)
            {
                new Ally();
            }
        }


        #endregion
    }
}