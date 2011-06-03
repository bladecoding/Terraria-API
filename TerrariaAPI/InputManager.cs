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

            if (OnMouse1Press != null && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                OnMouse1Press(GetMousePos(), previousMouseState.LeftButton == ButtonState.Released);
            }

            if (OnMouse1Release != null && currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                OnMouse1Release(GetMousePos(), true);
            }
        }

        public bool IsKeyDown(Keys key, bool once = false)
        {
            if (once)
            {
                return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
            }

            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key, bool once = false)
        {
            if (once)
            {
                return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
            }

            return currentKeyboardState.IsKeyUp(key);
        }

        public Vector2 GetMousePos()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }
    }
}