using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAHelpers
{
    public class XNAConsole : DrawableGameComponent
    {
        private enum ConsoleState { Closed, Closing, Open, Opening }

        public event Action<string, HandledEventArgs> MessageSend;

        public double AnimationTime { get; set; }
        public int MaxLineCount { get; set; }

        private StringWriter stringWriter;
        private StringBuilder outputBuffer;
        private int lineWidth, consoleXOffset, consoleYOffset, consoleWidth, consoleHeight;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Texture2D background, border;
        private ConsoleState consoleState;
        private double stateStartTime;
        private InputManager input;
        private string inputMessage;

        public XNAConsole(Game game)
            : base(game)
        {
            Visible = false;

            AnimationTime = 0.3f;
            MaxLineCount = 20;

            consoleState = ConsoleState.Closed;
            stateStartTime = 0;

            input = new InputManager();

            outputBuffer = new StringBuilder(1024);
            stringWriter = new StringWriter(outputBuffer);
            Console.SetOut(stringWriter);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = DrawingHelper.CreateOnePixelTexture(GraphicsDevice, new Color(0, 0, 0, 175));
            border = DrawingHelper.CreateOnePixelTexture(GraphicsDevice, Color.White);
        }

        public void LoadFont(SpriteFont font)
        {
            spriteFont = font;
            consoleWidth = Game.Window.ClientBounds.Right - Game.Window.ClientBounds.Left - 20;
            consoleHeight = font.LineSpacing * MaxLineCount + 20;
            lineWidth = (int)((consoleWidth - 20) / font.MeasureString("a").X) - 2;
        }

        public void Write(string text)
        {
            outputBuffer.Append(text);
        }

        public void Write(string format, params object[] args)
        {
            outputBuffer.AppendFormat(format, args);
        }

        public void WriteLine(string text)
        {
            outputBuffer.AppendLine(text);
        }

        public void WriteLine(string format, params object[] args)
        {
            outputBuffer.AppendFormat(format + Environment.NewLine, args);
        }

        public void Clear()
        {
            outputBuffer.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            input.Update();

            double now = gameTime.TotalGameTime.TotalSeconds;
            double elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds;

            switch (consoleState)
            {
                case ConsoleState.Opening:
                    if (now - stateStartTime > AnimationTime)
                    {
                        consoleState = ConsoleState.Open;
                        stateStartTime = now;
                    }
                    break;
                case ConsoleState.Closing:
                    if (now - stateStartTime > AnimationTime)
                    {
                        consoleState = ConsoleState.Closed;
                        stateStartTime = now;
                        Visible = false;
                    }
                    break;
                case ConsoleState.Open:
                    if (input.IsKeyDown(Keys.OemTilde, true))
                    {
                        consoleState = ConsoleState.Closing;
                        stateStartTime = now;
                    }
                    break;
                case ConsoleState.Closed:
                    if (input.IsKeyDown(Keys.OemTilde, true))
                    {
                        consoleState = ConsoleState.Opening;
                        stateStartTime = now;
                        Visible = true;
                    }
                    break;
            }

            if (Visible)
            {
                CheckInput();
            }
        }

        private void CheckInput()
        {
            foreach (Keys key in input.GetPressedKeys(true))
            {
                int num = (int)key;

                if (num == 8 && inputMessage.Length > 0) // Backspace
                {
                    inputMessage = inputMessage.Remove(inputMessage.Length - 1);
                }
                else if (num == 32) // Space
                {
                    inputMessage += ' ';
                }
                else if (num >= 65 && num <= 90) // A - Z
                {
                    if (!input.IsShiftDown)
                    {
                        num += 32;
                    }

                    inputMessage += (char)num;
                }
                else if ((num >= 48 && num <= 57) || (num >= 96 && num <= 105)) // 0 - 9
                {
                    if (num >= 96)
                    {
                        num -= 48;
                    }

                    inputMessage += (char)num;
                }
                else if (num == 13) // Enter
                {
                    bool handled = OnMessageSend(inputMessage);

                    if (!handled && !string.IsNullOrWhiteSpace(inputMessage))
                    {
                        WriteLine(inputMessage);
                    }

                    inputMessage = string.Empty;
                }
            }
        }

        protected bool OnMessageSend(string text)
        {
            if (MessageSend != null)
            {
                HandledEventArgs args = new HandledEventArgs();
                MessageSend(text, args);
                return args.Handled;
            }

            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            double now = gameTime.TotalGameTime.TotalSeconds;

            consoleWidth = Game.Window.ClientBounds.Right - Game.Window.ClientBounds.Left - 20;
            consoleHeight = spriteFont.LineSpacing * MaxLineCount + 20;

            consoleXOffset = 10;
            consoleYOffset = 10;

            if (consoleState == ConsoleState.Opening)
            {
                consoleYOffset = (int)MathHelpers.Lerp(-consoleHeight, 0, (float)Math.Sqrt((float)(now - stateStartTime) / (float)AnimationTime));
            }
            else if (consoleState == ConsoleState.Closing)
            {
                consoleYOffset = (int)MathHelpers.Lerp(0, -consoleHeight, ((float)(now - stateStartTime) / (float)AnimationTime) * ((float)(now - stateStartTime) / (float)AnimationTime));
            }

            lineWidth = (int)((consoleWidth - 20) / spriteFont.MeasureString("a").X) - 2;

            DrawConsole();
        }

        private void DrawConsole()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            DrawingHelper.DrawRectangle(spriteBatch, background, border,
                new Rectangle(consoleXOffset, consoleYOffset, consoleWidth, consoleHeight));
            DrawingHelper.DrawRectangle(spriteBatch, background, border,
                new Rectangle(consoleXOffset, consoleYOffset + consoleHeight - 1, consoleWidth, spriteFont.LineSpacing + 20));

            List<string> lines = ParseOutputBuffer(outputBuffer.ToString());

            for (int i = 0; i < lines.Count && i <= MaxLineCount; i++)
            {
                DrawingHelper.DrawTextWithShadow(spriteBatch, lines[i], new Vector2(consoleXOffset + 10, consoleYOffset + consoleHeight - 10 - spriteFont.LineSpacing * i - 1),
                    spriteFont, Color.White, Color.Black);
            }

            if (!string.IsNullOrEmpty(inputMessage))
            {
                DrawingHelper.DrawTextWithShadow(spriteBatch, inputMessage, new Vector2(consoleXOffset + 10, consoleYOffset + consoleHeight + spriteFont.LineSpacing - 10 - 1),
                    spriteFont, Color.White, Color.Black);
            }

            spriteBatch.End();
        }

        private List<string> ParseOutputBuffer(string line)
        {
            List<string> wraplines = new List<string>();

            if (!string.IsNullOrEmpty(line))
            {
                wraplines.Add("");

                int lineNum = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];

                    if (c == '\n' || wraplines[lineNum].Length > lineWidth)
                    {
                        wraplines.Add("");
                        lineNum++;
                    }
                    else
                    {
                        wraplines[lineNum] += c;
                    }
                }
            }

            wraplines.Reverse();

            return wraplines;
        }
    }
}