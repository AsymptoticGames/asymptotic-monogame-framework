using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework {

    /*** Update IsInputJoystickOptions() if changed ***/
    public enum JoystickOptions {
        Left = -1,
        Right = -2
    };

    /*** Update IsInputToggleOptions() if changed ***/
    public enum ToggleOptions {
        True = -10,
        False = -11
    }

    /*** Update IsInputMouseClickOptions() if changed ***/
    public enum MouseClickOptions {
        LeftClick = -1,
        RightClick = -2,
        MiddleClick = -3
    }

    public static class ControlsConfig{
        
        /** HELPER VARIABLES **/
        public static int numGamepads = 4;   /*** IMPORTANT: If changing numGamepads, uncomment DeleteSettingsFile in SettingsManager.cs before running ***/
        public static int keyboardControllerIndex = -1;

        /** CONTROLS **/
        public static Dictionary<string, int[]> gamepadControls = new Dictionary<string, int[]>();
        public static Dictionary<string, int[]> keyboardControls = new Dictionary<string, int[]>();

        /** HELPER FUNCTIONS **/
        public static int[] GetAllControllerIndexes() {
            int[] returnValue = new int[numGamepads + 1];
            for (int i = 0; i < numGamepads; i++) {
                returnValue[i] = i;
            }
            returnValue[numGamepads] = keyboardControllerIndex;
            return returnValue;
        }

        public static int[] GetAllGamepadControllerIndexes() {
            int[] returnValue = new int[numGamepads];
            for (int i = 0; i < numGamepads; i++) {
                returnValue[i] = i;
            }
            return returnValue;
        }

        public static bool IsInputToggleOptions(int input) {
            return input == (int)ToggleOptions.False || input == (int)ToggleOptions.True;
        }

        public static bool IsInputJoystickOptions(int input) {
            return input == (int)JoystickOptions.Left || input == (int)JoystickOptions.Right;
        }

        public static bool IsInputMouseClickOptions(int input) {
            return input == (int)MouseClickOptions.LeftClick || input == (int)MouseClickOptions.MiddleClick || input == (int)MouseClickOptions.RightClick;
        }

        public static JoystickOptions GetNextJoystickOption(JoystickOptions joystickOption) {
            if(joystickOption == JoystickOptions.Left) {
                return JoystickOptions.Right;
            }else {
                return JoystickOptions.Left;
            }
        }

        public static void ResetToDefault(int playerNumber) {
            if(playerNumber == keyboardControllerIndex) {
                ResetKeyboardToDefault();
            } else {
                ResetGamepadToDefault(playerNumber);
            }
        }

        private static void ResetGamepadToDefault(int playerNumber) {
            int controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.gamepadControls) {
                for (int i = 0; i < numGamepads; i++) {
                    gamepadControls[entry.Key][i] = (int)DefaultControls.gamepadControls[entry.Key];
                }
                controlsIndex++;
            }
        }

        private static void ResetKeyboardToDefault() {
            int controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.keyboardControls) {
                keyboardControls[entry.Key][0] = (int)DefaultControls.keyboardControls[entry.Key];
                controlsIndex++;
            }
        }

        public static void ApplyControlsToAllGamepads(int playerNumber) {
            int controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.gamepadControls) {
                for (int playerIndex = 0; playerIndex < numGamepads; playerIndex++) {
                    if (playerIndex != playerNumber) {
                        gamepadControls[entry.Key][playerIndex] = gamepadControls[entry.Key][playerNumber];
                    }
                }
                controlsIndex++;
            }
        }

        public static void LoadControlsSettings() {
            SetupInitialControls();
            LoadGamepadControls();
            LoadKeyboardControls();
        }

        private static void SetupInitialControls() {
            int controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.gamepadControls) {
                int[] playerControlsTValues = new int[numGamepads];
                for (int playerIndex = 0; playerIndex < numGamepads; playerIndex++) {
                    if (entry.Value is bool) {
                        playerControlsTValues[playerIndex] = (bool)entry.Value ? (int)ToggleOptions.True : (int)ToggleOptions.False;
                    } else {
                        playerControlsTValues[playerIndex] = (int)DefaultControls.gamepadControls[entry.Key];
                    }
                }
                gamepadControls.Add(entry.Key, playerControlsTValues);
                controlsIndex++;
            }

            controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.keyboardControls) {
                int[] playerControlsTValues = new int[1];
                playerControlsTValues[0] = (int)DefaultControls.keyboardControls[entry.Key];
                keyboardControls.Add(entry.Key, playerControlsTValues);
                controlsIndex++;
            }
        }

        private static void LoadGamepadControls() {
            int controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.gamepadControls) {
                for (int i = 0; i < numGamepads; i++) {
                    gamepadControls[entry.Key][i] = Globals.gameInstance.settingsManager.settings.gamepadControlsTValues[controlsIndex][i];
                }
                controlsIndex++;
            }
        }

        private static void LoadKeyboardControls() {
            int controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.keyboardControls) {
                keyboardControls[entry.Key][0] = Globals.gameInstance.settingsManager.settings.keyboardControlsTValues[controlsIndex][0];
                controlsIndex++;
            }
        }

        public static void SaveControlsSettings() {
            SaveGamepadControls();
            SaveKeyboardControls();
            Globals.gameInstance.settingsManager.Save();
        }

        private static void SaveGamepadControls() {
            int controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.gamepadControls) {
                for (int i = 0; i < numGamepads; i++) {
                    Globals.gameInstance.settingsManager.settings.gamepadControlsTValues[controlsIndex][i] = gamepadControls[entry.Key][i];
                }
                controlsIndex++;
            }
        }

        private static void SaveKeyboardControls() {
            int controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.keyboardControls) {
                Globals.gameInstance.settingsManager.settings.keyboardControlsTValues[controlsIndex][0] = keyboardControls[entry.Key][0];
                controlsIndex++;
            }
        }
    }
}
