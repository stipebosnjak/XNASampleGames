using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Flee.Classes
{
    class Audio
    {
        private static Song _mainSong;
        private static SoundEffect _soldierHealedSound;
        private static SoundEffect _enemyKilledSound;

        public static void LoadSounds(ContentManager contentManager)
        {
            _mainSong  = contentManager.Load<Song>(@"Audio\Main");
            _soldierHealedSound = contentManager.Load<SoundEffect>(@"Audio\Successful");
            _enemyKilledSound = contentManager.Load<SoundEffect>(@"Audio\Gun_Silencer");
        }

        public static void PlaySong()
        {
            MediaPlayer.Volume = 30;
            MediaPlayer.Play(_mainSong);
        }
        public static void PlaySoldierHealed()
        {
            _soldierHealedSound.Play(0.2f,0.0f,0.0f);
        }
        public static void PlayEnemyKilled()
        {
            _enemyKilledSound.Play(0.2f, 0.0f, 0.0f);
        }

    }
}
