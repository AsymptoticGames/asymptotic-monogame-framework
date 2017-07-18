using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AsymptoticMonoGameFramework {

    public class GraphicalSettingsMenu : MenuScreen {

        private ResolutionSelectionMenu resolutionSelectionMenu;

        private MenuButton resolutionButton;
        private MenuButton fullScreenButton;
        private MenuButton applyChangesButton;
        private MenuButton backButton;
        
        private Texture2D menuBackground;
        
        private Texture2D fullScreenCheckedTexture;
        private Texture2D fullScreenUncheckedTexture;

        private MenuLabel resolutionLabel;
        private bool fullScreenChecked;

        public int currentResolutionIndex;

        public GraphicalSettingsMenu() {
            currentResolutionIndex = ResolutionConfig.currentResolutionIndex;
        }

        public GraphicalSettingsMenu(MenuScreen _parentMenu) : base(_parentMenu) {
            currentResolutionIndex = ResolutionConfig.currentResolutionIndex;
        }

        public override void MenuScreenOpened() {
            base.MenuScreenOpened();
            fullScreenChecked = ResolutionConfig.isFullScreen;
            currentResolutionIndex = ResolutionConfig.currentResolutionIndex;
            LayoutVisualsToActualSettings();
        }

        public override void LoadContent() {
            resolutionSelectionMenu = new ResolutionSelectionMenu(this);

            menuBackground = Globals.content.Load<Texture2D>("Menues/PauseMenu/pause-menu-background");
            fullScreenCheckedTexture = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-button-blank-checked");
            fullScreenUncheckedTexture = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-button-blank-unchecked");

            Vector2 buttonSize = new Vector2(400, 80);

            resolutionLabel = new MenuLabel(
                    Globals.content.Load<SpriteFont>("Fonts/arial-bold-40"),
                    new Vector2(ResolutionConfig.virtualResolution.Item1 / 2, 540),
                    ResolutionConfig.GetCurrentResolution().Item1 + " x " + ResolutionConfig.GetCurrentResolution().Item2,
                    Color.White
                );

            resolutionButton = new MenuButton(
                    new Vector2(760, 430),
                    this,
                    buttonSize,
                    "Resolution"
                );

            fullScreenButton = new MenuButton(
                    new Vector2(760, 580),
                    this,
                    buttonSize,
                    "Full Screen",
                    fullScreenUncheckedTexture
                );

            applyChangesButton = new MenuButton(
                    new Vector2(760, 680),
                    this,
                    buttonSize,
                    "Apply Changes"
                );

            backButton = new MenuButton(
                    new Vector2(760, 780),
                    this,
                    buttonSize,
                    "Back"
                );
            
            labelList.Add(resolutionLabel);

            buttonList.Add(resolutionButton);
            buttonList.Add(fullScreenButton);
            buttonList.Add(applyChangesButton);
            buttonList.Add(backButton);

            subMenues.Add(resolutionSelectionMenu);

            LayoutVisualsToActualSettings();
            
            foreach (MenuScreen subMenu in subMenues) {
                subMenu.LoadContent();
            }
        }

        public override void UnloadContent() {

        }

        private void LayoutVisualsToActualSettings() {
            fullScreenButton.texture = ResolutionConfig.isFullScreen ? fullScreenCheckedTexture : fullScreenUncheckedTexture;
            currentResolutionIndex = ResolutionConfig.currentResolutionIndex;
            resolutionSelectionMenu.SetIndex(ResolutionConfig.currentResolutionIndex);
            if (ResolutionConfig.isFullScreen) {
                resolutionLabel.text = Resolution.GetMonitorResolution().X + " x " + Resolution.GetMonitorResolution().Y;
                resolutionLabel.SetColor(CustomColors.darkerGray);
                resolutionButton.selectable = false;
                fullScreenChecked = true;
            } else {
                resolutionLabel.text = ResolutionConfig.validResolutionOptions[currentResolutionIndex].Item1 + " x " + ResolutionConfig.validResolutionOptions[currentResolutionIndex].Item2;
                resolutionLabel.SetColor(Color.White);
                resolutionButton.selectable = true;
                fullScreenChecked = false;
            }
            
            while (!buttonList[currentlySelectedButtonIndex].selectable) {
                currentlySelectedButtonIndex++;
                if (currentlySelectedButtonIndex >= buttonList.Count) {
                    currentlySelectedButtonIndex = 0;
                }
            }

            if (currentResolutionIndex != ResolutionConfig.currentResolutionIndex || (fullScreenChecked && !ResolutionConfig.isFullScreen) || (!fullScreenChecked && ResolutionConfig.isFullScreen)) {
                backButton.buttonTextLabel.text = "Discard Changes";
            } else {
                backButton.buttonTextLabel.text = "Back";
            }
        }

        protected override void BackPressed() {
            currentlySelectedButtonIndex = 0;
            LayoutVisualsToActualSettings();
            parentMenu.CloseSubMenu();
        }

        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            if (button == resolutionButton) {
                ResolutionButtonPressed();
            }else if(button == fullScreenButton) {
                FullScreenButtonPressed();
            }else if(button == applyChangesButton) {
                ApplyChangesButtonPressed();
            }else if (button == backButton) {
                BackButtonPressed();
            }

            if(currentResolutionIndex != ResolutionConfig.currentResolutionIndex || (fullScreenChecked && !ResolutionConfig.isFullScreen) || (!fullScreenChecked && ResolutionConfig.isFullScreen)) {
                backButton.buttonTextLabel.text = "Discard Changes";
            } else {
                backButton.buttonTextLabel.text = "Back";
            }
        }

        private void ResolutionButtonPressed() {
            currentSubMenuScreenIndex = 0;
        }

        public override void CloseSubMenu() {
            base.CloseSubMenu();
            if (currentResolutionIndex != ResolutionConfig.currentResolutionIndex || (fullScreenChecked && !ResolutionConfig.isFullScreen) || (!fullScreenChecked && ResolutionConfig.isFullScreen)) {
                backButton.buttonTextLabel.text = "Discard Changes";
            } else {
                backButton.buttonTextLabel.text = "Back";
            }
        }

        private void FullScreenButtonPressed() {
            fullScreenChecked = !fullScreenChecked;
            fullScreenButton.texture = fullScreenChecked ? fullScreenCheckedTexture : fullScreenUncheckedTexture;
            if(fullScreenChecked) {
                resolutionLabel.text = Resolution.GetMonitorResolution().X + " x " + Resolution.GetMonitorResolution().Y;
                resolutionLabel.SetColor(CustomColors.darkerGray);
                resolutionButton.selectable = false;
            } else {
                resolutionLabel.text = ResolutionConfig.validResolutionOptions[currentResolutionIndex].Item1 + " x " + ResolutionConfig.validResolutionOptions[currentResolutionIndex].Item2;
                resolutionLabel.SetColor(Color.White);
                resolutionButton.selectable = true;
            }
        }

        private void ApplyChangesButtonPressed() {
            bool isFullScreen = fullScreenChecked;
            ResolutionConfig.SetResolution(currentResolutionIndex, isFullScreen);
        }

        private void BackButtonPressed() {
            BackPressed();
        }

        public void SetCurrentResolutionIndex(int index) {
            currentResolutionIndex = index;
            resolutionLabel.text = ResolutionConfig.validResolutionOptions[currentResolutionIndex].Item1 + " x " + ResolutionConfig.validResolutionOptions[currentResolutionIndex].Item2;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(menuBackground, new Rectangle(320, 180, 1280, 720), currentSubMenuScreenIndex >= 0 ? CustomColors.darkerGray : Color.White);
            base.Draw(spriteBatch);
        }
    }
}
