using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework{

    public abstract class MenuScreen {
        
        protected MenuScreen parentMenu;

        protected List<MenuSelection> buttonList;
        protected int currentlySelectedButtonIndex;

        protected List<MenuScreen> subMenues;
        protected int currentSubMenuScreenIndex;
        
        protected List<MenuLabel> labelList;
        protected List<MenuImage> imageList;

        protected bool cycleOptions;
        protected bool inGame;

        public MenuScreen() {
            Setup();
        }

        public MenuScreen(MenuScreen _parentMenu) {
            Setup();
            parentMenu = _parentMenu;
        }

        private void Setup() {
            buttonList = new List<MenuSelection>();
            subMenues = new List<MenuScreen>();
            labelList = new List<MenuLabel>();
            imageList = new List<MenuImage>();
            currentlySelectedButtonIndex = 0;
            currentSubMenuScreenIndex = -1;
            parentMenu = null;
            cycleOptions = true;
            inGame = Globals.gameInstance.gameState == GameState.inGame;
        }

        public abstract void LoadContent();

        public abstract void UnloadContent();

        public virtual void MenuScreenOpened() {
            //Do Nothing
        }

        public virtual void CloseSubMenu() {
            currentSubMenuScreenIndex = -1;
        }

        public virtual void Update(GameTime gameTime) {

            if (GlobalControls.PausePressed()) {
                BackPressed();
                Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuBack, false, SoundEffectsManager.zeroPanVectorLocation);
            }

            if (currentSubMenuScreenIndex >= 0) {
                subMenues[currentSubMenuScreenIndex].Update(gameTime);
            } else {

                if (GlobalControls.DeclinePressed()) {
                    BackPressed();
                    Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuBack, false, SoundEffectsManager.zeroPanVectorLocation);
                }

                if (buttonList.Count > 0) {

                    if (GlobalControls.MenuDownPressed()) {
                        currentlySelectedButtonIndex++;
                        if (currentlySelectedButtonIndex >= buttonList.Count) {
                            if (cycleOptions) {
                                currentlySelectedButtonIndex = 0;
                            } else {
                                currentlySelectedButtonIndex = buttonList.Count - 1;
                            }
                        }

                        while (!buttonList[currentlySelectedButtonIndex].enabled) {
                            currentlySelectedButtonIndex++;
                            if (currentlySelectedButtonIndex >= buttonList.Count) {
                                if (cycleOptions) {
                                    currentlySelectedButtonIndex = 0;
                                } else {
                                    currentlySelectedButtonIndex = LastSelectableButtonIndex();
                                }
                            }
                        }
                        Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuDown, false, SoundEffectsManager.zeroPanVectorLocation);
                    }

                    if (GlobalControls.MenuUpPressed()) {
                        currentlySelectedButtonIndex--;
                        if (currentlySelectedButtonIndex < 0) {
                            if (cycleOptions) {
                                currentlySelectedButtonIndex = buttonList.Count - 1;
                            } else {
                                currentlySelectedButtonIndex = 0;
                            }
                        }

                        while (!buttonList[currentlySelectedButtonIndex].enabled) {
                            currentlySelectedButtonIndex--;
                            if (currentlySelectedButtonIndex < 0) {
                                if (cycleOptions) {
                                    currentlySelectedButtonIndex = buttonList.Count - 1;
                                } else {
                                    currentlySelectedButtonIndex = FirstSelectableButtonIndex();
                                }
                            }
                        }
                        Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuUp, false, SoundEffectsManager.zeroPanVectorLocation);
                    }

                    if (PlayerControls.MousePositionMoved()) {
                        for (int i = 0; i < buttonList.Count; i++) {
                            if (buttonList[i].BoundingRect().Contains(PlayerControls.MousePosition()) && buttonList[i].enabled && buttonList[i].inScrollView) {
                                currentlySelectedButtonIndex = i;
                            }
                        }
                    }

                    for (int i = 0; i < buttonList.Count; i++) {
                        buttonList[i].selected = false;
                    }

                    if (currentlySelectedButtonIndex < buttonList.Count) {
                        buttonList[currentlySelectedButtonIndex].selected = true;
                    }

                    for (int i = 0; i < buttonList.Count; i++) {
                        buttonList[i].Update(gameTime);
                    }
                }
            }
        }
        
        private bool PointInRect(Vector2 mousePosition, Rectangle rect) {
            if (mousePosition.X <= rect.X)
                return false;
            if (mousePosition.X >= rect.X + rect.Width)
                return false;
            if (mousePosition.Y <= rect.Y)
                return false;
            if (mousePosition.Y >= rect.Y + rect.Height)
                return false;

            return true;
        }

        protected MenuSelection GetCurrentlySelectedButton() {
            return buttonList[currentlySelectedButtonIndex];
        }

        public virtual void ButtonClicked(MenuButton button) {
            //Do nothing
        }

        public virtual void ButtonSideScrollScrolled(MenuButtonSideScroll button, int direction) {
            //Do nothing
        }

        protected abstract void BackPressed();

        public virtual void Draw(SpriteBatch spriteBatch) {
            foreach (MenuImage image in imageList) {
                image.Draw(spriteBatch);
            }
            foreach (MenuSelection button in buttonList) {
                if (currentSubMenuScreenIndex >= 0) {
                    button.SetColor(CustomColors.darkerGray);
                } else {
                    button.SetColor(Color.White);
                }
                button.Draw(spriteBatch);
            }
            foreach (MenuLabel label in labelList) {
                label.Draw(spriteBatch);
            }
            if (currentSubMenuScreenIndex >= 0) {
                subMenues[currentSubMenuScreenIndex].Draw(spriteBatch);
            }
        }
        
        private int FirstSelectableButtonIndex() {
            for (int i = 0; i < buttonList.Count; i++) {
                if (buttonList[i].enabled) {
                    return i;
                }
            }
            return 0;
        }
        
        private int LastSelectableButtonIndex() {
            for (int i = buttonList.Count - 1; i >= 0; i++) {
                if (buttonList[i].enabled) {
                    return i;
                }
            }
            return 0;
        }
    }
}
