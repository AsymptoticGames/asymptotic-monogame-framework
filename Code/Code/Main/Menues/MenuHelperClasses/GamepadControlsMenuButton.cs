using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework {

    public enum GamepadControlsMenuButtonType {
        Button,
        Toggle,
        JoystickOption
    };

    public class GamepadControlsMenuButton : MenuButtonWithImage {

        public string controlsTKey;
        public int controllerIndex;

        public GamepadControlsMenuButtonType buttonType;

        public GamepadControlsMenuButton(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText, int _input) : base(_position, _menuScreen, _size, _buttonText, GetTextureFromInput(_input)) {
            Setup(_buttonText, _input);
        }

        public GamepadControlsMenuButton(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText, SpriteFont font, int _input) : base(_position, _menuScreen, _size, _buttonText, font, GetTextureFromInput(_input)) {
            Setup(_buttonText, _input);
        }

        private void Setup(string _buttonText, int _input) {
            controlsTKey = _buttonText;
            SetSideImageSize(_input);
            if (ControlsConfig.IsInputJoystickOptions(_input)) {
                buttonType = GamepadControlsMenuButtonType.JoystickOption;
            } else if (ControlsConfig.IsInputToggleOptions(_input)) {
                buttonType = GamepadControlsMenuButtonType.Toggle;
            } else {
                buttonType = GamepadControlsMenuButtonType.Button;
            }
        }

        private void SetSideImageSize(int _input) {
            sideImageSize = new Vector2(80f, 80f);
            Texture2D texture = GetTextureFromInput(_input);
            sideImageSize = new Vector2(HelperFunctions.Clamp(sideImageSize.Y * texture.Width / texture.Height, 40, 200), sideImageSize.Y);
            image = texture;
        }

        public void SetNewInput(Buttons button) {
            SetSideImageSize((int)button);
            ControlsConfig.gamepadControls[controlsTKey][controllerIndex] = (int)button;
        }

        public void SetNewInput(JoystickOptions joystickOption) {
            SetSideImageSize((int)joystickOption);
            ControlsConfig.gamepadControls[controlsTKey][controllerIndex] = (int)joystickOption;
        }

        public void SetNewInput(bool toggleValue) {
            SetSideImageSize(toggleValue ? (int)ToggleOptions.True : (int)ToggleOptions.False);
            ControlsConfig.gamepadControls[controlsTKey][controllerIndex] = toggleValue ? (int)ToggleOptions.True : (int)ToggleOptions.False;
        }

        public void SetWaitingForInput() {
            sideImageSize = new Vector2(80f, 80f);
            image = GlobalTextures.waitingForControlsInput;
        }

        public void UpdateImage() {
            SetSideImageSize(ControlsConfig.gamepadControls[controlsTKey][controllerIndex]);
        }

        public static Texture2D GetTextureFromInput(int _input) {
            switch (_input) {
                case (int)Buttons.A:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/a");
                case (int)Buttons.B:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/b");
                case (int)Buttons.X:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/x");
                case (int)Buttons.Y:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/y");
                case (int)Buttons.RightShoulder:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/right-bumper");
                case (int)Buttons.LeftShoulder:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/left-bumper");
                case (int)Buttons.RightTrigger:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/right-trigger");
                case (int)Buttons.LeftTrigger:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/left-trigger");
                case (int)Buttons.Start:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/start");
                case (int)Buttons.Back:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/back");
                case (int)Buttons.DPadUp:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/dpad-up");
                case (int)Buttons.DPadDown:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/dpad-down");
                case (int)Buttons.DPadLeft:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/dpad-left");
                case (int)Buttons.DPadRight:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/dpad-right");
                case (int)Buttons.LeftStick:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/left-stick-press");
                case (int)Buttons.RightStick:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/right-stick-press");
                case (int)JoystickOptions.Left:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/left-stick");
                case (int)JoystickOptions.Right:
                    return Globals.content.Load<Texture2D>("Controls/Buttons/right-stick");
                case (int)ToggleOptions.True:
                    return Globals.content.Load<Texture2D>("Controls/Checkbox/true");
                case (int)ToggleOptions.False:
                    return Globals.content.Load<Texture2D>("Controls/Checkbox/false");
                default:
                    break;
            }

            Console.WriteLine("Input Not found: Input " + _input);
            return Globals.content.Load<Texture2D>("Controls/input-not-found");
        }

    }
}
