using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TerrariaAPI
{
    public enum MouseButtons { Left, Right, Middle, X1, X2 }

    public class InputManager
    {
        private KeyboardState currentKeyboardState, previousKeyboardState;
        private MouseState currentMouseState, previousMouseState;

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
            previousMouseState = currentMouseState;

            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
        }

        public bool IsKeyDown(Keys key, bool once = false)
        {
            return currentKeyboardState.IsKeyDown(key) && (!once || previousKeyboardState.IsKeyUp(key));
        }

        public bool IsKeyUp(Keys key, bool once = false)
        {
            return currentKeyboardState.IsKeyUp(key) && (!once || previousKeyboardState.IsKeyDown(key));
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

        public bool IsMouseDown(MouseButtons buttons, bool once = false)
        {
            ButtonState currentButtonState = GetButtonState(currentMouseState, buttons);
            ButtonState previousButtonState = GetButtonState(previousMouseState, buttons);

            return currentButtonState == ButtonState.Pressed && (!once || previousButtonState == ButtonState.Released);
        }

        public bool IsMouseUp(MouseButtons buttons, bool once = false)
        {
            ButtonState currentButtonState = GetButtonState(currentMouseState, buttons);
            ButtonState previousButtonState = GetButtonState(previousMouseState, buttons);

            return currentButtonState == ButtonState.Released && (!once || previousButtonState == ButtonState.Pressed);
        }

        public Vector2 GetMousePosition()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        public int GetMouseScrollWheelDelta()
        {
            return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
        }

        private ButtonState GetButtonState(MouseState mouseState, MouseButtons buttons)
        {
            switch (buttons)
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