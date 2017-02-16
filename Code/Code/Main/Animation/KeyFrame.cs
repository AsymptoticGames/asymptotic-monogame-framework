using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsymptoticMonoGameFramework
{
   public  class KeyFrame
    {
        private Rectangle source;

        public Rectangle Source{
            get { return source; }
        }

        public int Width{
            get { return source.Width; }
        }

        public int Height{
            get { return source.Height; }
        }

        public KeyFrame(int x, int y, int width, int height){
            source = new Rectangle(x, y, width, height);
        }
    }
}
