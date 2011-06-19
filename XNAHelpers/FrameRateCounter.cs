using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAHelpers
{
    public class FrameRateCounter : DrawableGameComponent
    {
        public int CurrentFrameRate { get; private set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Vector2 ShadowPosition { get; set; }
        public Color ShadowColor { get; set; }

        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private readonly TimeSpan oneSecond = TimeSpan.FromSeconds(1);

        public FrameRateCounter(Game game, Vector2 position, Color color, Color shadowColor)
            : base(game)
        {
            Position = position;
            ShadowPosition = new Vector2(Position.X + 1, Position.Y + 1);
            Color = color;
            ShadowColor = shadowColor;
        }

        public void LoadFont(SpriteFont font)
        {
            spriteFont = font;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > oneSecond)
            {
                elapsedTime -= oneSecond;
                CurrentFrameRate = frameCounter;
                frameCounter = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            frameCounter++;

            if (spriteFont != null)
            {
                string text = "FPS: " + CurrentFrameRate;

                spriteBatch.Begin();
                spriteBatch.DrawString(spriteFont, text, ShadowPosition, ShadowColor);
                spriteBatch.DrawString(spriteFont, text, Position, Color);
                spriteBatch.End();
            }
        }
    }
}