using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace TeleportPlugin
{
    internal class TeleportHelper
    {
        public List<TeleportLocation> Locations { get; set; }
        public TeleportLocation LastLocation { get; set; }
        public string LastPlayerName { get; set; }

        public TeleportHelper()
        {
            Locations = new List<TeleportLocation>();
        }

        public Player Me
        {
            get { return Main.player[Main.myPlayer]; }
        }

        public TeleportLocation AddCurrentLocation(string locationName)
        {
            TeleportLocation location = null;

            if (!string.IsNullOrEmpty(locationName))
            {
                location = FindLocation(locationName);

                if (location != null)
                {
                    location.Position = Me.position;
                }
                else
                {
                    location = new TeleportLocation(locationName, Me.position);
                    Locations.Add(location);
                }
            }

            return location;
        }

        public TeleportLocation FindLocation(string locationName)
        {
            if (!string.IsNullOrEmpty(locationName))
            {
                foreach (TeleportLocation location in Locations)
                {
                    if (location.Name.Equals(locationName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return location;
                    }
                }
            }

            return null;
        }

        public bool TeleportToLocation(string locationName)
        {
            TeleportLocation location = FindLocation(locationName);
            return TeleportToLocation(location);
        }

        public bool TeleportToLocation(TeleportLocation location)
        {
            if (location != null)
            {
                TeleportToPosition(location.Position);
                LastLocation = location;
                return true;
            }

            return false;
        }

        public bool TeleportToLastLocation()
        {
            if (LastLocation != null)
            {
                return TeleportToLocation(LastLocation);
            }

            return false;
        }

        public bool TeleportToPlayer(string playerName)
        {
            Player player = FindPlayerByName(playerName);
            return TeleportToPlayer(player);
        }

        public bool TeleportToPlayer(Player player)
        {
            if (player != null && player.active && !player.dead)
            {
                TeleportToPosition(player.position);
                LastPlayerName = player.name;
                return true;
            }

            return false;
        }

        public bool TeleportToLastPlayer()
        {
            if (!string.IsNullOrEmpty(LastPlayerName))
            {
                return TeleportToPlayer(LastPlayerName);
            }

            return false;
        }

        public void TeleportToPosition(float x, float y)
        {
            TeleportToPosition(new Vector2(x, y));
        }

        public void TeleportToPosition(Vector2 position)
        {
            Me.grappling[0] = -1;
            Me.grapCount = 0;
            Me.position = position;
            Me.fallStart = (int)(Me.position.Y / 16f);
        }

        public Player FindPlayerByName(string name, bool ignoreMe = true)
        {
            if (!string.IsNullOrEmpty(name))
            {
                foreach (Player player in Main.player)
                {
                    if (ignoreMe && player.whoAmi == Main.myPlayer)
                    {
                        continue;
                    }

                    if (player.name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return player;
                    }
                }
            }

            return null;
        }

        public void TeleportToHome()
        {
            float x = (float)(Me.SpawnX * 16 + 8 - Me.width / 2);
            float y = (float)(Me.SpawnY * 16 - Me.height);

            TeleportToPosition(x, y);
        }
    }
}