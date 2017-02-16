using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework
{
    public static class HelperFunctions {
        public static double RoundFloat(float value, int decPlaces) {
            double baseValue = Math.Pow(10f, decPlaces);
            return Math.Round(value * baseValue) / baseValue;
        }

        public static bool RectInRect(Rectangle rect1, Rectangle rect2) {
            if (rect1.X + rect1.Size.X < rect2.X)
                return false;
            if (rect1.X > rect2.X + rect2.Size.X)
                return false;
            if (rect1.Y + rect1.Size.Y < rect2.Y)
                return false;
            if (rect1.Y > rect2.Y + rect2.Size.Y)
                return false;

            return true;
        }

        public static bool ObjectInRect(Vector2 position, Vector2 size, Rectangle rectangle) {
            if (position.X + size.X < rectangle.X)
                return false;
            if (position.X > rectangle.X + rectangle.Size.X)
                return false;
            if (position.Y + size.Y < rectangle.Y)
                return false;
            if (position.Y > rectangle.Y + rectangle.Size.Y)
                return false;

            return true;
        }

        public static Vector2 SizeOfString(SpriteFont font, string text) {
            return font.MeasureString(text);
        }

        public static float Clamp(float value, float min, float max) {
            if(value < min) {
                return min;
            }else if(value > max) {
                return max;
            } else {
                return value;
            }
        }
    }
}
