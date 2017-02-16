using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
    public class GameFlow {

        public enum GameScreenStatus {
            inGame,
            inPause
        };

        public PauseMenu pauseMenu;
        public GameUI gameUI;
        public GameScreenStatus gameScreenStatus;

        public GameFlow() {
            pauseMenu = new PauseMenu();
            gameUI = new GameUI();
            gameScreenStatus = GameScreenStatus.inGame;
        }

        public void LoadContent() {
            gameUI.LoadContent();
            pauseMenu.LoadContent();
        }

        public void UnloadContent() {
            gameUI.UnloadContent();
            pauseMenu.UnloadContent();
        }

        public void Update(GameTime gameTime) {
            gameUI.Update(gameTime);
            if (gameScreenStatus == GameScreenStatus.inPause) {
                pauseMenu.Update(gameTime);
            } else if (gameScreenStatus == GameScreenStatus.inGame) {
                if (GlobalControls.PausePressed() || PlayerControls.PausePressed(ControlsConfig.keyboardControllerIndex)) {
                    PauseGame();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            gameUI.Draw(spriteBatch);
            if (gameScreenStatus == GameScreenStatus.inPause) {
                pauseMenu.Draw(spriteBatch);
            }
        }

        public void PauseGame() {
            gameScreenStatus = GameScreenStatus.inPause;
            PauseAllSoundEffects();
        }

        public void UnpauseGame() {
            gameScreenStatus = GameScreenStatus.inGame;
            ResumeAllSoundEffects();
        }

        public void PauseAllSoundEffects() {

        }

        public void ResumeAllSoundEffects() {

        }
    }
}
