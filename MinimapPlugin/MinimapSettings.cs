using System.ComponentModel;

namespace MinimapPlugin
{
    public class MinimapSettings
    {
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
        [DefaultValue(MinimapPosition.LeftBottom)]
        public MinimapPosition MinimapPosition { get; set; }
        [DefaultValue(10)]
        public int MinimapPositionOffset { get; set; }
        [DefaultValue(1.0f)]
        public float MinimapTransparency { get; set; }
        [DefaultValue(true)]
        public bool ShowSky { get; set; }
        [DefaultValue(true)]
        public bool ShowBorder { get; set; }
    }
}