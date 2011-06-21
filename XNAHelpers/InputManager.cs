using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XNAHelpers
{
    public enum MouseButtons { Left, Right, Middle, X1, X2 }

    public class InputManager
    {
        private KeyboardState currentKeyboardState, previousKeyboardState;
        private MouseState currentMouseState, previousMouseState;
        private double keyboardDelay, mouseDelay;

        public InputManager()
        {
            currentKeyboardState = new KeyboardState();
            previousKeyboardState = new KeyboardState();
            currentMouseState = new MouseState();
            previousMouseState = new MouseState();
        }

        public void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (keyboardDelay > 0)
            {
                keyboardDelay -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (mouseDelay > 0)
            {
                mouseDelay -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        #region Keyboard

        public bool IsAltKeyDown
        {
            get { return IsKeyDown(Keys.LeftAlt) || IsKeyDown(Keys.RightAlt); }
        }

        public bool IsControlKeyDown
        {
            get { return IsKeyDown(Keys.LeftControl) || IsKeyDown(Keys.RightControl); }
        }

        public bool IsShiftKeyDown
        {
            get { return IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.RightShift); }
        }

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyDown(Keys key, int delay)
        {
            if (keyboardDelay <= 0 && currentKeyboardState.IsKeyDown(key))
            {
                keyboardDelay = delay;
                return true;
            }

            return false;
        }

        public bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }

        public Keys[] GetKeysDown()
        {
            return currentKeyboardState.GetPressedKeys();
        }

        public Keys[] GetKeysPressed()
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

        public Vector2 MousePosition
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        public Vector2 MouseVelocity
        {
            get { return new Vector2(currentMouseState.X - previousMouseState.X, currentMouseState.Y - previousMouseState.Y); }
        }

        public float MouseScrollWheelPosition
        {
            get { return currentMouseState.ScrollWheelValue; }
        }

        public float MouseScrollWheelVelocity
        {
            get { return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue; }
        }

        public bool IsMouseButtonDown(MouseButtons button)
        {
            return GetMouseButtonState(currentMouseState, button) == ButtonState.Pressed;
        }

        public bool IsMouseButtonDown(MouseButtons button, int delay)
        {
            if (mouseDelay <= 0 && GetMouseButtonState(currentMouseState, button) == ButtonState.Pressed)
            {
                mouseDelay = delay;
                return true;
            }

            return false;
        }

        public bool IsMouseButtonUp(MouseButtons button)
        {
            return GetMouseButtonState(currentMouseState, button) == ButtonState.Released;
        }

        public bool IsMouseButtonPressed(MouseButtons button)
        {
            return GetMouseButtonState(currentMouseState, button) == ButtonState.Pressed && GetMouseButtonState(previousMouseState, button) == ButtonState.Released;
        }

        public bool IsMouseButtonReleased(MouseButtons button)
        {
            return GetMouseButtonState(currentMouseState, button) == ButtonState.Released && GetMouseButtonState(previousMouseState, button) == ButtonState.Pressed;
        }

        private ButtonState GetMouseButtonState(MouseState mouseState, MouseButtons button)
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