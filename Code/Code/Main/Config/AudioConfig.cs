namespace AsymptoticMonoGameFramework{

    public static class AudioConfig {

        public static float musicVolume = 0.0f;
        public static float soundEffectVolume = 0.0f;
        
        public static void SaveAudioSettings() {
            Globals.gameInstance.settingsManager.settings.soundEffectVolume = (int) (soundEffectVolume * 100);
            Globals.gameInstance.settingsManager.settings.musicVolume = (int) (musicVolume * 100);
            Globals.gameInstance.settingsManager.Save();
        }

        public static void LoadAudioSettings() {
            soundEffectVolume = HelperFunctions.Clamp((float)Globals.gameInstance.settingsManager.settings.soundEffectVolume / 100.0f, 0, 1);
            musicVolume = HelperFunctions.Clamp((float)Globals.gameInstance.settingsManager.settings.musicVolume / 100.0f, 0, 1);
        }
    }
}
