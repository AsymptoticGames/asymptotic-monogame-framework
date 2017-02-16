using System;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework { 

    public static class GlobalTextures {

        public static Texture2D waitingForControlsInput;

        public static void LoadContent() {
            waitingForControlsInput = Globals.content.Load<Texture2D>("Controls/waiting-for-input");
        }

        public static void UnloadContent() {

        }
    }
}
