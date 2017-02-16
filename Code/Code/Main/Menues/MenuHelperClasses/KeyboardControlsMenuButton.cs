using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsymptoticMonoGameFramework {

    public enum KeyboardControlsMenuButtonType {
        Button,
        Toggle
    };

    public class KeyboardControlsMenuButton : MenuButtonWithImage {

        public string controlsTKey;

        public KeyboardControlsMenuButtonType buttonType;

        public KeyboardControlsMenuButton(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText, int _input) : base(_position, _menuScreen, _size, _buttonText, GetTextureFromInput(_input)) {
            Setup(_buttonText, _input);
        }

        public KeyboardControlsMenuButton(Vector2 _position, MenuScreen _menuScreen, Vector2 _size, string _buttonText, SpriteFont font, int _input) : base(_position, _menuScreen, _size, _buttonText, font, GetTextureFromInput(_input)) {
            Setup(_buttonText, _input);
        }

        private void Setup(string _buttonText, int _input) {
            controlsTKey = _buttonText;
            SetSideImageSize(_input);
            if (ControlsConfig.IsInputToggleOptions(_input)) {
                buttonType = KeyboardControlsMenuButtonType.Toggle;
            } else {
                buttonType = KeyboardControlsMenuButtonType.Button;
            }
        }

        private void SetSideImageSize(int _input) {
            sideImageSize = new Vector2(80f, 80f);
            Texture2D texture = GetTextureFromInput(_input);
            sideImageSize = new Vector2(HelperFunctions.Clamp(sideImageSize.Y * texture.Width / texture.Height, 40, 200), sideImageSize.Y);
            image = texture;
        }

        public void SetNewInput(int button) {
            SetSideImageSize(button);
            ControlsConfig.keyboardControls[controlsTKey][0] = button;
        }

        public void SetNewInput(bool toggleValue) {
            SetSideImageSize(toggleValue ? (int)ToggleOptions.True : (int)ToggleOptions.False);
            ControlsConfig.keyboardControls[controlsTKey][0] = toggleValue ? (int)ToggleOptions.True : (int)ToggleOptions.False;
        }

        public void SetWaitingForInput() {
            sideImageSize = new Vector2(80f, 80f);
            image = GlobalTextures.waitingForControlsInput;
        }

        public void UpdateImage() {
            SetSideImageSize(ControlsConfig.keyboardControls[controlsTKey][0]);
        }

        public static Texture2D GetTextureFromInput(int key) {
            switch (key) {
                case (int)Keys.D0:
                    return Globals.content.Load<Texture2D>("Controls/Keys/0");
                case (int)Keys.D1:
                    return Globals.content.Load<Texture2D>("Controls/Keys/1");
                case (int)Keys.D2:
                    return Globals.content.Load<Texture2D>("Controls/Keys/2");
                case (int)Keys.D3:
                    return Globals.content.Load<Texture2D>("Controls/Keys/3");
                case (int)Keys.D4:
                    return Globals.content.Load<Texture2D>("Controls/Keys/4");
                case (int)Keys.D5:
                    return Globals.content.Load<Texture2D>("Controls/Keys/5");
                case (int)Keys.D6:
                    return Globals.content.Load<Texture2D>("Controls/Keys/6");
                case (int)Keys.D7:
                    return Globals.content.Load<Texture2D>("Controls/Keys/7");
                case (int)Keys.D8:
                    return Globals.content.Load<Texture2D>("Controls/Keys/8");
                case (int)Keys.D9:
                    return Globals.content.Load<Texture2D>("Controls/Keys/9");
                case (int)Keys.A:
                    return Globals.content.Load<Texture2D>("Controls/Keys/a");
                case (int)Keys.LeftAlt:
                    return Globals.content.Load<Texture2D>("Controls/Keys/alt");
                case (int)Keys.RightAlt:
                    return Globals.content.Load<Texture2D>("Controls/Keys/alt-right");
                case (int)Keys.OemTilde:
                    return Globals.content.Load<Texture2D>("Controls/Keys/apostroph");
                case (int)Keys.B:
                    return Globals.content.Load<Texture2D>("Controls/Keys/b");
                case (int)Keys.OemPipe:
                    return Globals.content.Load<Texture2D>("Controls/Keys/backslash");
                case (int)Keys.Back:
                    return Globals.content.Load<Texture2D>("Controls/Keys/backspace");
                case (int)Keys.OemCloseBrackets:
                    return Globals.content.Load<Texture2D>("Controls/Keys/bracket-close");
                case (int)Keys.OemOpenBrackets:
                    return Globals.content.Load<Texture2D>("Controls/Keys/bracket-open");
                case (int)Keys.C:
                    return Globals.content.Load<Texture2D>("Controls/Keys/c");
                case (int)Keys.CapsLock:
                    return Globals.content.Load<Texture2D>("Controls/Keys/capslock");
                case (int)Keys.OemQuotes:
                    return Globals.content.Load<Texture2D>("Controls/Keys/comma");
                case (int)Keys.OemComma:
                    return Globals.content.Load<Texture2D>("Controls/Keys/comma-lt");
                case (int)Keys.Apps:
                    return Globals.content.Load<Texture2D>("Controls/Keys/context-menu");
                case (int)Keys.LeftControl:
                    return Globals.content.Load<Texture2D>("Controls/Keys/ctrl");
                case (int)Keys.RightControl:
                    return Globals.content.Load<Texture2D>("Controls/Keys/ctrl-2");
                case (int)Keys.Down:
                    return Globals.content.Load<Texture2D>("Controls/Keys/cursor-down");
                case (int)Keys.Left:
                    return Globals.content.Load<Texture2D>("Controls/Keys/cursor-left");
                case (int)Keys.Right:
                    return Globals.content.Load<Texture2D>("Controls/Keys/cursor-right");
                case (int)Keys.Up:
                    return Globals.content.Load<Texture2D>("Controls/Keys/cursor-up");
                case (int)Keys.D:
                    return Globals.content.Load<Texture2D>("Controls/Keys/d");
                case (int)Keys.Delete:
                    return Globals.content.Load<Texture2D>("Controls/Keys/delete");
                case (int)Keys.E:
                    return Globals.content.Load<Texture2D>("Controls/Keys/e");
                case (int)Keys.End:
                    return Globals.content.Load<Texture2D>("Controls/Keys/end");
                case (int)Keys.Enter:
                    return Globals.content.Load<Texture2D>("Controls/Keys/enter");
                case (int)Keys.OemPlus:
                    return Globals.content.Load<Texture2D>("Controls/Keys/equals-plus");
                case (int)Keys.Escape:
                    return Globals.content.Load<Texture2D>("Controls/Keys/esc");
                case (int)Keys.F:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f");
                case (int)Keys.F1:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f1");
                case (int)Keys.F2:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f2");
                case (int)Keys.F3:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f3");
                case (int)Keys.F4:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f4");
                case (int)Keys.F5:
                    return Globals.content.Load<Texture2D>("Controls/Keys/F5");
                case (int)Keys.F6:
                    return Globals.content.Load<Texture2D>("Controls/Keys/F6");
                case (int)Keys.F7:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f7");
                case (int)Keys.F8:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f8");
                case (int)Keys.F9:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f9");
                case (int)Keys.F10:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f10");
                case (int)Keys.F11:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f11");
                case (int)Keys.F12:
                    return Globals.content.Load<Texture2D>("Controls/Keys/f12");
                case (int)Keys.G:
                    return Globals.content.Load<Texture2D>("Controls/Keys/g");
                case (int)Keys.H:
                    return Globals.content.Load<Texture2D>("Controls/Keys/h");
                case (int)Keys.Home:
                    return Globals.content.Load<Texture2D>("Controls/Keys/home");
                case (int)Keys.I:
                    return Globals.content.Load<Texture2D>("Controls/Keys/i");
                case (int)Keys.Insert:
                    return Globals.content.Load<Texture2D>("Controls/Keys/insert");
                case (int)Keys.J:
                    return Globals.content.Load<Texture2D>("Controls/Keys/j");
                case (int)Keys.K:
                    return Globals.content.Load<Texture2D>("Controls/Keys/k");
                case (int)Keys.NumPad0:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-0");
                case (int)Keys.NumPad1:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-1");
                case (int)Keys.NumPad2:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-2");
                case (int)Keys.NumPad3:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-3");
                case (int)Keys.NumPad4:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-4");
                case (int)Keys.NumPad5:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-5");
                case (int)Keys.NumPad6:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-6");
                case (int)Keys.NumPad7:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-7");
                case (int)Keys.NumPad8:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-8");
                case (int)Keys.NumPad9:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-9");
                case (int)Keys.Add:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-plus");
                case (int)Keys.L:
                    return Globals.content.Load<Texture2D>("Controls/Keys/l");
                case (int)Keys.M:
                    return Globals.content.Load<Texture2D>("Controls/Keys/m");
                case (int)Keys.OemMinus:
                    return Globals.content.Load<Texture2D>("Controls/Keys/minus");
                case (int)Keys.N:
                    return Globals.content.Load<Texture2D>("Controls/Keys/N");
                case (int)Keys.NumLock:
                    return Globals.content.Load<Texture2D>("Controls/Keys/num-lock");
                case (int)Keys.O:
                    return Globals.content.Load<Texture2D>("Controls/Keys/o");
                case (int)Keys.P:
                    return Globals.content.Load<Texture2D>("Controls/Keys/p");
                case (int)Keys.PageDown:
                    return Globals.content.Load<Texture2D>("Controls/Keys/page-down");
                case (int)Keys.PageUp:
                    return Globals.content.Load<Texture2D>("Controls/Keys/page-up");
                case (int)Keys.Pause:
                    return Globals.content.Load<Texture2D>("Controls/Keys/pause");
                case (int)Keys.OemPeriod:
                    return Globals.content.Load<Texture2D>("Controls/Keys/period-gt");
                case (int)Keys.PrintScreen:
                    return Globals.content.Load<Texture2D>("Controls/Keys/print");
                case (int)Keys.Q:
                    return Globals.content.Load<Texture2D>("Controls/Keys/q");
                case (int)Keys.R:
                    return Globals.content.Load<Texture2D>("Controls/Keys/r");
                case (int)Keys.S:
                    return Globals.content.Load<Texture2D>("Controls/Keys/s");
                case (int)Keys.Scroll:
                    return Globals.content.Load<Texture2D>("Controls/Keys/scroll-lock");
                case (int)Keys.OemSemicolon:
                    return Globals.content.Load<Texture2D>("Controls/Keys/semicolon-dble");
                case (int)Keys.LeftShift:
                    return Globals.content.Load<Texture2D>("Controls/Keys/shift");
                case (int)Keys.RightShift:
                    return Globals.content.Load<Texture2D>("Controls/Keys/shift-right");
                case (int)Keys.OemQuestion:
                    return Globals.content.Load<Texture2D>("Controls/Keys/slash-questionmark");
                case (int)Keys.Sleep:
                    return Globals.content.Load<Texture2D>("Controls/Keys/sleep");
                case (int)Keys.Space:
                    return Globals.content.Load<Texture2D>("Controls/Keys/spacebar");
                case (int)Keys.T:
                    return Globals.content.Load<Texture2D>("Controls/Keys/t");
                case (int)Keys.Tab:
                    return Globals.content.Load<Texture2D>("Controls/Keys/tab");
                case (int)Keys.U:
                    return Globals.content.Load<Texture2D>("Controls/Keys/u");
                case (int)Keys.V:
                    return Globals.content.Load<Texture2D>("Controls/Keys/v");
                case (int)Keys.W:
                    return Globals.content.Load<Texture2D>("Controls/Keys/w");
                case (int)Keys.X:
                    return Globals.content.Load<Texture2D>("Controls/Keys/X");
                case (int)Keys.Y:
                    return Globals.content.Load<Texture2D>("Controls/Keys/Y");
                case (int)Keys.Z:
                    return Globals.content.Load<Texture2D>("Controls/Keys/z");
                case (int)Keys.Multiply:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-asterix");
                case (int)Keys.Subtract:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-minus");
                case (int)Keys.Decimal:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-period");
                case (int)Keys.Divide:
                    return Globals.content.Load<Texture2D>("Controls/Keys/keypad-slash");
                case (int)MouseClickOptions.LeftClick:
                    return Globals.content.Load<Texture2D>("Controls/Mouse/mouse-left-click");
                case (int)MouseClickOptions.RightClick:
                    return Globals.content.Load<Texture2D>("Controls/Mouse/mouse-right-click");
                case (int)MouseClickOptions.MiddleClick:
                    return Globals.content.Load<Texture2D>("Controls/Mouse/mouse-middle-click");
                case (int)ToggleOptions.True:
                    return Globals.content.Load<Texture2D>("Controls/Checkbox/true");
                case (int)ToggleOptions.False:
                    return Globals.content.Load<Texture2D>("Controls/Checkbox/false");
                default:
                    break;
            }

            Console.WriteLine("Input Not found: Input " + key);
            return Globals.content.Load<Texture2D>("Controls/input-not-found");
        }

    }
}
