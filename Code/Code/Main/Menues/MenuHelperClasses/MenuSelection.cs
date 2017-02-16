using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework {

    public abstract class MenuSelection {

        public Texture2D texture;
        public Vector2 position;

        protected MenuScreen menuScreen;
        protected Vector2 size;
        protected Vector2 imageSize;
        protected Color color;

        public bool inScrollView;
        public bool selected;
        public bool enabled;
        public float transparency;

        protected int selectionColor;

        public MenuSelection(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, Vector2 _imageSize) {
            position = _position;
            menuScreen = _menuScreen;
            size = _size;
            imageSize = _imageSize;
            
            color = Color.White;
            inScrollView = true;
            selected = false;
            enabled = true;
            transparency = 1.0f;
        }

        public abstract void Update(GameTime gameTime);

        public void SelectionClicked() {
            selectionColor = 2;
        }

        public void SetColor(Color _color) {
            color = _color;
        }

        public virtual Rectangle BoundingRect() {
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        protected virtual Rectangle UnselectedSourceRect() {
            return new Rectangle(0, 0, (int)imageSize.X, (int)imageSize.Y);
        }

        protected virtual Rectangle SelectedSourceRect() {
            return new Rectangle(0, (int)imageSize.Y, (int)imageSize.X, (int)imageSize.Y);
        }
        
        public virtual void Draw(SpriteBatch spriteBatch) {
            Rectangle sourceRect = selected ? SelectedSourceRect() : UnselectedSourceRect();
            Color _color = enabled ? color : CustomColors.darkerGray;
            float _transparency = transparency;
            if(selectionColor > 0) {
                selectionColor--;
                _color = Color.White;
                _transparency = 0.5f;
            }
            spriteBatch.Draw(texture, BoundingRect(), sourceRect, _color * _transparency, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }

    }
}
