using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AsymptoticMonoGameFramework {

    public class ResolutionSelectionMenu : ScrollingMenuScreen {

        public ResolutionSelectionMenu() {
            Setup();
        }

        public ResolutionSelectionMenu(MenuScreen _parentMenu) : base(_parentMenu) {
            Setup();
        }

        public void SetIndex(int index) {
            currentlySelectedButtonIndex = index;
        }

        private void Setup() {
            scrollAmount = 70;
            menuSize = new Vector2(960, 540);
            menuPadding = 60;
            buttonSize = new Vector2(300, 60);
            buttonPadding = 5;
        }

        public override void LoadContent() {
            base.LoadContent();
            
            for (int i = 0; i < ResolutionConfig.validResolutionOptions.Count; i++) {
                AddButton(new MenuButton(
                    new Vector2(),
                    this,
                    buttonSize,
                    ResolutionConfig.validResolutionOptions[i].Item1 + " x " + ResolutionConfig.validResolutionOptions[i].Item2
                ));
            }
            SetIndex(ResolutionConfig.currentResolutionIndex);
        }

        public override void UnloadContent() {

        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void ButtonClicked(MenuButton button) {
            base.ButtonClicked(button);
            for (int i = 0; i < buttonList.Count; i++) {
                if(button == buttonList[i]) {
                    ((GraphicalSettingsMenu) parentMenu).SetCurrentResolutionIndex(i);
                    parentMenu.CloseSubMenu();
                }
            }
        }

        protected override void BackPressed() {
            SetIndex(((GraphicalSettingsMenu)parentMenu).currentResolutionIndex);
            parentMenu.CloseSubMenu();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }
    }
}
