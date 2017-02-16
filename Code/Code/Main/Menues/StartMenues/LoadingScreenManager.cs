using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
    public class LoadingScreenManager {
        
        private AnimatedEntity loadingIcon;
        private Animation loadingAnimation;
        
        private Thread backgroundThread;

        public LoadingScreenManager() {

        }

        public void LoadContent() {
            loadingIcon = new AnimatedEntity();
            loadingAnimation = new Animation("idle", true, 2, "Menues/MainMenu/loading-icon", string.Empty);
            loadingAnimation.LoadRectAnimation(600, 200, 1, 3);
            loadingIcon.AddAnimation(loadingAnimation);
            loadingIcon.PlayAnimation("idle");
        }

        public void UnloadContent() {

        }

        public void Update(GameTime gameTime) {
            loadingIcon.Update(gameTime);
            if (!Globals.gameInstance.isLoading) {
                backgroundThread = new Thread(Globals.gameplayManager.LoadContent);
                Globals.gameInstance.isLoading = true;
                backgroundThread.Start();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            loadingIcon.Draw(spriteBatch, new Rectangle(1920 / 2 - 300, 1080 / 2 - 100, 600, 200));
        }
    }
}
