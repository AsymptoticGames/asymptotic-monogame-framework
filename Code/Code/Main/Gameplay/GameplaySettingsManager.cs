using System;
using Microsoft.Xna.Framework;

namespace AsymptoticMonoGameFramework {

    public enum GameplayDifficulty {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }

    public static class GameplaySettingsManager {

        public static GameplayDifficulty currentGameplayDifficulty = GameplayDifficulty.Medium;
        public static string[] gameplayDifficultiesFriendlyStrings = new string[3] { "Easy", "Medium", "Hard" };

        public static void ResetToDefault() {
            currentGameplayDifficulty = GameplayDifficulty.Medium;
        }
    }
}
