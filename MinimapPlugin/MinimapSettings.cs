namespace MinimapPlugin
{
    public class MinimapSettings
    {
        public int MinimapWidth { get; set; }
        public int MinimapHeight { get; set; }
        public float MinimapZoom { get; set; }
        public int PositionOffsetX { get; set; }
        public int PositionOffsetY { get; set; }
        public MinimapPosition MinimapPosition { get; set; }
        public int MinimapPositionOffset { get; set; }
        public float MinimapTransparency { get; set; }
        public bool ShowSky { get; set; }
    }
}