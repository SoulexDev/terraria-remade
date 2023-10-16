using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    public static class Input
    {
        private static KeyboardState keyboardState, lastKeyboardState;
        public static MouseState mouseState, lastMouseState;
        private static GamePadState gamepadState, lastGamepadState;

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y)
                    + Camera.Instance.transform.position 
                    - GameRoot.ScreenSize / 2; } }
        public static void Update()
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
            lastGamepadState = gamepadState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            gamepadState = GamePad.GetState(PlayerIndex.One);
        }
        public static bool GetMouseDown(string inputName)
        {
            switch (inputName)
            {
                case "LeftButton":
                    return lastMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed;
                case "RightButton":
                    return lastMouseState.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed;
                case "MiddleButton":
                    return lastMouseState.MiddleButton == ButtonState.Released && mouseState.MiddleButton == ButtonState.Pressed;
                default:
                    break;
            }
            return false;
        }
        public static bool GetMouse(string inputName)
        {
            switch (inputName)
            {
                case "LeftButton":
                    return mouseState.LeftButton == ButtonState.Pressed;
                case "RightButton":
                    return mouseState.RightButton == ButtonState.Pressed;
                case "MiddleButton":
                    return mouseState.MiddleButton == ButtonState.Pressed;
                default:
                    break;
            }
            return false;
        }
        public static bool GetKeyUp(Keys key)
        {
            return lastKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key);
        }
        public static bool GetKeyDown(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }
        public static bool GetKey(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool GetButtonUp(Buttons button)
        {
            return lastGamepadState.IsButtonDown(button) && gamepadState.IsButtonUp(button);
        }
        public static bool GetButtonDown(Buttons button)
        {
            return lastGamepadState.IsButtonUp(button) && gamepadState.IsButtonDown(button);
        }
        public static bool GetButton(Buttons button)
        {
            return gamepadState.IsButtonDown(button);
        }
    }
}