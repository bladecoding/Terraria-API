using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace TeleportPlugin
{
    public class TeleportHelper
    {
        public List<TeleportLocation> Locations { get; set; }
        public TeleportLocation LastLocation { get; set; }
        public string LastPlayerName { get; set; }
        public bool ShowInfoText { get; set; }

        public TeleportHelper()
        {
            Locations = new List<TeleportLocation>();
        }

        public Player Me
        {
            get { return Main.player[Main.myPlayer]; }
        }

        public List<TeleportLocation> GetCurrentWorldLocations()
        {
            List<TeleportLocation> locations = new List<TeleportLocation>();

            if (!string.IsNullOrEmpty(Main.worldName))
            {
                foreach (TeleportLocation location in Locations)
                {
                    if (location.WorldName == Main.worldName && location.WorldID == Main.worldID)
                    {
                        locations.Add(location);
                    }
                }
            }

            return locations;
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
                    Locations.Add(new TeleportLocation(locationName));
                }
            }

            return location;
        }

        public TeleportLocation FindLocation(string locationName)
        {
            if (!string.IsNullOrEmpty(locationName))
            {
                foreach (TeleportLocation location in GetCurrentWorldLocations())
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
            Me.velocity = Vector2.Zero;
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
            Me.FindSpawn();

            float x, y;

            if (Player.CheckSpawn(Me.SpawnX, Me.SpawnY))
            {
                x = (float)(Me.SpawnX * 16 + 8 - Me.width / 2);
                y = (float)(Me.SpawnY * 16 - Me.height);
            }
            else
            {
                x = (float)(Main.spawnTileX * 16 + 8 - Me.width / 2);
                y = (float)(Main.spawnTileY * 16 - Me.height);
            }

            TeleportToPosition(x, y);
        }

        public void SetHome()
        {
            SetHome(Me.position);
        }

        public void SetHome(Vector2 position)
        {
            int x = (int)(Me.position.X / 16);
            int y = (int)(Me.position.Y / 16);

            Me.ChangeSpawn(x, y);
            Main.NewText("Spawn point set!", 255, 240, 20);
        }

        public List<string> GetPlayerList()
        {
            List<string> players = new List<string>();

            foreach (Player player in Main.player)
            {
                if (player != null && player.active && !string.IsNullOrEmpty(player.name))
                {
                    players.Add(player.name);
                }
            }

            return players;
        }

        public int GetDepth()
        {
            return (int)((double)((Me.position.Y + (float)Me.height) * 2f / 16f) - Main.worldSurface * 2.0);
        }
    }
}