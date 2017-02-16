using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace AsymptoticMonoGameFramework
{
    public class SongManager {

        private readonly string[] gameplaySongList = new string[5] { "Back Into the Fray", "Get Those Spies!", "Prayer, Worry, Hope", "The Fractured Hero", "The Power and the Control" };
        private readonly string mainMenuSong = "A Terrible Battle (cut)";

        private SoundEffect song;
        private SoundEffectInstance songInstance;
        private int gameplaySongListIndex;

        public SongManager() {

        }

        public void Update(GameTime gameTime) {
            if (songInstance != null) {
                songInstance.Volume = GetSongVolume();
                if (Globals.gameInstance.gameState == GameState.inGame) {
                    if (songInstance.State != SoundState.Playing) {
                        gameplaySongListIndex++;
                        if (gameplaySongListIndex >= gameplaySongList.Length) {
                            gameplaySongListIndex = 0;
                        }
                        StopGameplay();
                        StartGameplay();
                    }
                }
            }
        }

        public void LoadContent() {

        }

        public void UnloadContent() {

        }
       
        public void StartGameplay() {
            gameplaySongListIndex = Globals.random.Next(gameplaySongList.Length);
            LoadNewSong(gameplaySongList[gameplaySongListIndex]);
            songInstance.IsLooped = false;
        }

        public void StopGameplay() {
            StopSongManager();
        }

        public void StartMainMenu() {
            LoadNewSong(mainMenuSong);
            songInstance.Volume = GetSongVolume();
            songInstance.Pitch = 0.0f;
            songInstance.IsLooped = true;
        }

        public void StopMainMenu() {
            StopSongManager();
        }

        private void StopSongManager() {
            songInstance.Stop();
            songInstance = null;
        }

        private void LoadNewSong(string songName) {
            song = Globals.content.Load<SoundEffect>("Audio/Songs/" + songName);
            songInstance = song.CreateInstance();
            songInstance.Play();
        }

        private float GetSongVolume() {
            return 0.3f * AudioConfig.musicVolume;
        }
    }
}
