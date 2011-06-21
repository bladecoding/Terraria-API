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

        public InputManager()
        {
            currentKeyboardState = new KeyboardState();
            previousKeyboardState = new KeyboardState();
            currentMouseState = new MouseState();
            previousMouseState = new MouseState();
        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        // Keyboard

        public bool IsAltDown
        {
            get { return IsKeyDown(Keys.LeftAlt) || IsKeyDown(Keys.RightAlt); }
        }

        public bool IsControlDown
        {
            get { return IsKeyDown(Keys.LeftControl) || IsKeyDown(Keys.RightControl); }
        }

        public bool IsShiftDown
        {
            get { return IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.RightShift); }
        }

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
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

        public Keys[] GetPressedKeys(bool once = false)
        {
            if (once)
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

            return currentKeyboardState.GetPressedKeys();
        }

        // Mouse

        public bool IsMouseDown(MouseButtons button)
        {
            return GetButtonState(currentMouseState, button) == ButtonState.Pressed;
        }

        public bool IsMouseUp(MouseButtons button)
        {
            return GetButtonState(currentMouseState, button) == ButtonState.Released;
        }

        public bool IsMousePressed(MouseButtons button)
        {
            return GetButtonState(currentMouseState, button) == ButtonState.Pressed && GetButtonState(previousMouseState, button) == ButtonState.Released;
        }

        public bool IsMouseReleased(MouseButtons button)
        {
            return GetButtonState(currentMouseState, button) == ButtonState.Released && GetButtonState(previousMouseState, button) == ButtonState.Pressed;
        }

        public Vector2 GetMousePosition()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        public int GetMouseScrollWheelDelta()
        {
            return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
        }

        private ButtonState GetButtonState(MouseState mouseState, MouseButtons button)
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
    }
}