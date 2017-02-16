using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework{

    public abstract class ScrollingMenuScreen : MenuScreen {

        private Texture2D menuBackground;
        private Texture2D upArrow;
        private Texture2D downArrow;

        private bool scrolledToBottom = false;
        private bool scrolledToTop = false;

        protected int scrollAmount;
        protected Vector2 menuSize;
        protected int menuPadding;
        protected Vector2 buttonSize;
        protected int buttonPadding;

        private int selectionArrowColor = 0;

        public ScrollingMenuScreen() : base() {
            Setup();
            cycleOptions = false;
        }

        public ScrollingMenuScreen(MenuScreen _parentMenu) : base(_parentMenu) {
            Setup();
            cycleOptions = false;
        }

        private void Setup() {
            scrollAmount = 100;
            menuSize = new Vector2(1280, 720);
            menuPadding = 60;
            buttonSize = MenuButton.buttonSize;
            buttonPadding = 10;
        }

        public override void LoadContent() {
            menuBackground = Globals.content.Load<Texture2D>("Menues/PauseMenu/pause-menu-background");
            downArrow = Globals.content.Load<Texture2D>("Menues/MenuHelpers/scroll-menu-arrow");
            upArrow = Globals.content.Load<Texture2D>("Menues/MenuHelpers/scroll-menu-arrow");
        }

        public override void UnloadContent() {

        }
        
        protected virtual void AddButton(MenuButton button) {
            buttonList.Add(button);
            button.position = new Vector2(ResolutionConfig.virtualResolution.Item1 / 2 - buttonSize.X / 2, ContentRect().Y + buttonPadding + (buttonSize.Y + buttonPadding * 2) * (buttonList.Count - 1));
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if ((PlayerControls.MouseLeftPressed() && DownArrowBoundingRect().Contains(PlayerControls.MousePosition()))) {
                currentlySelectedButtonIndex++;
                if(currentlySelectedButtonIndex > buttonList.Count - 1) {
                    currentlySelectedButtonIndex = buttonList.Count - 1;
                }
            }
            if ((PlayerControls.MouseLeftPressed() && UpArrowBoundingRect().Contains(PlayerControls.MousePosition()))) {
                currentlySelectedButtonIndex--;
                if(currentlySelectedButtonIndex < 0) {
                    currentlySelectedButtonIndex = 0;
                }
            }

            while (buttonList[currentlySelectedButtonIndex].position.Y + buttonSize.Y > ContentRect().Y + ContentRect().Size.Y) {
                ScrollDown();
            }
            while (buttonList[currentlySelectedButtonIndex].position.Y < ContentRect().Y) {
                ScrollUp();
            }

            scrolledToTop = (buttonList[0].position.Y >= ContentRect().Y);
            scrolledToBottom = (buttonList[buttonList.Count - 1].position.Y + buttonSize.Y <= ContentRect().Y + ContentRect().Size.Y);
        }

        private void ScrollDown() {
            ScrollAllElements(scrollAmount * -1);
            selectionArrowColor = 3;
        }

        private void ScrollUp() {
            ScrollAllElements(scrollAmount);
            selectionArrowColor = -3;
        }

        private void ScrollAllElements(int value) {
            foreach(MenuButton button in buttonList) {
                button.position.Y += value;
            }
            foreach(MenuLabel label in labelList) {
                label.centerPosition.Y += value;
            }
            foreach(MenuImage image in imageList) {
                image.position.Y += value;
            }
        }

        protected Rectangle ContentRect() {
            return new Rectangle((int)(ResolutionConfig.virtualResolution.Item1 / 2 - menuSize.X / 2), (int)(ResolutionConfig.virtualResolution.Item2 / 2 - menuSize.Y / 2 + menuPadding), (int)menuSize.X, (int)menuSize.Y - menuPadding * 2);
        }

        protected Rectangle BoundingRect() {
            return new Rectangle((int)(ResolutionConfig.virtualResolution.Item1 / 2 - menuSize.X / 2), (int)(ResolutionConfig.virtualResolution.Item2 / 2 - menuSize.Y / 2), (int)menuSize.X, (int)menuSize.Y);
        }
        
        private Rectangle UpArrowBoundingRect() {
            return new Rectangle((int)ResolutionConfig.virtualResolution.Item1 / 2 - 20, BoundingRect().Y + 25, 40, 25);
        }

        private Rectangle DownArrowBoundingRect() {
            return new Rectangle((int)ResolutionConfig.virtualResolution.Item1 / 2 - 20, BoundingRect().Y + BoundingRect().Size.Y - 50, 40, 25);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(menuBackground, BoundingRect(), currentSubMenuScreenIndex >= 0 ? CustomColors.darkerGray : Color.White);
            if (!scrolledToTop) {
                if (selectionArrowColor < 0) {
                    spriteBatch.Draw(upArrow, UpArrowBoundingRect(), Color.White * 0.3f);
                    selectionArrowColor++;
                } else {
                    spriteBatch.Draw(upArrow, UpArrowBoundingRect(), currentSubMenuScreenIndex >= 0 ? CustomColors.darkerGray : Color.White);
                }
            }
            if (!scrolledToBottom) {
                if (selectionArrowColor > 0) {
                    spriteBatch.Draw(downArrow, DownArrowBoundingRect(), null, Color.White * 0.3f, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                    selectionArrowColor--;
                } else {
                    spriteBatch.Draw(downArrow, DownArrowBoundingRect(), null, currentSubMenuScreenIndex >= 0 ? CustomColors.darkerGray : Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                }
            }
            
            foreach (MenuImage image in imageList) {
                if (image.position.Y + image.GetSize().Y <= ContentRect().Y + ContentRect().Size.Y && image.position.Y >= ContentRect().Y) {
                    image.Draw(spriteBatch);
                }
            }
            foreach (MenuSelection button in buttonList) {
                if (button.position.Y + buttonSize.Y <= ContentRect().Y + ContentRect().Size.Y && button.position.Y >= ContentRect().Y) {
                    if (currentSubMenuScreenIndex >= 0) {
                        button.SetColor(CustomColors.darkerGray);
                    } else {
                        button.SetColor(Color.White);
                    }
                    button.Draw(spriteBatch);
                    button.inScrollView = true;
                } else {
                    button.inScrollView = false;
                }
            }
            foreach (MenuLabel label in labelList) {
                if (label.centerPosition.Y + label.SizeOfString().Y / 2 <= ContentRect().Y + ContentRect().Size.Y && label.centerPosition.Y - label.SizeOfString().Y / 2 >= ContentRect().Y) {
                    label.Draw(spriteBatch);
                }
            }
            if (currentSubMenuScreenIndex >= 0) {
                subMenues[currentSubMenuScreenIndex].Draw(spriteBatch);
            }
        }

    }
}
