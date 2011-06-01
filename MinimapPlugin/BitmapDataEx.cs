using System.Drawing;
using System.Drawing.Imaging;

public static class BitmapDataExt
{
    public unsafe static void SetPixelInt(this BitmapData bd, uint pos, uint c)
    {
        if (pos < 0 || pos >= (bd.Width * bd.Height))
            return;

        *((uint*)bd.Scan0.ToPointer() + pos) = c;
    }

    public static void SetPixelInt(this BitmapData bd, uint x, uint y, uint c)
    {
        bd.SetPixelInt((uint)((y * bd.Width) + x), c);
    }

    public static void SetPixel(this BitmapData bd, uint x, uint y, Color c)
    {
        bd.SetPixelInt(x, y, (uint)c.ToArgb());
    }

    public static void SetPixelInt(this BitmapData bd, int x, int y, int c)
    {
        bd.SetPixelInt((uint)x, (uint)y, (uint)c);
    }

    public static void SetPixel(this BitmapData bd, int x, int y, Color c)
    {
        bd.SetPixel((uint)x, (uint)y, c);
    }

    public unsafe static uint GetPixelInt(this BitmapData bd, uint pos)
    {
        if (pos < 0 || pos >= (bd.Width * bd.Height))
            return 0x00000000;
        return *((uint*)bd.Scan0.ToPointer() + pos);
    }

    public static uint GetPixelInt(this BitmapData bd, uint x, uint y)
    {
        return bd.GetPixelInt((uint)((y * bd.Width) + x));
    }

    public static Color GetPixel(this BitmapData bd, uint x, uint y)
    {
        return Color.FromArgb((int)bd.GetPixelInt(x, y));
    }

    public static int GetPixelInt(this BitmapData bd, int x, int y)
    {
        return (int)bd.GetPixelInt((uint)x, (uint)y);
    }

    public static Color GetPixel(this BitmapData bd, int x, int y)
    {
        return bd.GetPixel((uint)x, (uint)y);
    }
}