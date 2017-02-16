using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AsymptoticMonoGameFramework {

    public class ControlsMenu : ScrollingMenuScreen {

        private MenuButton backButton;
        private MenuButton[] gamepadControlsButton;
        private MenuButton keyboardControlsButton;
        private GamepadControlsMenu[] gamepadControlsMenu;
        private KeyboardControlsMenu keyboardControlsMenu;
        
        public ControlsMenu() {
            gamepadControlsButton = new MenuButton[ControlsConfig.numGamepads];
            gamepadControlsMenu = new GamepadControlsMenu[ControlsConfig.numGamepads];
        }

        public ControlsMenu(MenuScreen _parentMenu) : base(_parentMenu) {
            gamepadControlsButton = new MenuButton[ControlsConfig.numGamepads];
            gamepadControlsMenu = new GamepadControlsMenu[ControlsConfig.numGamepads];
        }

        private void Setup() {
            buttonSize = MenuButton.buttonSize + new Vector2(0, 12);
            buttonPadding = 4;
        }

        public override void LoadContent() {
            base.LoadContent();

            for (int i = 0; i < ControlsConfig.numGamepads; i++) {
                gamepadControlsButton[i] = new MenuButton(
                    new Vector2(),
                    this,
                    buttonSize,
                    "Player " + (i+1) + " Gamepad"
                );
                AddButton(gamepadControlsButton[i]);

                gamepadControlsMenu[i] = new GamepadControlsMenu(this, i);
                gamepadControlsMenu[i].LoadContent();
                subMenues.Add(gamepadControlsMenu[i]);
            }
            
            keyboardControlsButton = new MenuButton(
                new Vector2(),
                this,
                buttonSize,
                "Keyboard"
            );
            AddButton(keyboardControlsButton);

            keyboardControlsMenu = new KeyboardControlsMenu(this);
            keyboardControlsMenu.LoadContent();
            subMenues.Add(keyboardControlsMenu);

            backButton = new MenuButton(
                    new Vector2(),
                    this,
                    buttonSize,
                    "Back"
                );

            AddButton(backButton);
        }

        public override void UnloadContent() {

        }

        protected override void BackPressed() {
            currentlySelectedButtonIndex = 0;
            parentMenu.CloseSubMenu();
        }

        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            if (button == backButton) {
                BackButtonPressed();
            }else if(ButtonIsGamepadControlsButton(button) >= 0) {
                GamepadControlsButtonPressed(ButtonIsGamepadControlsButton(button));
            }else if(button == keyboardControlsButton) {
                KeyboardControlsButtonPressed();
            }
        }

        private void BackButtonPressed() {
            BackPressed();
        }

        private void GamepadControlsButtonPressed(int index) {
            currentSubMenuScreenIndex = index;
            gamepadControlsMenu[index].MenuScreenOpened();
        }
        
        private void KeyboardControlsButtonPressed() {
            currentSubMenuScreenIndex = subMenues.IndexOf(keyboardControlsMenu);
            keyboardControlsMenu.MenuScreenOpened();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }

        public void UpdateAllControlsButtonImages() {
            foreach(GamepadControlsMenu gamepadMenu in gamepadControlsMenu) {
                gamepadMenu.UpdateAllButtonImages();
            }
            keyboardControlsMenu.UpdateAllButtonImages();
        }

        public bool CurrentlySettingControl() {
            foreach(GamepadControlsMenu gamepadMenu in gamepadControlsMenu) {
                if (gamepadMenu.CurrentlySettingControl()) {
                    return true;
                }
            }
            if (keyboardControlsMenu.CurrentlySettingControl()) {
                return true;
            }
            return false;
        }

        private int ButtonIsGamepadControlsButton(MenuButton button) {
            for(int i = 0; i < gamepadControlsButton.Length; i++) {
                MenuButton gamepadButton = gamepadControlsButton[i];
                if(button == gamepadButton) {
                    return i;
                }
            }
            return -1;
        }
    }
}
