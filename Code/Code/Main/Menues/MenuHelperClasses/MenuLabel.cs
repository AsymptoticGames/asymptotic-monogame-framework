using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework {

    public enum TextAlignment {
        Left,
        Right,
        Center
    };

    public class MenuLabel {

        private SpriteFont font;
        private Color color;
        private TextAlignment textAlignment;

        public Vector2 centerPosition;
        public string text;

        public bool hidden = false;
        public int stroke = 0;
        public float transparency;
        public bool enabled;

        public MenuLabel(SpriteFont _font, Vector2 _centerPosition, string _text) {
            font = _font;
            centerPosition = _centerPosition;
            text = _text;
            color = Color.Black;
            textAlignment = TextAlignment.Center;
            transparency = 1.0f;
            enabled = true;
        }

        public MenuLabel(SpriteFont _font, Vector2 _centerPosition, string _text, Color _color) {
            font = _font;
            centerPosition = _centerPosition;
            text = _text;
            color = _color;
            textAlignment = TextAlignment.Center;
            transparency = 1.0f;
            enabled = true;
        }

        public MenuLabel(SpriteFont _font, Vector2 _centerPosition, string _text, Color _color, int _stroke) {
            font = _font;
            centerPosition = _centerPosition;
            text = _text;
            color = _color;
            stroke = _stroke;
            textAlignment = TextAlignment.Center;
            transparency = 1.0f;
            enabled = true;
        }

        public MenuLabel(SpriteFont _font, Vector2 _centerPosition, string _text, Color _color, TextAlignment _textAlignment) {
            font = _font;
            centerPosition = _centerPosition;
            text = _text;
            color = _color;
            textAlignment = _textAlignment;
            transparency = 1.0f;
            enabled = true;
        }

        public void SetColor(Color _color) {
            color = _color;
        }

        public void SetFont(SpriteFont _font) {
            font = _font;
        }

        public SpriteFont GetFont() {
            return font;
        }

        public Vector2 FontOrigin() {
            if (textAlignment == TextAlignment.Right) {
                return new Vector2(SizeOfString().X, SizeOfString().Y / 2);
            } else if (textAlignment == TextAlignment.Left) {
                return new Vector2(0, SizeOfString().Y /2);
            } else {
                return new Vector2(SizeOfString().X / 2, SizeOfString().Y / 2);
            }
        }

        public Vector2 SizeOfString() {
            return HelperFunctions.SizeOfString(font, text);
        }

        public void Draw(SpriteBatch spriteBatch) {
            Color _color = enabled ? color : CustomColors.darkerGray;
            if (!hidden) {
                if(stroke > 0) {
                    spriteBatch.DrawString(font, text, centerPosition + new Vector2(-stroke, -stroke), Color.Black * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, text, centerPosition + new Vector2(stroke, -stroke), Color.Black * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, text, centerPosition + new Vector2(-stroke, stroke), Color.Black * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, text, centerPosition + new Vector2(stroke, stroke), Color.Black * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, text, centerPosition + new Vector2(-stroke, 0), Color.Black * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, text, centerPosition + new Vector2(stroke, 0), Color.Black * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, text, centerPosition + new Vector2(0, -stroke), Color.Black * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, text, centerPosition + new Vector2(0, stroke), Color.Black * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
                }
                spriteBatch.DrawString(font, text, centerPosition, _color * transparency, 0, FontOrigin(), 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
