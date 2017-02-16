using System;

namespace AsymptoticMonoGameFramework
{
    public static class GlobalControls
    {   
        public static bool ConfirmPressed() {
            foreach(int controllerIndex in ControlsConfig.GetAllControllerIndexes()) { 
                if (PlayerControls.ConfirmPressed(controllerIndex)) {
                    return true;
                }
            }
            return false;
        }

        public static bool DeclinePressed() {
            foreach (int controllerIndex in ControlsConfig.GetAllControllerIndexes()) {
                if (PlayerControls.DeclinePressed(controllerIndex)) {
                    return true;
                }
            }
            return false;
        }

        public static bool PausePressed() {
            if (CurrentlySettingControl()) {
                //When setting control in controls menu, don't want to unpause the game
                return false;
            }
            foreach (int controllerIndex in ControlsConfig.GetAllControllerIndexes()) {
                if (PlayerControls.PausePressed(controllerIndex)) {
                    return true;
                }
            }
            return false;
        }

        public static bool MenuLeftPressed() {
            foreach (int controllerIndex in ControlsConfig.GetAllControllerIndexes()) {
                if (PlayerControls.MenuLeftPressed(controllerIndex)) {
                    return true;
                }
            }
            return false;
        }

        public static bool MenuRightPressed() {
            foreach (int controllerIndex in ControlsConfig.GetAllControllerIndexes()) {
                if (PlayerControls.MenuRightPressed(controllerIndex)) {
                    return true;
                }
            }
            return false;
        }

        public static bool MenuUpPressed() {
            foreach (int controllerIndex in ControlsConfig.GetAllControllerIndexes()) {
                if (PlayerControls.MenuUpPressed(controllerIndex)) {
                    return true;
                }
            }
            return false;
        }

        public static bool MenuDownPressed() {
            foreach (int controllerIndex in ControlsConfig.GetAllControllerIndexes()) {
                if (PlayerControls.MenuDownPressed(controllerIndex)) {
                    return true;
                }
            }
            return false;
        }

        public static bool CurrentlySettingControl() {
            if (Globals.gameInstance.gameState == GameState.inStartMenues) {
                return Globals.startMenuesManager.mainMenu.mainMenuSettingsMenu.controlsMenu.CurrentlySettingControl();
            } else if (Globals.gameInstance.gameState == GameState.inGame) {
                return Globals.gameplayManager.gameFlow.pauseMenu.controlsMenu.CurrentlySettingControl();
            }
            return false;
        }
    }
}
