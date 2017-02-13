#region

using System.Collections.Generic;
using Xna.Helpers;

#endregion

namespace Frend.Classes
{

    #region Enums

    #endregion

    internal static class TextTimeout
    {
        #region Fields

        private const int TIMEMS = 2000;
        private static TimeOut _timer;
        private static List<string> _textFinishGame;
        private static Dictionary<string, List<string>> _texts;
        private static int _textIndex;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        static TextTimeout()
        {
            _timer = new TimeOut(TIMEMS);
            _textFinishGame = new List<string>();
            _texts = new Dictionary<string, List<string>> {{"Finish", _textFinishGame}};


            _textIndex = 0;
            Initialize();
        }

        #endregion

        #region Methods

        public static void PlayText(ref bool hasText, ref string text, string nameOfTextsList)
        {
            if (_textIndex < _texts["Finish"].Count)
            {
                hasText = true;
                text = _texts["Finish"][_textIndex];
            }
            else
            {
                hasText = false;
                _timer.RestartTimer();
            }
            if (!_timer.StopWatch.IsRunning)
            {
                _timer.StopWatch.Start();
            }
            if (_timer.Update())
            {
                _textIndex++;
                _timer.RestartTimer();
            }
        }

        public static void Initialize()
        {
            InitializeTexts();
        }

        #endregion

        #region Texts

        private static void InitializeTexts()
        {
            _textFinishGame.Add("Nothing interests me");
            _textFinishGame.Add("Hm maybe i will roll a bit ha? :D");
            _textFinishGame.Add("You can stand here and watch if you want");
        }

        #endregion

        //todo:Dictionary of popular lines Download???!!
    }
}