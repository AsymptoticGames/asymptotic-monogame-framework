using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework {

    public class GameplaySettingsMenu : MenuScreen {

        private Texture2D background;

        private MenuLabel gameDifficultyTextLabel;
        private MenuButtonSideScroll gameplayDifficultySelection;
        
        private MenuButton beginGameButton;

        public GameplaySettingsMenu() {

        }

        public override void LoadContent() {
            background = Globals.content.Load<Texture2D>("Menues/MainMenu/GameSettingsMenu/game-settings-menu-background");

            gameDifficultyTextLabel = new MenuLabel(
                         Globals.content.Load<SpriteFont>("Fonts/arial-bold-40"),
                         new Vector2(1920 / 2, 240),
                         "Game Difficulty",
                         Color.Black
                     );
            labelList.Add(gameDifficultyTextLabel);

            gameplayDifficultySelection = new MenuButtonSideScroll(
                         new Vector2(1920/2 - MenuButton.buttonSize.X / 2, 310 - MenuButton.buttonSize.Y / 2),
                         this,
                         MenuButton.buttonSize,
                         GameplaySettingsManager.gameplayDifficultiesFriendlyStrings,
                         (int)GameplaySettingsManager.currentGameplayDifficulty
                     );
            buttonList.Add(gameplayDifficultySelection);

            beginGameButton = new MenuButton(
                         new Vector2(1920 / 2 - MenuButton.buttonSize.X / 2 - 15, 420 - MenuButton.buttonSize.Y / 2 - 5),
                         this,
                         MenuButton.buttonSize + new Vector2(30, 10),
                         "Begin Game",
                         Globals.content.Load<SpriteFont>("Fonts/arial-bold-40")
                    );
            buttonList.Add(beginGameButton);
        }

        public override void UnloadContent() {

        }
        
        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            if (button == beginGameButton) {
                BeginGameButtonPressed();
            }
        }

        protected override void BackPressed() {
            Globals.startMenuesManager.startMenuesState = StartMenuesState.MainMenu;
        }

        private void BeginGameButtonPressed() {
            SetGameplayOptions();
            Globals.startMenuesManager.LoadGameplay();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(background, new Rectangle(0, 0, ResolutionConfig.virtualResolution.Item1, ResolutionConfig.virtualResolution.Item2), Color.White);
            base.Draw(spriteBatch);   
        }
        
        private void SetGameplayOptions() {
            GameplaySettingsManager.currentGameplayDifficulty = (GameplayDifficulty)gameplayDifficultySelection.selectionIndex;
            Console.WriteLine("" + GameplaySettingsManager.currentGameplayDifficulty);
        }
    }
}
