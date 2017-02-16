using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework {
    public class MenuButtonWithImage : MenuButton {

        protected Vector2 sideImageSize = new Vector2(50f, 50f);
        private readonly int widthPadding = 10;

        protected Texture2D image;

        public MenuButtonWithImage(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText, Texture2D _image) : base(_position, _menuScreen, _size, _buttonText) {
            SetImage(_image);
        }

        public MenuButtonWithImage(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText, SpriteFont font, Texture2D _image) : base(_position, _menuScreen, _size, _buttonText, font) {
            SetImage(_image);
        }

        public void SetImage(Texture2D _image) {
            image = _image;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            Rectangle sourceRect = selected ? SelectedSourceRect() : UnselectedSourceRect();
            Color _color = enabled ? color : CustomColors.darkerGray;
            float _transparency = transparency;
            if (selectionColor > 0) {
                selectionColor--;
                _color = Color.White;
                _transparency = 0.3f;
            }

            spriteBatch.Draw(texture, BoundingRect(), sourceRect, _color * _transparency, 0, new Vector2(0, 0), SpriteEffects.None, 0);

            Vector2 sizeOfLabel = HelperFunctions.SizeOfString(buttonTextLabel.GetFont(), buttonTextLabel.text);
            if (buttonTextLabel.text != "") {
                buttonTextLabel.centerPosition = new Vector2(position.X + sizeOfLabel.X / 2 + widthPadding, position.Y + size.Y / 2);
                buttonTextLabel.transparency = _transparency;
                buttonTextLabel.enabled = enabled;
                buttonTextLabel.Draw(spriteBatch);
            }

            spriteBatch.Draw(image, new Rectangle((int)(position.X + size.X - sideImageSize.X - widthPadding), (int)(position.Y + ((size.Y - sideImageSize.Y) / 2)), (int)sideImageSize.X, (int)sideImageSize.Y), _color * _transparency);
        }
    }
}
