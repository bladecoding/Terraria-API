using System.ComponentModel;

namespace MinimapPlugin
{
    public class MinimapSettings
    {
        [DefaultValue(true)]
        public bool ShowMinimap { get; set; }
        [DefaultValue(200)]
        public int MinimapWidth { get; set; }
        [DefaultValue(150)]
        public int MinimapHeight { get; set; }
        [DefaultValue(1.0f)]
        public float MinimapZoom { get; set; }
        [DefaultValue(0)]
        public int PositionOffsetX { get; set; }
        [DefaultValue(0)]
        public int PositionOffsetY { get; set; }
        [DefaultValue(MinimapPosition.RightBottom)]
        public MinimapPosition MinimapPosition { get; set; }
        [DefaultValue(10)]
        public int MinimapPositionOffset { get; set; }
        [DefaultValue(1.0f)]
        public float MinimapTransparency { get; set; }
        [DefaultValue(true)]
        public bool ShowSky { get; set; }
        [DefaultValue(true)]
        public bool ShowBorder { get; set; }
        [DefaultValue(true)]
        public bool ShowCrosshair { get; set; }

        public MinimapSettings()
        {
            ShowMinimap = true;
            MinimapWidth = 200;
            MinimapHeight = 150;
            MinimapZoom = 1.0f;
            PositionOffsetX = 0;
            PositionOffsetY = 0;
            MinimapPosition = MinimapPosition.RightBottom;
            MinimapPositionOffset = 10;
            MinimapTransparency = 1.0f;
            ShowSky = true;
            ShowBorder = true;
            ShowCrosshair = true;
        }
    }
}