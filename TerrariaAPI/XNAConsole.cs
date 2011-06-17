using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TerrariaAPI
{
    public class XNAConsole : DrawableGameComponent
    {
        private enum ConsoleState { Closed, Closing, Open, Opening }

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

        public void LoadFont(SpriteFont font)
        {
            spriteFont = font;
            consoleWidth = Game.Window.ClientBounds.Right - Game.Window.ClientBounds.Left - 20;
            consoleHeight = font.LineSpacing * MaxLineCount + 20;
            lineWidth = (int)((consoleWidth - 20) / font.MeasureString("a").X) - 2;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = DrawingHelper.CreateOnePixelTexture(GraphicsDevice, new Color(0, 0, 0, 175));
            border = DrawingHelper.CreateOnePixelTexture(GraphicsDevice, Color.White);
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
        }

        public override void Draw(GameTime gameTime)
        {
            double now = gameTime.TotalGameTime.TotalSeconds;

            consoleWidth = Game.Window.ClientBounds.Right - Game.Window.ClientBounds.Left - 20;
            consoleHeight = spriteFont.LineSpacing * MaxLineCount + 20;

            consoleXOffset = 10;
            consoleYOffset = 0;

            if (consoleState == ConsoleState.Opening)
            {
                consoleYOffset = (int)MathHelper.Lerp(-consoleHeight, 0, (float)Math.Sqrt((float)(now - stateStartTime) / (float)AnimationTime));
            }
            else if (consoleState == ConsoleState.Closing)
            {
                consoleYOffset = (int)MathHelper.Lerp(0, -consoleHeight, ((float)(now - stateStartTime) / (float)AnimationTime) * ((float)(now - stateStartTime) / (float)AnimationTime));
            }

            lineWidth = (int)((consoleWidth - 20) / spriteFont.MeasureString("a").X) - 2;

            DrawConsole();
        }

        private void DrawConsole()
        {
            spriteBatch.Draw(background, new Rectangle(consoleXOffset, consoleYOffset, consoleWidth, consoleHeight), Color.White); // Background
            spriteBatch.Draw(border, new Rectangle(consoleXOffset, consoleYOffset, 1, consoleHeight), Color.White); // Border: Left
            spriteBatch.Draw(border, new Rectangle(consoleXOffset, consoleYOffset + consoleHeight - 1, consoleWidth, 1), Color.White); // Border: Bottom
            spriteBatch.Draw(border, new Rectangle(consoleXOffset + consoleWidth - 1, consoleYOffset, 1, consoleHeight), Color.White); // Border: Right

            List<string> lines = ParseOutputBuffer(outputBuffer.ToString());

            for (int i = 0; i < lines.Count && i <= MaxLineCount; i++)
            {
                DrawingHelper.DrawTextWithShadow(spriteBatch, lines[i], new Vector2(consoleXOffset + 10, consoleYOffset + consoleHeight - 10 - spriteFont.LineSpacing * i),
                    spriteFont, Color.White, Color.Black);
            }
        }

        public void Write(string text)
        {
            outputBuffer.Append(text);
        }

        public void WriteLine(string text)
        {
            outputBuffer.AppendLine(text);
        }

        public void Clear()
        {
            outputBuffer.Clear();
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