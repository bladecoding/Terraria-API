using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TerrariaAPI
{
    public class InputManager
    {
        public delegate void MouseClickDelegate(Vector2 clickPosition, bool wasSingleClick);

        public event MouseClickDelegate OnMouse1Press;
        public event MouseClickDelegate OnMouse1Release;

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

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

            if (OnMouse1Press != null)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    OnMouse1Press(GetMousePos(), true);
                else if (currentMouseState.LeftButton == ButtonState.Pressed)
                    OnMouse1Press(GetMousePos(), false);
            }

            if (OnMouse1Release != null)
            {
                if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
                    OnMouse1Release(GetMousePos(), true);
            }
        }

        public bool IsKeyDown(Keys key, bool once = false)
        {
            if (once)
            {
                if (currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
                    return true;
                return false;
            }

            if (currentKeyboardState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool IsKeyUp(Keys key, bool once = false)
        {
            if (once)
            {
                if (currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key))
                    return true;
                return false;
            }

            if (currentKeyboardState.IsKeyUp(key))
                return true;
            return false;
        }

        public Vector2 GetMousePos()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }
    }
}