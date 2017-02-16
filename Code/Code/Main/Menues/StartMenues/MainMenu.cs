using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework
{
    public class MainMenu : MenuScreen{

        private Texture2D background;

        private MenuButton startGameButton;
        private MenuButton settingsButton;
        private MenuButton exitGameButton;

        public MainMenuSettingsMenu mainMenuSettingsMenu;

        public MainMenu() {

        }

        public override void LoadContent() {
            background = Globals.content.Load<Texture2D>("Menues/MainMenu/main-menu-background");

            startGameButton = new MenuButton(
                new Vector2(760, 740),
                this,
                MenuButton.buttonSize,
                "",
                Globals.content.Load<Texture2D>("Menues/MainMenu/main-menu-start-game-button")
            );
            buttonList.Add(startGameButton);

            settingsButton = new MenuButton(
                new Vector2(760, 820),
                this,
                MenuButton.buttonSize,
                "",
                Globals.content.Load<Texture2D>("Menues/MainMenu/main-menu-game-settings-button")
            );
            buttonList.Add(settingsButton);

            exitGameButton = new MenuButton(
                new Vector2(760, 900),
                this,
                MenuButton.buttonSize,
                "",
                Globals.content.Load<Texture2D>("Menues/MainMenu/main-menu-exit-button")
            );
            buttonList.Add(exitGameButton);

            mainMenuSettingsMenu = new MainMenuSettingsMenu(this);
            subMenues.Add(mainMenuSettingsMenu);

            foreach (MenuScreen subMenu in subMenues) {
                subMenu.LoadContent();
            }
        }

        public override void UnloadContent() {

        }

        private void StartGameButtonPressed() {
            Globals.startMenuesManager.LoadGameplaySettingsMenu();
        }

        private void SettingsButtonPressed() {
            for (int i = 0; i < subMenues.Count; i++) {
                if (subMenues[i] == mainMenuSettingsMenu) {
                    currentSubMenuScreenIndex = i;
                }
            }
        }

        private void ExitGameButtonPressed() {
            Globals.gameInstance.Exit();
        }

        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            if (button == startGameButton) {
                StartGameButtonPressed();
            }else if(button == settingsButton) {
                SettingsButtonPressed();
            }else if(button == exitGameButton) {
                ExitGameButtonPressed();
            }
        }

        protected override void BackPressed() {
            //Do Nothing
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(background, new Rectangle(0, 0, ResolutionConfig.virtualResolution.Item1, ResolutionConfig.virtualResolution.Item2), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
