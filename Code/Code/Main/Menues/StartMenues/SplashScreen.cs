using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework{

    public class SplashScreen{

        private Texture2D splashImage;
        private Texture2D tint;

        private float fadeInTime;
        private float showTime;
        private float fadeOutTime;
        private float timer;

        public SplashScreen() {
            timer = 0.0f;
            fadeInTime = 1.0f;
            showTime = 2.0f;
            fadeOutTime = 1.0f;
        }

        public void LoadContent() {
            tint = Globals.content.Load<Texture2D>("SplashScreen/tint");
            splashImage = Globals.content.Load<Texture2D>("SplashScreen/splash-image");
        }

        public void UnloadContent() {

        }

        public void Update (GameTime gameTime) {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(timer > GetTotalScreenTime()){
                Globals.startMenuesManager.startMenuesState = StartMenuesState.MainMenu;
                Globals.songManager.StartMainMenu();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(splashImage, new Rectangle(0, 0, ResolutionConfig.virtualResolution.Item1, ResolutionConfig.virtualResolution.Item2), Color.White);
            if(timer < fadeInTime) {
                spriteBatch.Draw(tint, new Rectangle(0, 0, ResolutionConfig.virtualResolution.Item1, ResolutionConfig.virtualResolution.Item2), Color.White * GetFadeInAmount());
            }
            if(timer > (fadeInTime + showTime)) {
                spriteBatch.Draw(tint, new Rectangle(0, 0, ResolutionConfig.virtualResolution.Item1, ResolutionConfig.virtualResolution.Item2), Color.White * GetFadeOutAmount());
            }
        }

        private float GetFadeInAmount() {
            return ((fadeInTime - timer) / fadeInTime);
        }

        private float GetFadeOutAmount() {
            return ((timer - (GetTotalScreenTime() - fadeOutTime)) / fadeOutTime);
        }

        private float GetTotalScreenTime() {
            return fadeInTime + showTime + fadeOutTime;
        }

    }
}
