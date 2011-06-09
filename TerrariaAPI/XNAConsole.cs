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
        private enum ConsoleState
        {
            Closed,
            Closing,
            Open,
            Opening
        }

        private const double AnimationTime = 0.3;
        private const int LinesDisplayed = 20;

        private StringWriter stringWriter;
        private StringBuilder outputBuffer;
        private int lineWidth, consoleXSize, consoleYSize;
        private GraphicsDevice device;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D background;
        private ConsoleState consoleState;
        private double stateStartTime;
        private InputManager input;

        public XNAConsole(Game game, SpriteFont font)
            : base(game)
        {
            Visible = false;
            device = game.GraphicsDevice;
            spriteBatch = new SpriteBatch(device);
            this.font = font;

            background = DrawingHelper.CreateOnePixelTexture(device, new Color(0, 0, 0, 125));

            consoleXSize = Game.Window.ClientBounds.Right - Game.Window.ClientBounds.Left - 20;
            consoleYSize = font.LineSpacing * LinesDisplayed + 20;
            lineWidth = (int)((consoleXSize - 20) / font.MeasureString("a").X) - 2;

            consoleState = ConsoleState.Closed;
            stateStartTime = 0;

            input = new InputManager();
            outputBuffer = new StringBuilder(1024);
            stringWriter = new StringWriter(outputBuffer);
            Console.SetOut(stringWriter);
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
                    if (input.IsKeyDown(Keys.OemTilde))
                    {
                        consoleState = ConsoleState.Closing;
                        stateStartTime = now;
                    }
                    break;
                case ConsoleState.Closed:
                    if (input.IsKeyDown(Keys.OemTilde))
                    {
                        consoleState = ConsoleState.Opening;
                        stateStartTime = now;
                        Visible = true;
                    }
                    break;
            }
        }

        public void Write(string str)
        {
            outputBuffer.Append(str);
        }

        public void WriteLine(string text)
        {
            outputBuffer.AppendLine(text);
        }

        public void Clear()
        {
            outputBuffer.Clear();
        }

        public override void Draw(GameTime gameTime)
        {
            base.DrawOrder = 1000;

            double now = gameTime.TotalGameTime.TotalSeconds;

            consoleXSize = this.Game.Window.ClientBounds.Right - this.Game.Window.ClientBounds.Left - 20;
            consoleYSize = this.font.LineSpacing * LinesDisplayed + 20;

            int consoleXOffset = 10;
            int consoleYOffset = 0;

            if (consoleState == ConsoleState.Opening)
            {
                consoleYOffset = (int)MathHelper.Lerp(-consoleYSize, 0, (float)Math.Sqrt((float)(now - stateStartTime) / (float)AnimationTime));
            }
            else if (consoleState == ConsoleState.Closing)
            {
                consoleYOffset = (int)MathHelper.Lerp(0, -consoleYSize, ((float)(now - stateStartTime) / (float)AnimationTime) * ((float)(now - stateStartTime) / (float)AnimationTime));
            }

            this.lineWidth = (int)((consoleXSize - 20) / font.MeasureString("a").X) - 2;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spriteBatch.Draw(background, new Rectangle(consoleXOffset, consoleYOffset, consoleXSize, consoleYSize), Color.White);

            List<string> lines = ParseOutputBuffer(outputBuffer.ToString());

            for (int i = 0; i < lines.Count && i <= LinesDisplayed; i++)
            {
                spriteBatch.DrawString(font, lines[i], new Vector2(consoleXOffset + 10, consoleYOffset + consoleYSize - 10 - font.LineSpacing * i), Color.White);
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