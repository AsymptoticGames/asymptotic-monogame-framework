using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework {

    public class MenuImage {

        public Vector2 position;
        private Texture2D texture;
        private Vector2 size;
        private Color color;

        public bool hidden = false;
        public bool enabled = true;

        public MenuImage(Texture2D _texture, Vector2 _position, Vector2 _size) {
            texture = _texture;
            position = _position;
            size = _size;
            color = Color.White;
        }

        public MenuImage(Texture2D _texture, Vector2 _position, Vector2 _size, Color _color) {
            texture = _texture;
            position = _position;
            size = _size;
            color = _color;
        }

        public void SetColor(Color _color) {
            color = _color;
        }

        public void SetSize(Vector2 _size) {
            size = _size;
        }

        public Vector2 GetSize() {
            return size;
        }

        public Rectangle BoundingRect() {
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (!hidden) {
                Color _color = enabled ? color : CustomColors.darkerGray;
                spriteBatch.Draw(texture, BoundingRect(), null, _color, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            }
        }
    }
}
