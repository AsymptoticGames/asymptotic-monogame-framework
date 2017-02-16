using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace AsymptoticMonoGameFramework{

    public static class Globals {

        public static StartMenuesManager startMenuesManager;
        public static LoadingScreenManager loadingScreenManager;
        public static GameplayManager gameplayManager;
        public static SongManager songManager;
        public static SoundEffectsManager soundEffectsManager;

        public static ContentManager content;
        public static GameWindow gameWindow;
        public static Game1 gameInstance;

        public static Random random = new Random();
        
    }
}
