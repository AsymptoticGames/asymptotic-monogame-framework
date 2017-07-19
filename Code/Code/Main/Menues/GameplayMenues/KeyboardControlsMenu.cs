using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework {

    public class KeyboardControlsMenu : ScrollingMenuScreen {
        
        private MenuButton backButton;

        private Texture2D leftArrow;
        private Texture2D rightArrow;

        private bool waitingForAllKeysToBeUnpressed = false; //must unpress all keys after clicking a button so the control isn't immediately assigned to that button
        private bool waitingForKeyPress = false;
        private int buttonIndexPressed = -1;
        private int selectionArrowColor = 0;

        public KeyboardControlsMenu() {
            Setup();
        }

        public KeyboardControlsMenu(MenuScreen _parentMenu) : base(_parentMenu) {
            Setup();
        }

        private void Setup() {
            buttonSize = MenuButton.buttonSize + new Vector2(0, 12);
            buttonPadding = 4;
            menuSize = new Vector2(1450, 816);
            titleSize = new Vector2(1280, 96);
            titleString = DefaultControls.currentKeyboardPreset[0];
        }

        public override void MenuScreenOpened() {
            base.MenuScreenOpened();
            UpdateAllButtonImages();
            UpdateTitleLabel(DefaultControls.currentKeyboardPreset[0]);
            CheckToEnableButtons();
        }

        public override void LoadContent() {
            base.LoadContent();

            leftArrow = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-button-side-scroll-arrow");
            rightArrow = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-button-side-scroll-arrow");

            SpriteFont arial22Font = Globals.content.Load<SpriteFont>("Fonts/arial-bold-22");

            foreach (KeyValuePair<string, object> entry in DefaultControls.keyboardControls) {
                AddButton(new KeyboardControlsMenuButton(
                        new Vector2(),
                        this,
                        buttonSize,
                        entry.Key,
                        arial22Font,
                        ControlsConfig.keyboardControls[entry.Key][0]
                    ));
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

        private void UpdateNewControlsPreset() {
            UpdateTitleLabel(DefaultControls.currentKeyboardPreset[0]);
            ControlsConfig.ApplyPresetToKeyboard(DefaultControls.currentKeyboardPreset[0]);
            UpdateAllButtonImages();
        }

        public override void Update(GameTime gameTime) {
            if (!waitingForKeyPress) {
                base.Update(gameTime);
                if (GlobalControls.MenuLeftPressed() ||
                        (PlayerControls.MouseLeftPressed() && LeftArrowBoundingRect().Contains(PlayerControls.MousePosition()))) {
                    selectionArrowColor = -3;
                    DefaultControls.DecrementKeyboardPreset();
                    CheckToEnableButtons();
                    UpdateNewControlsPreset();
                } else if (GlobalControls.MenuRightPressed() ||
                        (PlayerControls.MouseLeftPressed() && RightArrowBoundingRect().Contains(PlayerControls.MousePosition()))) {
                    selectionArrowColor = 3;
                    DefaultControls.IncrementKeyboardPreset();
                    CheckToEnableButtons();
                    UpdateNewControlsPreset();
                }
            } else {
                if (!waitingForAllKeysToBeUnpressed) {
                    if (KeyboardInputPressed()) {
                        ((KeyboardControlsMenuButton)buttonList[buttonIndexPressed]).SetNewInput(KeyPressed());
                        waitingForKeyPress = false;
                    }
                } else if (!KeyboardInputPressed()) {
                    waitingForAllKeysToBeUnpressed = false;
                }
            }
        }

        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            if (button == backButton) {
                BackButtonPressed();
            } else {
                KeyboardControlsMenuButton controlsButton = (KeyboardControlsMenuButton)button;
                if (controlsButton.buttonType == KeyboardControlsMenuButtonType.Button) {
                    controlsButton.SetWaitingForInput();
                    buttonIndexPressed = buttonList.IndexOf(button);
                    waitingForAllKeysToBeUnpressed = true;
                    waitingForKeyPress = true;
                } else if (controlsButton.buttonType == KeyboardControlsMenuButtonType.Toggle) {
                    bool currentToggle = (ControlsConfig.keyboardControls[controlsButton.controlsTKey][0] == (int)ToggleOptions.True);
                    controlsButton.SetNewInput(!currentToggle);
                }
            }
        }
        
        private bool KeyboardInputPressed() {
            return Keyboard.GetState().GetPressedKeys().Length > 0 || Mouse.GetState().LeftButton == ButtonState.Pressed || 
                    Mouse.GetState().RightButton == ButtonState.Pressed || Mouse.GetState().MiddleButton == ButtonState.Pressed;
        }

        private int KeyPressed() {
            if (Keyboard.GetState().GetPressedKeys().Length > 0) {
                Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
                return (int)keysPressed[0];
            } else if(Mouse.GetState().LeftButton == ButtonState.Pressed){
                return (int)MouseClickOptions.LeftClick;
            } else if (Mouse.GetState().RightButton == ButtonState.Pressed) {
                return (int)MouseClickOptions.RightClick;
            } else if (Mouse.GetState().MiddleButton == ButtonState.Pressed) {
                return (int)MouseClickOptions.MiddleClick;
            }
            return 0;
        }

        private void BackButtonPressed() {
            BackPressed();
        }

        private void CheckToEnableButtons() {
            SetAllButtonsEnabled(DefaultControls.KeyboardPresetIsCustom());
        }

        private void SetAllButtonsEnabled(bool value) {
            foreach (MenuSelection button in buttonList) {
                if (button is KeyboardControlsMenuButton) {
                    button.enabled = value;
                }
            }
        }

        public void UpdateAllButtonImages() {
            foreach (MenuButton button in buttonList) {
                if (button is KeyboardControlsMenuButton) {
                    ((KeyboardControlsMenuButton)button).UpdateImage();
                }
            }
        }

        public bool CurrentlySettingControl() {
            return waitingForKeyPress;
        }

        private Rectangle LeftArrowBoundingRect() {
            return new Rectangle((int)ResolutionConfig.virtualResolution.Item1 / 2 - (int)SizeOfTitleString().X / 2 - 72, BoundingRect().Y + (int)(titleSize.Y / 2) + menuPadding / 2 - 16, 32, 32);
        }

        private Rectangle RightArrowBoundingRect() {
            return new Rectangle((int)ResolutionConfig.virtualResolution.Item1 / 2 + (int)SizeOfTitleString().X / 2 + 40, BoundingRect().Y + (int)(titleSize.Y / 2) + menuPadding / 2 - 16, 32, 32);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            if (selectionArrowColor < 0) {
                spriteBatch.Draw(leftArrow, LeftArrowBoundingRect(), Color.White * 0.3f);
                selectionArrowColor++;
            } else {
                spriteBatch.Draw(leftArrow, LeftArrowBoundingRect(), currentSubMenuScreenIndex >= 0 ? CustomColors.darkerGray : Color.White);
            }

            if (selectionArrowColor > 0) {
                spriteBatch.Draw(rightArrow, RightArrowBoundingRect(), null, Color.White * 0.3f, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                selectionArrowColor--;
            } else {
                spriteBatch.Draw(rightArrow, RightArrowBoundingRect(), null, currentSubMenuScreenIndex >= 0 ? CustomColors.darkerGray : Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}
