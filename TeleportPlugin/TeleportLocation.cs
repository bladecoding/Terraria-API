using Microsoft.Xna.Framework;

namespace TeleportPlugin
{
    public class TeleportLocation
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }

        public TeleportLocation(string name, Vector2 pos)
        {
            Name = name;
            Position = pos;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}