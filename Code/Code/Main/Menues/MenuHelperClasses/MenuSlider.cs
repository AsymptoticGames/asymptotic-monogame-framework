using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework {

    public class MenuSlider : MenuSelection {

        public static Vector2 sliderSize = new Vector2(600, 60);
        public static Vector2 handleImageSize = new Vector2(20, 60);

        public float value;

        private Texture2D handleTexture;
        private float valueIncrement = 0.05f;

        private bool mouseChangingValue = false;

        public bool soundEffectOnSliderChange = false;

        public MenuSlider(Vector2 _position, MenuScreen _menuScreen, float _value) : base(_position, _menuScreen, sliderSize, sliderSize) {
            value = _value;
            texture = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-slider");
            handleTexture = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-slider-handle");
        }
        
        public override void Update(GameTime gameTime) {
            bool inGame = Globals.gameInstance.gameState == GameState.inGame;
            if (selected) {
                if (GlobalControls.MenuLeftPressed()) {
                    value -= valueIncrement;
                    if (value < 0) {
                        value = 0.0f;
                    }
                    if (soundEffectOnSliderChange) {
                        Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuSlider, false, SoundEffectsManager.zeroPanVectorLocation);
                    }
                }

                if (GlobalControls.MenuRightPressed()) {
                    value += valueIncrement;
                    if (value > 1) {
                        value = 1.0f;
                    }
                    if (soundEffectOnSliderChange) {
                        Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuSlider, false, SoundEffectsManager.zeroPanVectorLocation);
                    }
                }

                if (PlayerControls.MouseLeftPressed() && BoundingRect().Contains(PlayerControls.MousePosition())) {
                    mouseChangingValue = true;
                }

                if (mouseChangingValue && PlayerControls.MouseLeftDown()) {
                    value = (PlayerControls.MousePosition().X - position.X) / size.X;
                    if (value < 0) {
                        value = 0;
                    } else if (value > 1) {
                        value = 1;
                    }
                } else {
                    if (mouseChangingValue && soundEffectOnSliderChange) {
                        Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuSlider, false, SoundEffectsManager.zeroPanVectorLocation);
                    }
                    mouseChangingValue = false;
                }
            }
        }

        private Rectangle HandleBoundingRect() {
            return new Rectangle((int)(position.X + size.X * value - handleImageSize.X / 2), (int)position.Y, (int)handleImageSize.X, (int)handleImageSize.Y);
        }

        private Rectangle HandleUnselectedSourceRect() {
            return new Rectangle(0, 0, (int)handleImageSize.X, (int)handleImageSize.Y);
        }

        private Rectangle HandleSelectedSourceRect() {
            return new Rectangle(0, (int)handleImageSize.Y, (int)handleImageSize.X, (int)handleImageSize.Y);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            Rectangle sourceRect = selected ? HandleSelectedSourceRect() : HandleUnselectedSourceRect();
            Color _color = enabled ? color : CustomColors.darkerGray;
            spriteBatch.Draw(handleTexture, HandleBoundingRect(), sourceRect, _color, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }

    }
}
