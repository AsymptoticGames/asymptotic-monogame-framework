using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
    public class PauseMenu : MenuScreen
    {
        private MenuButton continueButton;
        private MenuButton graphicalSettingsButton;
        private MenuButton audioSettingsButton;
        private MenuButton controlsButton;
        private MenuButton exitButton;
        
        private Texture2D backgroundTint;
        private Texture2D menuBackground;

        private GraphicalSettingsMenu graphicalSettingsMenu;
        private AudioSettingsMenu audioSettingsMenu;
        public ControlsMenu controlsMenu;

        public PauseMenu() {

        }

        public PauseMenu(MenuScreen _parentMenu) : base(_parentMenu) {

        }

        public override void LoadContent() {
            backgroundTint = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-tint");
            menuBackground = Globals.content.Load<Texture2D>("Menues/PauseMenu/pause-menu-background");

            Vector2 buttonSize = new Vector2(400, 80);

            continueButton = new MenuButton(
                    new Vector2(760, 380),
                    this,
                    buttonSize,
                    "Continue"
                );
            
            graphicalSettingsButton = new MenuButton(
                    new Vector2(760, 480),
                    this,
                    buttonSize,
                    "Graphical Settings"
                );

            audioSettingsButton = new MenuButton(
                    new Vector2(760, 580),
                    this,
                    buttonSize,
                    "Audio Settings"
                );

            controlsButton = new MenuButton(
                    new Vector2(760, 680),
                    this,
                    buttonSize,
                    "Controls"
                );

            exitButton = new MenuButton(
                    new Vector2(760, 780),
                    this,
                    buttonSize, 
                    "Exit"
                );

            buttonList.Add(continueButton);
            buttonList.Add(graphicalSettingsButton);
            buttonList.Add(audioSettingsButton);
            buttonList.Add(controlsButton);
            buttonList.Add(exitButton);

            graphicalSettingsMenu = new GraphicalSettingsMenu(this);
            audioSettingsMenu = new AudioSettingsMenu(this);
            controlsMenu = new ControlsMenu(this);

            subMenues.Add(graphicalSettingsMenu);
            subMenues.Add(audioSettingsMenu);
            subMenues.Add(controlsMenu);

            foreach(MenuScreen subMenu in subMenues) {
                subMenu.LoadContent();
            }
        }

        public override void UnloadContent() {

        }

        protected override void BackPressed() {
            currentlySelectedButtonIndex = 0;
            Globals.gameplayManager.gameFlow.UnpauseGame();
        }

        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            if (button == continueButton) {
                ContinueButtonPressed();
            } else if (button == graphicalSettingsButton) {
                GraphicalSettingsButtonPressed();
            } else if(button == audioSettingsButton) {
                AudioSettingsButtonPressed();
            } else if(button == controlsButton) {
                ControlsButtonPressed();
            } else if(button == exitButton) {
                ExitButtonPressed();
            }
        }

        private void ContinueButtonPressed() {
            BackPressed();
        }

        private void GraphicalSettingsButtonPressed() {
            for(int i = 0; i < subMenues.Count; i++) {
                if(subMenues[i] == graphicalSettingsMenu) {
                    currentSubMenuScreenIndex = i;
                }
            }
            graphicalSettingsMenu.MenuScreenOpened();
        }

        private void AudioSettingsButtonPressed() {
            for (int i = 0; i < subMenues.Count; i++) {
                if (subMenues[i] == audioSettingsMenu) {
                    currentSubMenuScreenIndex = i;
                }
            }
            audioSettingsMenu.MenuScreenOpened();
        }

        private void ControlsButtonPressed() {
            for (int i = 0; i < subMenues.Count; i++) {
                if (subMenues[i] == controlsMenu) {
                    currentSubMenuScreenIndex = i;
                }
            }
            controlsMenu.MenuScreenOpened();
        }

        private void ExitButtonPressed() {
            Globals.gameInstance.ExitGameplay();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(backgroundTint, new Rectangle(0, 0, ResolutionConfig.virtualResolution.Item1, ResolutionConfig.virtualResolution.Item2), Color.White);
            spriteBatch.Draw(menuBackground, new Rectangle(320, 180, 1280, 720), currentSubMenuScreenIndex >= 0 ? CustomColors.darkerGray : Color.White);
            base.Draw(spriteBatch);
        }
    }
}

