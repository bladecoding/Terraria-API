using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = System.Drawing.Rectangle;

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

        private static Vector2[] shadowOffset = { new Vector2(-1, -1), new Vector2(1, -1), new Vector2(1, 1), new Vector2(-1, 1) };

        public static void DrawTextWithShadow(SpriteBatch sb, string text, Vector2 position, SpriteFont font, Color color, Color shadowColor)
        {
            sb.DrawString(font, text, position + shadowOffset[0], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[1], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[2], shadowColor);
            sb.DrawString(font, text, position + shadowOffset[3], shadowColor);
            sb.DrawString(font, text, position, color);
        }

        public static Bitmap ResizeImage(Image img, int width, int height)
        {
            // Figure out the ratio
            double ratioX = (double)width / (double)img.Width;
            double ratioY = (double)height / (double)img.Height;
            double ratio = ratioX < ratioY ? ratioX : ratioY; // Use whichever multiplier is smaller

            // Now we can get the new height and width
            int newWidth = Convert.ToInt32(img.Width * ratio);
            int newHeight = Convert.ToInt32(img.Height * ratio);

            // Now calculate the X, Y position of the upper-left corner (one of these will always be zero)
            int posX = Convert.ToInt32((width - (img.Width * ratio)) / 2);
            int posY = Convert.ToInt32((height - (img.Height * ratio)) / 2);

            Bitmap bmp = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(img, posX, posY, newWidth, newHeight);
            }

            return bmp;
        }

        private const float rw = 0.212671f;
        private const float gw = 0.715160f;
        private const float bw = 0.072169f;

        public static Image ApplyColorMatrix(Image img, ColorMatrix matrix)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (ImageAttributes imgattr = new ImageAttributes())
                {
                    imgattr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgattr);
                }
            }

            return bmp;
        }

        public static ColorMatrix Colorize(Color color, float percentage)
        {
            float r = (float)color.R / 255;
            float g = (float)color.G / 255;
            float b = (float)color.B / 255;
            float amount = percentage / 100;
            float inv_amount = 1 - amount;

            return new ColorMatrix(new[]{
                new float[] {inv_amount + amount * r * rw, amount * g * rw, amount * b * rw, 0, 0},
                new float[] {amount * r * gw, inv_amount + amount * g * gw, amount * b * gw, 0, 0},
                new float[] {amount * r * bw, amount * g * bw, inv_amount + amount * b * bw, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}});
        }
    }
}