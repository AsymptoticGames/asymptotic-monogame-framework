using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework{

    public static class DefaultControls {

        public static string defaultPresetString = "Default";
        public static string customPresetString = "Custom";

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

        public static Dictionary<string, object> gamepadControlsPreset2 = new Dictionary<string, object> {
            {moveUpTKey, Buttons.DPadUp },
            {moveDownTKey, Buttons.DPadDown },
            {moveLeftTKey, Buttons.DPadLeft },
            {moveRightTKey, Buttons.DPadRight },
            {joystickExampleTKey, JoystickOptions.Left },
            {booleanExampleTKey, ToggleOptions.True },
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

        public static Dictionary<string, object> keyboardControlsPreset2 = new Dictionary<string, object> {
            {moveUpTKey, Keys.W },
            {moveDownTKey, Keys.S },
            {moveLeftTKey, Keys.A },
            {moveRightTKey, Keys.D },
            {mouseClickExampleTkey, MouseClickOptions.RightClick },
            {booleanExampleTKey, ToggleOptions.True },
            {confirmTKey, Keys.Enter },
            {declineTKey, Keys.Back },
            {pauseTKey, Keys.Escape }
        };

        public static Dictionary<string, Dictionary<string, object>> gamepadPresets;
        public static Dictionary<string, Dictionary<string, object>> keyboardPresets;

        public static string[] currentGamepadPreset = new string[ControlsConfig.numGamepads];
        public static string[] currentKeyboardPreset = new string[1];

        public static void SetupPresets() {
            gamepadPresets = new Dictionary<string, Dictionary<string, object>> {
                { defaultPresetString, gamepadControls },
                { "Preset 2", gamepadControlsPreset2 },
                { customPresetString, gamepadControls }
            };
            for (int i = 0; i < ControlsConfig.numGamepads; i++) {
                currentGamepadPreset[i] = defaultPresetString;
            }

            keyboardPresets = new Dictionary<string, Dictionary<string, object>> {
                { defaultPresetString, keyboardControls },
                { "Preset 2", keyboardControlsPreset2 },
                { customPresetString, keyboardControls }
            };
            currentKeyboardPreset[0] = defaultPresetString;
        }

        public static bool GamepadPresetIsCustom(int playerNumber) {
            return currentGamepadPreset[playerNumber] == customPresetString;
        }

        public static bool KeyboardPresetIsCustom() {
            return currentKeyboardPreset[0] == customPresetString;
        }

        public static void IncrementGamepadPreset(int playerNumber) {
            int currentIndex = gamepadPresets.Keys.ToList().IndexOf(currentGamepadPreset[playerNumber]);
            currentIndex++;
            if (currentIndex >= gamepadPresets.Keys.ToList().Count) {
                currentIndex = 0;
            }
            currentGamepadPreset[playerNumber] = gamepadPresets.Keys.ToList()[currentIndex];
        }

        public static void DecrementGamepadPreset(int playerNumber) {
            int currentIndex = gamepadPresets.Keys.ToList().IndexOf(currentGamepadPreset[playerNumber]);
            currentIndex--;
            if (currentIndex < 0) {
                currentIndex = gamepadPresets.Keys.ToList().Count - 1;
            }
            currentGamepadPreset[playerNumber] = gamepadPresets.Keys.ToList()[currentIndex];
        }

        public static void IncrementKeyboardPreset() {
            int currentIndex = keyboardPresets.Keys.ToList().IndexOf(currentKeyboardPreset[0]);
            currentIndex++;
            if (currentIndex >= keyboardPresets.Keys.ToList().Count) {
                currentIndex = 0;
            }
            currentKeyboardPreset[0] = keyboardPresets.Keys.ToList()[currentIndex];
        }

        public static void DecrementKeyboardPreset() {
            int currentIndex = keyboardPresets.Keys.ToList().IndexOf(currentKeyboardPreset[0]);
            currentIndex--;
            if (currentIndex < 0) {
                currentIndex = keyboardPresets.Keys.ToList().Count - 1;
            }
            currentKeyboardPreset[0] = keyboardPresets.Keys.ToList()[currentIndex];
        }
    }
}
