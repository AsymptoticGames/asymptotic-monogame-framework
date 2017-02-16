using System;
using System.Collections.Generic;

namespace AsymptoticMonoGameFramework {

    public static class ResolutionConfig {

        public static Tuple<int, int> virtualResolution = new Tuple<int, int>(1920, 1080);
        public static List<Tuple<int, int>> resolutionOptions = new List<Tuple<int, int>>(new Tuple<int, int>[] {
                    new Tuple<int, int>(800, 600),      //0
                    new Tuple<int, int>(960, 540),      //1
                    new Tuple<int, int>(1024, 768),     //2
                    new Tuple<int, int>(1280, 720),     //3
                    new Tuple<int, int>(1280, 1024),    //4
                    new Tuple<int, int>(1366, 768),     //5
                    new Tuple<int, int>(1440, 900),     //6
                    new Tuple<int, int>(1600, 900),     //7
                    new Tuple<int, int>(1680, 1050),    //8
                    new Tuple<int, int>(1920, 1080),    //9
                    new Tuple<int, int>(2560, 1440),    //10
                    new Tuple<int, int>(3840, 2160)     //11
                });

        public static List<Tuple<int, int>> validResolutionOptions;

        public static int currentResolutionIndex;
        public static bool isFullScreen;
        
        public static Tuple<int, int> GetCurrentResolution() {
            return validResolutionOptions[currentResolutionIndex];
        }

        public static void SetResolution(int _currentResolutionIndex, bool _isFullScreen) {
            isFullScreen = _isFullScreen;
            currentResolutionIndex = _currentResolutionIndex;
            Resolution.SetResolution(validResolutionOptions[currentResolutionIndex].Item1, validResolutionOptions[currentResolutionIndex].Item2, isFullScreen);
            SaveGraphicalSettings();
        }

        public static void SetValidResolutionOptions() {
            validResolutionOptions = new List<Tuple<int, int>>();
            foreach(Tuple<int, int> option in resolutionOptions) {
                if(option.Item1 <= Resolution.GetMonitorResolution().X && option.Item2 <= Resolution.GetMonitorResolution().Y) {
                    validResolutionOptions.Add(option);
                }
            }

            /** Set resolution to 2 under valid resolution options so the entire window is in view the first time the game is loaded **/
            currentResolutionIndex = validResolutionOptions.Count - 2;
            if (currentResolutionIndex < 0) {
                currentResolutionIndex = 0;
            }

            //special case for 1920x1080 monitors, set to 1600x900
            if (validResolutionOptions[validResolutionOptions.Count - 1].Item1 == 1920 && validResolutionOptions[validResolutionOptions.Count - 1].Item2 == 1080) {
                for(int i = 0; i < validResolutionOptions.Count; i++) {
                    if(validResolutionOptions[i].Item1 == 1600 && validResolutionOptions[i].Item2 == 900) {
                        currentResolutionIndex = i;
                        break;
                    }
                }
            }
        }

        public static void SaveGraphicalSettings() {
            Globals.gameInstance.settingsManager.settings.fullScreen = isFullScreen ? "true" : "false";
            Globals.gameInstance.settingsManager.settings.resolution = GetCurrentResolution().Item1 + "x" + GetCurrentResolution().Item2;
            Globals.gameInstance.settingsManager.Save();
        }

        public static void LoadGraphicalSettings() {
            isFullScreen = (Globals.gameInstance.settingsManager.settings.fullScreen == "true");
            
            SetValidResolutionOptions();
            for(int i = 0; i < validResolutionOptions.Count; i++) {
                Tuple<int, int> option = validResolutionOptions[i];
                if(option.Item1 + "x" + option.Item2 == Globals.gameInstance.settingsManager.settings.resolution) {
                    currentResolutionIndex = i;
                    break;
                }
            }

            SetResolution(currentResolutionIndex, isFullScreen);
        }

    }
}
