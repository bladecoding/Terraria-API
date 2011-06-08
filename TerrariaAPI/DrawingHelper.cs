using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

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
    }
}