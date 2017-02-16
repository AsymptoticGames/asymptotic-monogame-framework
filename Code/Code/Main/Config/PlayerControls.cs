using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework
{
    public static class PlayerControls
    {
        static KeyboardState oldKeyboardState;
        static MouseState oldMouseState;
        static GamePadState[] oldGamePadState = new GamePadState[ControlsConfig.numGamepads];

        public static void Update() {
            oldKeyboardState = Keyboard.GetState();
            oldMouseState = Mouse.GetState();
            for(int i = 0; i < ControlsConfig.numGamepads; i++) {
                oldGamePadState[i] = GamePad.GetState(i);
            }
        }

        public static bool PausePressed(int controllerIndex) {
            if (controllerIndex == ControlsConfig.keyboardControllerIndex) {
                return KeyboardKeyPressed((Keys)ControlsConfig.keyboardControls[DefaultControls.pauseTKey][0]);
            } else {
                return GamepadButtonPressed(controllerIndex, (Buttons)ControlsConfig.gamepadControls[DefaultControls.pauseTKey][controllerIndex]);
            }
        }

        public static bool ConfirmPressed(int controllerIndex) {
            if (controllerIndex == ControlsConfig.keyboardControllerIndex) {
                return KeyboardKeyPressed((Keys)ControlsConfig.keyboardControls[DefaultControls.confirmTKey][0]);
            } else {
                return GamepadButtonPressed(controllerIndex, (Buttons)ControlsConfig.gamepadControls[DefaultControls.confirmTKey][controllerIndex]);
            }
        }

        public static bool DeclinePressed(int controllerIndex) {
            if (controllerIndex == ControlsConfig.keyboardControllerIndex) {
                return KeyboardKeyPressed((Keys)ControlsConfig.keyboardControls[DefaultControls.declineTKey][0]);
            } else {
                return GamepadButtonPressed(controllerIndex, (Buttons)ControlsConfig.gamepadControls[DefaultControls.declineTKey][controllerIndex]);
            }
        }

        public static bool MenuLeftPressed(int controllerIndex) {
            if (controllerIndex == ControlsConfig.keyboardControllerIndex) {
                return KeyboardKeyPressed((Keys)ControlsConfig.keyboardControls[DefaultControls.moveLeftTKey][0]) || KeyboardKeyPressed(Keys.Left);
            } else {
                if (GamePad.GetState(controllerIndex).ThumbSticks.Left.X < -0.5f && oldGamePadState[controllerIndex].ThumbSticks.Left.X >= -0.5f) {
                    return true;
                }
                if (GamePad.GetState(controllerIndex).ThumbSticks.Right.X < -0.5f && oldGamePadState[controllerIndex].ThumbSticks.Right.X >= -0.5f) {
                    return true;
                }
                if (GamepadButtonPressed(controllerIndex, (Buttons)ControlsConfig.gamepadControls[DefaultControls.moveLeftTKey][controllerIndex])) {
                    return true;
                }
                return false;
            }
        }

        public static bool MenuRightPressed(int controllerIndex) {
            if (controllerIndex == ControlsConfig.keyboardControllerIndex) {
                return KeyboardKeyPressed((Keys)ControlsConfig.keyboardControls[DefaultControls.moveRightTKey][0]) || KeyboardKeyPressed(Keys.Right);
            } else {
                if (GamePad.GetState(controllerIndex).ThumbSticks.Left.X > 0.5f && oldGamePadState[controllerIndex].ThumbSticks.Left.X <= 0.5f) {
                    return true;
                }
                if (GamePad.GetState(controllerIndex).ThumbSticks.Right.X > 0.5f && oldGamePadState[controllerIndex].ThumbSticks.Right.X <= 0.5f) {
                    return true;
                }
                if (GamepadButtonPressed(controllerIndex, (Buttons)ControlsConfig.gamepadControls[DefaultControls.moveRightTKey][controllerIndex])) {
                    return true;
                }
                return false;
            }
        }

        public static bool MenuUpPressed(int controllerIndex) {
            if (controllerIndex == ControlsConfig.keyboardControllerIndex) {
                return KeyboardKeyPressed((Keys)ControlsConfig.keyboardControls[DefaultControls.moveUpTKey][0]) || KeyboardKeyPressed(Keys.Up);
            } else {
                if (GamePad.GetState(controllerIndex).ThumbSticks.Left.Y > 0.5f && oldGamePadState[controllerIndex].ThumbSticks.Left.Y <= 0.5f) {
                    return true;
                }
                if (GamePad.GetState(controllerIndex).ThumbSticks.Right.Y > 0.5f && oldGamePadState[controllerIndex].ThumbSticks.Right.Y <= 0.5f) {
                    return true;
                }
                if (GamepadButtonPressed(controllerIndex, (Buttons)ControlsConfig.gamepadControls[DefaultControls.moveUpTKey][controllerIndex])) {
                    return true;
                }
                return false;
            }
        }

        public static bool MenuDownPressed(int controllerIndex) {
            if (controllerIndex == ControlsConfig.keyboardControllerIndex) {
                return KeyboardKeyPressed((Keys)ControlsConfig.keyboardControls[DefaultControls.moveDownTKey][0]) || KeyboardKeyPressed(Keys.Down);
            } else {
                if (GamePad.GetState(controllerIndex).ThumbSticks.Left.Y < -0.5f && oldGamePadState[controllerIndex].ThumbSticks.Left.Y >= -0.5f) {
                    return true;
                }
                if (GamePad.GetState(controllerIndex).ThumbSticks.Right.Y < -0.5f && oldGamePadState[controllerIndex].ThumbSticks.Right.Y >= -0.5f) {
                    return true;
                }
                if (GamepadButtonPressed(controllerIndex, (Buttons)ControlsConfig.gamepadControls[DefaultControls.moveDownTKey][controllerIndex])) {
                    return true;
                }
                return false;
            }
        }
        
        public static Vector2 MousePosition() {
            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            return Vector2.Transform(mousePosition - Resolution.InputTranslate, Resolution.InputScale);
        }

        public static bool MouseLeftDown() {
            return (Mouse.GetState().LeftButton == ButtonState.Pressed);
        }

        public static bool MouseLeftPressed() {
            return (oldMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed);
        }

        public static bool MouseRightPressed() {
            return (oldMouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed);
        }

        public static bool MousePositionMoved() {
            return (new Vector2(Mouse.GetState().X, Mouse.GetState().Y) != new Vector2(oldMouseState.X, oldMouseState.Y));
        }

        /** Helper Functions **/
        static bool GamepadButtonPressed(int controllerIndex, Buttons button) {
            return GamePad.GetState(controllerIndex).IsButtonDown(button) && !oldGamePadState[controllerIndex].IsButtonDown(button);
        }

        static bool GamepadButtonDown(int controllerIndex, Buttons button) {
            return GamePad.GetState(controllerIndex).IsButtonDown(button);
        }

        static bool KeyboardKeyDown(Keys key) {
            if (key == (Keys)MouseClickOptions.LeftClick) {
                return Mouse.GetState().LeftButton == ButtonState.Pressed;
            } else if (key == (Keys)MouseClickOptions.RightClick) {
                return Mouse.GetState().RightButton == ButtonState.Pressed;
            } else if (key == (Keys)MouseClickOptions.MiddleClick) {
                return Mouse.GetState().MiddleButton == ButtonState.Pressed;
            } else {
                return Keyboard.GetState().IsKeyDown(key);
            }
        }

        static bool KeyboardKeyPressed(Keys key) {
            if (key == (Keys)MouseClickOptions.LeftClick) {
                return Mouse.GetState().LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed;
            } else if (key == (Keys)MouseClickOptions.RightClick) {
                return Mouse.GetState().RightButton == ButtonState.Pressed && oldMouseState.RightButton != ButtonState.Pressed;
            } else if (key == (Keys)MouseClickOptions.MiddleClick) {
                return Mouse.GetState().MiddleButton == ButtonState.Pressed && oldMouseState.MiddleButton != ButtonState.Pressed;
            } else {
                return Keyboard.GetState().IsKeyDown(key) && !oldKeyboardState.IsKeyDown(key);
            }
        }

    }
}
