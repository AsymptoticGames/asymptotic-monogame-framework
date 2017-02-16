using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
    public class MenuButtonSideScroll : MenuButton {

        public int selectionIndex;

        private string[] selectionOptions;
        private Texture2D selectionArrow;
        private int selectionArrowColor;

        public MenuButtonSideScroll(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string[] _selectionOptions, int _selectionIndex) : base(_position, _menuScreen, _size, _selectionOptions[0]) {
            selectionOptions = _selectionOptions;
            selectionIndex = _selectionIndex;
            SetTextLabelToSelectionIndex();
            selectionArrow = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-button-side-scroll-arrow");
        }

        public MenuButtonSideScroll(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string[] _selectionOptions, int _selectionIndex, SpriteFont font) : base(_position, _menuScreen, _size, _selectionOptions[0], font) {
            selectionOptions = _selectionOptions;
            selectionIndex = _selectionIndex;
            SetTextLabelToSelectionIndex();
            selectionArrow = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-button-side-scroll-arrow");
        }
        
        public Rectangle LeftArrowBoundingRect() {
            return new Rectangle((int)position.X + 16, (int)position.Y + 24, 32, 32);
        }

        public Rectangle RightArrowBoundingRect() {
            return new Rectangle((int)position.X + (int)buttonSize.X - 16 - 32, (int)position.Y + 24, 32, 32);
        }

        public string GetSelectedValue() {
            return selectionOptions[selectionIndex];
        }

        public void LeftArrowPressed() {
            selectionIndex--;
            SetTextLabelToSelectionIndex();
            selectionArrowColor = -3;
            menuScreen.ButtonSideScrollScrolled(this, -1);
            Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuLeft, false, SoundEffectsManager.zeroPanVectorLocation);
        }

        public void RightArrowPressed() {
            selectionIndex++;
            SetTextLabelToSelectionIndex();
            selectionArrowColor = 3;
            menuScreen.ButtonSideScrollScrolled(this, 1);
            Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuRight, false, SoundEffectsManager.zeroPanVectorLocation);
        }

        private void SetTextLabelToSelectionIndex() {
            if(selectionIndex > selectionOptions.Length - 1) {
                selectionIndex = 0;
            }else if(selectionIndex < 0) {
                selectionIndex = selectionOptions.Length - 1;
            }
            buttonTextLabel.text = selectionOptions[selectionIndex];
        }

        public override void Update(GameTime gameTime) {
            if (selected && menuScreen != null && enabled && inScrollView) {
                if (((PlayerControls.MouseLeftPressed() && RightArrowBoundingRect().Contains(PlayerControls.MousePosition())) ||
                        GlobalControls.MenuRightPressed() || PlayerControls.MenuRightPressed(ControlsConfig.keyboardControllerIndex))) {
                    RightArrowPressed();
                }
                if (((PlayerControls.MouseLeftPressed() && LeftArrowBoundingRect().Contains(PlayerControls.MousePosition())) ||
                        GlobalControls.MenuLeftPressed() || PlayerControls.MenuLeftPressed(ControlsConfig.keyboardControllerIndex))) {
                    LeftArrowPressed();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            if (selectionArrowColor < 0) {
                spriteBatch.Draw(selectionArrow, LeftArrowBoundingRect(), null, Color.White * 0.3f * transparency, 0, Vector2.Zero, SpriteEffects.None, 0); //left arrow
                selectionArrowColor++;
            } else {
                spriteBatch.Draw(selectionArrow, LeftArrowBoundingRect(), null, CustomColors.veryLightOrange * transparency, 0, Vector2.Zero, SpriteEffects.None, 0); //left arrow
            }

            if (selectionArrowColor > 0) {
                spriteBatch.Draw(selectionArrow, RightArrowBoundingRect(), null, Color.White * 0.3f * transparency, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0); //right arrow
                selectionArrowColor--;
            } else {
                spriteBatch.Draw(selectionArrow, RightArrowBoundingRect(), null, CustomColors.veryLightOrange * transparency, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0); //right arrow
            }
        }
    }
}
