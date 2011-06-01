using Microsoft.Xna.Framework;
using Terraria;

namespace TeleportPlugin
{
    public class TeleportLocation
    {
        public string WorldName { get; set; }
        public int WorldID { get; set; }
        public string Name { get; set; }
        public Vector2 Position { get; set; }

        public TeleportLocation()
        {
        }

        public TeleportLocation(string locationName)
            : this(locationName, Main.player[Main.myPlayer].position)
        {
        }

        public TeleportLocation(string locationName, Vector2 position)
        {
            WorldName = Main.worldName;
            WorldID = Main.worldID;
            Name = locationName;
            Position = position;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}