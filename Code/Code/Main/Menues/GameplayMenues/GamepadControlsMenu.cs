using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework{

    public class GamepadControlsMenu : ScrollingMenuScreen {

        private int playerNumber;

        private MenuButtonSideScroll choosePresetButton;
        private MenuButton applyToAllGamepadsButton;
        private MenuButton backButton;

        private bool waitingForAllKeysToBeUnpressed = false; //must unpress all keys after clicking a button so the control isn't immediately assigned to that button
        private bool waitingForKeyPress = false;
        private int buttonIndexPressed = -1;

        public GamepadControlsMenu(int _playerNumber) {
            playerNumber = _playerNumber;
            Setup();
        }

        public GamepadControlsMenu(MenuScreen _parentMenu, int _playerNumber) : base(_parentMenu) {
            playerNumber = _playerNumber;
            Setup();
        }

        private void Setup() {
            buttonSize = MenuButton.buttonSize + new Vector2(0, 12);
            buttonPadding = 4;
        }

        public override void MenuScreenOpened() {
            base.MenuScreenOpened();
            UpdateAllButtonImages();
            UpdateButtonsEnabled();
        }

        public override void LoadContent() {
            base.LoadContent();
            
            SpriteFont arial22Font = Globals.content.Load<SpriteFont>("Fonts/arial-bold-22");
            
            foreach (KeyValuePair<string, object> entry in DefaultControls.gamepadControls) {
                AddButton(new GamepadControlsMenuButton(
                        new Vector2(),
                        this,
                        buttonSize,
                        entry.Key,
                        arial22Font,
                        ControlsConfig.gamepadControls[entry.Key][playerNumber]
                    ));
            }

            applyToAllGamepadsButton = new MenuButton(
                    new Vector2(),
                    this,
                    buttonSize,
                    "Apply to all Gamepads",
                    arial22Font
                );
            AddButton(applyToAllGamepadsButton);
            
            if (DefaultControls.gamepadPresets.Count > 1) {
                choosePresetButton = new MenuButtonSideScroll(
                    new Vector2(),
                    this,
                    buttonSize,
                    DefaultControls.gamepadPresets.Keys.ToArray(),
                    0,
                    arial22Font
                );
                AddButton(choosePresetButton);
            }

            backButton = new MenuButton(
                    new Vector2(),
                    this,
                    buttonSize,
                    "Back",
                    arial22Font
                );
            AddButton(backButton);
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        protected override void BackPressed() {
            ControlsConfig.SaveControlsSettings();
            currentlySelectedButtonIndex = 0;
            parentMenu.CloseSubMenu();
        }

        protected override void AddButton(MenuButton button) {
            base.AddButton(button);
            if (button is GamepadControlsMenuButton) {
                ((GamepadControlsMenuButton)button).controllerIndex = playerNumber;
            }
        }

        public override void Update(GameTime gameTime) {
            if (!waitingForKeyPress) {
                base.Update(gameTime);
            } else {
                if (!waitingForAllKeysToBeUnpressed) {
                    if (GamepadInputPressed()) {
                        ((GamepadControlsMenuButton)buttonList[buttonIndexPressed]).SetNewInput(ButtonPressed());
                        waitingForKeyPress = false;
                    }
                } else if (!GamepadInputPressed()) {
                    waitingForAllKeysToBeUnpressed = false;
                }
            }
        }

        public override void ButtonSideScrollScrolled(MenuButtonSideScroll button, int direction) {
            base.ButtonSideScrollScrolled(button, direction);
            if (button == choosePresetButton) {
                ControlsConfig.ApplyPresetToGamepad(playerNumber, button.GetSelectedValue());
                UpdateAllButtonImages();
                UpdateButtonsEnabled();
            }
        }

        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            if (button == backButton) {
                BackButtonPressed();
            } else if (button == applyToAllGamepadsButton) {
                ApplyToAllGamepadsPressed();
            } else {
                GamepadControlsMenuButton controlsButton = (GamepadControlsMenuButton)button;
                if (controlsButton.buttonType == GamepadControlsMenuButtonType.Button) {
                    controlsButton.SetWaitingForInput();
                    buttonIndexPressed = buttonList.IndexOf(button);
                    waitingForAllKeysToBeUnpressed = true;
                    waitingForKeyPress = true;
                } else if (controlsButton.buttonType == GamepadControlsMenuButtonType.JoystickOption) {
                    JoystickOptions currentInput = (JoystickOptions)ControlsConfig.gamepadControls[controlsButton.controlsTKey][controlsButton.controllerIndex];
                    controlsButton.SetNewInput(ControlsConfig.GetNextJoystickOption(currentInput));
                } else if (controlsButton.buttonType == GamepadControlsMenuButtonType.Toggle) {
                    bool currentToggle = (ControlsConfig.gamepadControls[controlsButton.controlsTKey][controlsButton.controllerIndex] == (int)ToggleOptions.True);
                    controlsButton.SetNewInput(!currentToggle);
                }
            }
        }

        private bool GamepadInputPressed() {
            foreach(Buttons button in Enum.GetValues(typeof(Buttons))) {
                if (ButtonIsValid(button)) { 
                    for(int i = 0; i < ControlsConfig.numGamepads; i++) {
                        if (GamePad.GetState(i).IsButtonDown(button)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private Buttons ButtonPressed() {
            foreach (Buttons button in Enum.GetValues(typeof(Buttons))) {
                if (ButtonIsValid(button)) {
                    for (int i = 0; i < ControlsConfig.numGamepads; i++) {
                        if (GamePad.GetState(i).IsButtonDown(button)) {
                            return button;
                        }
                    }
                }
            }
            return Buttons.A;
        }

        private bool ButtonIsValid(Buttons button) {
            return button != Buttons.LeftThumbstickUp && button != Buttons.LeftThumbstickDown && button != Buttons.LeftThumbstickLeft && button != Buttons.LeftThumbstickRight &&
                    button != Buttons.RightThumbstickUp && button != Buttons.RightThumbstickDown && button != Buttons.RightThumbstickLeft && button != Buttons.RightThumbstickRight;
        }

        private void BackButtonPressed() {
            BackPressed();
        }

        private void ApplyToAllGamepadsPressed() {
            ControlsConfig.ApplyControlsToAllGamepads(playerNumber);
            ((ControlsMenu)parentMenu).UpdateAllControlsButtonImages();
        }

        private void UpdateButtonsEnabled() {
            if (choosePresetButton.GetSelectedValue() == DefaultControls.customPresetString) {
                SetAllButtonsEnabled(true);
            } else {
                SetAllButtonsEnabled(false);
            }
        }

        private void SetAllButtonsEnabled(bool value) {
            foreach(MenuSelection button in buttonList) {
                if (button is GamepadControlsMenuButton) {
                    button.enabled = value;
                }
            }
        }

        public void UpdateAllButtonImages() {
            foreach (MenuButton button in buttonList) {
                if (button is GamepadControlsMenuButton) {
                    ((GamepadControlsMenuButton)button).UpdateImage();
                }
            }
        }

        public bool CurrentlySettingControl() {
            return waitingForKeyPress;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }
    }
}
