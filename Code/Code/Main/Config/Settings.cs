using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework
{
    public class Settings {

        /** Graphical Settings **/
        public string fullScreen = "false";
        public string resolution = "";

        /** Audio Settings **/
        public int soundEffectVolume = 50;
        public int musicVolume = 50;

        /** Controls Settings **/
        public int[][] gamepadControlsTValues = new int[DefaultControls.gamepadControls.Count][];
        public int[][] keyboardControlsTValues = new int[DefaultControls.keyboardControls.Count][];

        public Settings() {
            int controlsIndex = 0;
            foreach(KeyValuePair<string, object> entry in DefaultControls.gamepadControls) {
                int[] playerControlsTValues = new int[ControlsConfig.numGamepads];
                for(int playerIndex = 0; playerIndex < ControlsConfig.numGamepads; playerIndex++) {
                    if (entry.Value is bool) {
                        playerControlsTValues[playerIndex] = (bool)entry.Value ? (int)ToggleOptions.True : (int)ToggleOptions.False;
                    } else {
                        playerControlsTValues[playerIndex] = (int)DefaultControls.gamepadControls[entry.Key];
                    }
                }
                gamepadControlsTValues[controlsIndex] = playerControlsTValues;
                controlsIndex++;
            }
            
            controlsIndex = 0;
            foreach (KeyValuePair<string, object> entry in DefaultControls.keyboardControls) {
                int[] playerControlsTValues = new int[1];
                playerControlsTValues[0] = (int)DefaultControls.keyboardControls[entry.Key];
                keyboardControlsTValues[controlsIndex] = playerControlsTValues;
                controlsIndex++;
            }
        }
    }

    public static class ArrayExtensions {
        public static void Fill<T>(this T[] originalArray, T with) {
            for (int i = 0; i < originalArray.Length; i++) {
                originalArray[i] = with;
            }
        }
    }
}
