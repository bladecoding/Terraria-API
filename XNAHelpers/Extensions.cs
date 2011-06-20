using System;
using Microsoft.Xna.Framework;

namespace XNAHelpers
{
    public static class Extensions
    {
        public static float NextFloat(this Random rand)
        {
            return (float)rand.NextDouble();
        }

        public static float NextFloat(this Random rand, float max)
        {
            return rand.NextFloat() * max;
        }

        public static float NextFloat(this Random rand, float min, float max)
        {
            return min + rand.NextFloat() * (max - min);
        }

        public static Color NextColor(this Random rand)
        {
            return new Color(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
        }

        public static Color NextColor(this Random rand, Color min, Color max)
        {
            float random = rand.NextFloat();
            int r = (int)(min.R + (max.R - min.R) * random);
            int g = (int)(min.G + (max.G - min.G) * random);
            int b = (int)(min.B + (max.B - min.B) * random);
            return new Color(r, g, b);
        }

        public static float NextAngle(this Random rand)
        {
            return rand.NextFloat(MathHelpers.TwoPI);
        }

        public static int ToAbgr(this Color color)
        {
            return (color.A << 24) | (color.B << 16) | (color.G << 8) | color.R;
        }
    }
}