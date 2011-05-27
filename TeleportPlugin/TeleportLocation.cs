using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TeleportPlugin
{
    public class TeleportLocation
    {
        public string Name;
        public Vector2 Position;

        public override string ToString()
        {
            return Name;
        }

        public TeleportLocation(string name, Vector2 pos)
        {
            Name = name;
            Position = pos;
        }
    }
}
