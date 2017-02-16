using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework{

    public static class DefaultControls {

        /*******                                                                                                                *******\
                        When changing these controls, you must delete the current settings file on your hard drive.
                        You can do this in SettingsManager.cs and uncommenting the function call to DeleteSettingsFile()
                        Or you can do it manually
        \*******                                                                                                                *******/
        
        public static string moveUpTKey = "Move Up";
        public static string moveDownTKey = "Move Down";
        public static string moveLeftTKey = "Move Left";
        public static string moveRightTKey = "Move Right";
        public static string joystickExampleTKey = "Joystick Example";
        public static string booleanExampleTKey = "Boolean Example";
        public static string mouseClickExampleTkey = "Mouse Click Example";
        public static string confirmTKey = "Confirm";
        public static string declineTKey = "Decline";
        public static string pauseTKey = "Pause";

        public static Dictionary<string, object> gamepadControls = new Dictionary<string, object> {
            {moveUpTKey, Buttons.DPadUp },
            {moveDownTKey, Buttons.DPadDown },
            {moveLeftTKey, Buttons.DPadLeft },
            {moveRightTKey, Buttons.DPadRight },
            {joystickExampleTKey, JoystickOptions.Right },
            {booleanExampleTKey, ToggleOptions.False },
            {confirmTKey, Buttons.A },
            {declineTKey, Buttons.B },
            {pauseTKey, Buttons.Start }
        };

        public static Dictionary<string, object> keyboardControls = new Dictionary<string, object> {
            {moveUpTKey, Keys.W },
            {moveDownTKey, Keys.S },
            {moveLeftTKey, Keys.A },
            {moveRightTKey, Keys.D },
            {mouseClickExampleTkey, MouseClickOptions.LeftClick },
            {booleanExampleTKey, ToggleOptions.False },
            {confirmTKey, Keys.Enter },
            {declineTKey, Keys.Back },
            {pauseTKey, Keys.Escape }
        };
    }
}
