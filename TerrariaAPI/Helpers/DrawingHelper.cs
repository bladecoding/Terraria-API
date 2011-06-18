using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = System.Drawing.Rectangle;
using XNARectangle = Microsoft.Xna.Framework.Rectangle;

namespace TerrariaAPI
{
    public static class DrawingHelper
    {
        public static Texture2D BitmapToTexture(GraphicsDevice gd, Image img)
        {
            int bufferSize = img.Height * img.Width * 4;

            using (MemoryStream ms = new MemoryStream(bufferSize))
            {
                img.Save(ms, ImageFormat.Png);
                return Texture2D.FromStream(gd, ms);
            }
        }

        public static Texture2D IntsToTexture(GraphicsDevice gd, int[] img, int width, int height)
        {
            Texture2D texture = new Texture2D(gd, width, height);
            texture.SetData(img);
            return texture;
        }

        public static Image TextureToImage(Texture2D texture)
        {
            return TextureToImage(texture, texture.Width, texture.Height);
        }

        public static Image TextureToImage(Texture2D texture, int width, int height)
        {
            MemoryStream ms = new MemoryStream();
            texture.SaveAsPng(ms, width, height);
            return Image.FromStream(ms);
        }

        public static Texture2D CreateOnePixelTexture(GraphicsDevice gd, Color color)
        {
            Texture2D texture = new Texture2D(gd, 1, 1);
            texture.SetData<Color>(new Color[1] { color });
            return texture;
        }

        private static readonly Vector2[] shadowOffset = { new Vector2(-1, -1), new Vector2(1, -1), new Vector2(1, 1), new Vector2(-1, 1) };

        public static void DrawTextWithShadow(SpriteBatch sb, string text, Vector2 position, SpriteFont font, Color textColor, Color shadowColor)
        {
            sb.DrawString(font, text, position + shadowOffset[0], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[1], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[2], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[3], shadowColor);
            sb.DrawString(font, text, position, textColor);
        }

        public static void DrawRectangle(SpriteBatch sb, Texture2D background, Texture2D border, XNARectangle rectangle)
        {
            sb.Draw(background, rectangle, Color.White);
            DrawBorder(sb, border, rectangle);
        }

        public static void DrawBorder(SpriteBatch sb, Texture2D border, XNARectangle rectangle)
        {
            sb.Draw(border, new XNARectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), Color.White); // Top
            sb.Draw(border, new XNARectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), Color.White); // Left
            sb.Draw(border, new XNARectangle(rectangle.X, rectangle.Y + rectangle.Height - 1, rectangle.Width, 1), Color.White); // Bottom
            sb.Draw(border, new XNARectangle(rectangle.X + rectangle.Width - 1, rectangle.Y, 1, rectangle.Height), Color.White); // Right
        }

        public static Bitmap ResizeImage(Image img, int width, int height)
        {
            double ratio;
            int newWidth, newHeight;

            if (img.Width <= width && img.Height <= height)
            {
                ratio = 1.0;
                newWidth = img.Width;
                newHeight = img.Height;
            }
            else
            {
                double ratioX = (double)width / (double)img.Width;
                double ratioY = (double)height / (double)img.Height;
                ratio = ratioX < ratioY ? ratioX : ratioY;
                newWidth = (int)(img.Width * ratio);
                newHeight = (int)(img.Height * ratio);
            }

            int posX = (int)((width - (img.Width * ratio)) / 2);
            int posY = (int)((height - (img.Height * ratio)) / 2);

            Bitmap bmp = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(img, posX, posY, newWidth, newHeight);
            }

            return bmp;
        }

        public static Image ColorizeImage(Image img, Color color)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            using (ImageAttributes imgattr = new ImageAttributes())
            {
                float red = (float)color.R / 255;
                float green = (float)color.G / 255;
                float blue = (float)color.B / 255;

                ColorMatrix matrix = new ColorMatrix(new[]{
                    new float[] {red, 0, 0, 0, 0},
                    new float[] {0, green, 0, 0, 0},
                    new float[] {0, 0, blue, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}});

                imgattr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgattr);
            }

            return bmp;
        }
    }
}