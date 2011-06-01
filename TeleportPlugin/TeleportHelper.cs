using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace TeleportPlugin
{
    internal class TeleportHelper
    {
        private string lastPlayerName;

        public Player Me
        {
            get { return Main.player[Main.myPlayer]; }
        }

        public bool TeleportToPlayerByName(string playerName)
        {
            Player player = FindPlayerByName(playerName);
            return TeleportToPlayer(player);
        }

        public bool TeleportToLastPlayer()
        {
            if (!string.IsNullOrEmpty(lastPlayerName))
            {
                return TeleportToPlayerByName(lastPlayerName);
            }

            return false;
        }

        public bool TeleportToPlayer(Player player)
        {
            if (player != null && player.active && !player.dead)
            {
                TeleportToPosition(player.position);
                lastPlayerName = player.name;
                return true;
            }

            return false;
        }

        public void TeleportToPosition(Vector2 position)
        {
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
    }
}