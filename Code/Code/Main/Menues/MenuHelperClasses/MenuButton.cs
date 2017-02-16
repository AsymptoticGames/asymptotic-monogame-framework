using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
    public class MenuButton : MenuSelection{

        public static Vector2 buttonSize = new Vector2(400, 80);
        
        public MenuLabel buttonTextLabel;

        public MenuButton(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText) : base(_position, _menuScreen, _size, buttonSize) {
            texture = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-button-blank");
            buttonTextLabel = new MenuLabel(
                         Globals.content.Load<SpriteFont>("Fonts/candara-bold-28"),
                         new Vector2(_position.X + _size.X / 2, _position.Y + _size.Y / 2),
                         _buttonText,
                         CustomColors.veryLightOrange
                     );
        }

        public MenuButton(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText, SpriteFont font) : base(_position, _menuScreen, _size, buttonSize) {
            texture = Globals.content.Load<Texture2D>("Menues/MenuHelpers/menu-button-blank");
            buttonTextLabel = new MenuLabel(
                         font,
                         new Vector2(_position.X + _size.X / 2, _position.Y + _size.Y / 2),
                         _buttonText,
                         CustomColors.veryLightOrange
                     );
        }

        public MenuButton(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText, Texture2D _texture) : base(_position, _menuScreen, _size, buttonSize) {
            texture = _texture;
            buttonTextLabel = new MenuLabel(
                         Globals.content.Load<SpriteFont>("Fonts/candara-bold-28"),
                         new Vector2(_position.X + _size.X / 2, _position.Y + _size.Y / 2),
                         _buttonText,
                         CustomColors.veryLightOrange
                     );
        }

        public override void Update(GameTime gameTime) {
            if (selected && menuScreen != null && enabled && inScrollView &&
                    ((PlayerControls.MouseLeftPressed() && BoundingRect().Contains(PlayerControls.MousePosition())) ||
                    GlobalControls.ConfirmPressed() || PlayerControls.ConfirmPressed(ControlsConfig.keyboardControllerIndex))) {
                SelectionClicked();
                menuScreen.ButtonClicked(this);
                Globals.soundEffectsManager.PlaySoundEffect(SoundEffects.MenuConfirm, false, SoundEffectsManager.zeroPanVectorLocation);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            if(buttonTextLabel.text != "") {
                buttonTextLabel.centerPosition = new Vector2(position.X + size.X / 2, position.Y + size.Y / 2);
                buttonTextLabel.transparency = transparency;
                buttonTextLabel.enabled = enabled;
                buttonTextLabel.Draw(spriteBatch);
            }
        }

    }
}
