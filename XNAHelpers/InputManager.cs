using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XNAHelpers
{
    public enum MouseButtons { Left, Right, Middle, X1, X2 }

    public static class InputManager
    {
        private static KeyboardState currentKeyboardState, previousKeyboardState;
        private static MouseState currentMouseState, previousMouseState;
        private static int keyboardDelay, mouseDelay;

        static InputManager()
        {
            currentKeyboardState = new KeyboardState();
            previousKeyboardState = new KeyboardState();
            currentMouseState = new MouseState();
            previousMouseState = new MouseState();
        }

        public static void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            int elapsed = (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (keyboardDelay > 0)
            {
                keyboardDelay -= elapsed;
            }

            if (mouseDelay > 0)
            {
                mouseDelay -= elapsed;
            }
        }

        #region Keyboard

        public static bool IsAltKeyDown
        {
            get { return IsKeyDown(Keys.LeftAlt) || IsKeyDown(Keys.RightAlt); }
        }

        public static bool IsControlKeyDown
        {
            get { return IsKeyDown(Keys.LeftControl) || IsKeyDown(Keys.RightControl); }
        }

        public static bool IsShiftKeyDown
        {
            get { return IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.RightShift); }
        }

        public static bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key, int delay)
        {
            if (keyboardDelay <= 0 && currentKeyboardState.IsKeyDown(key))
            {
                keyboardDelay = delay;
                return true;
            }

            return false;
        }

        public static bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }

        public static Keys[] GetKeysDown()
        {
            return currentKeyboardState.GetPressedKeys();
        }

        public static Keys[] GetKeysPressed()
        {
            List<Keys> keys = new List<Keys>();

            foreach (Keys key in currentKeyboardState.GetPressedKeys())
            {
                if (previousKeyboardState.IsKeyUp(key))
                {
                    keys.Add(key);
                }
            }

            return keys.ToArray();
        }

        #endregion Keyboard

        #region Mouse

        public static Vector2 MousePosition
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        public static Vector2 MouseVelocity
        {
            get { return new Vector2(currentMouseState.X - previousMouseState.X, currentMouseState.Y - previousMouseState.Y); }
        }

        public static float MouseScrollWheelPosition
        {
            get { return currentMouseState.ScrollWheelValue; }
        }

        public static float MouseScrollWheelVelocity
        {
            get { return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue; }
        }

        public static bool IsMouseButtonDown(MouseButtons button)
        {
            return GetMouseButtonState(currentMouseState, button) == ButtonState.Pressed;
        }

        public static bool IsMouseButtonDown(MouseButtons button, int delay)
        {
            if (mouseDelay <= 0 && GetMouseButtonState(currentMouseState, button) == ButtonState.Pressed)
            {
                mouseDelay = delay;
                return true;
            }

            return false;
        }

        public static bool IsMouseButtonUp(MouseButtons button)
        {
            return GetMouseButtonState(currentMouseState, button) == ButtonState.Released;
        }

        public static bool IsMouseButtonPressed(MouseButtons button)
        {
            return GetMouseButtonState(currentMouseState, button) == ButtonState.Pressed && GetMouseButtonState(previousMouseState, button) == ButtonState.Released;
        }

        public static bool IsMouseButtonReleased(MouseButtons button)
        {
            return GetMouseButtonState(currentMouseState, button) == ButtonState.Released && GetMouseButtonState(previousMouseState, button) == ButtonState.Pressed;
        }

        private static ButtonState GetMouseButtonState(MouseState mouseState, MouseButtons button)
        {
            switch (button)
            {
                default:
                case MouseButtons.Left:
                    return mouseState.LeftButton;
                case MouseButtons.Right:
                    return mouseState.RightButton;
                case MouseButtons.Middle:
                    return mouseState.MiddleButton;
                case MouseButtons.X1:
                    return mouseState.XButton1;
                case MouseButtons.X2:
                    return mouseState.XButton2;
            }
        }

        #endregion Mouse
    }
}