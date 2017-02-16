using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
    public enum StartMenuesState {
        SplashScreen,
        MainMenu,
        GameplaySettingsMenu
    }

    public class StartMenuesManager
    {
        public StartMenuesState startMenuesState;

        private SplashScreen splashScreen;
        public MainMenu mainMenu;
        private GameplaySettingsMenu gameplaySettingsMenu;

        public StartMenuesManager() {
            startMenuesState = StartMenuesState.SplashScreen;
            splashScreen = new SplashScreen();
            mainMenu = new MainMenu();
            gameplaySettingsMenu = new GameplaySettingsMenu();
        }

        public void LoadContent() {
            splashScreen.LoadContent();
            mainMenu.LoadContent();
            gameplaySettingsMenu.LoadContent();
        }

        public void UnloadContent() {
            splashScreen.UnloadContent();
            mainMenu.UnloadContent();
            gameplaySettingsMenu.UnloadContent();
        }

        public void Update(GameTime gameTime) {
            if (startMenuesState == StartMenuesState.SplashScreen) {
                splashScreen.Update(gameTime);
            }else if(startMenuesState == StartMenuesState.MainMenu) {
                mainMenu.Update(gameTime);
            } else if (startMenuesState == StartMenuesState.GameplaySettingsMenu) {
                gameplaySettingsMenu.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (startMenuesState == StartMenuesState.SplashScreen) {
                splashScreen.Draw(spriteBatch);
            } else if (startMenuesState == StartMenuesState.MainMenu) {
                mainMenu.Draw(spriteBatch);
            } else if (startMenuesState == StartMenuesState.GameplaySettingsMenu) {
                gameplaySettingsMenu.Draw(spriteBatch);
            }
        }

        public void LoadGameplay() {
            Globals.songManager.StopMainMenu();
            Globals.gameInstance.gameState = GameState.inLoadingScreen;
            Globals.gameInstance.isLoading = false;
        }

        public void LoadGameplaySettingsMenu() {
            startMenuesState = StartMenuesState.GameplaySettingsMenu;
            GameplaySettingsManager.ResetToDefault();
        }
    }
}
