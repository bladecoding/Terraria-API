using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class DrawHooks
    {
        /// <summary>
        /// Called right before SpriteBatch.End
        /// </summary>
        public static event Action<SpriteBatch> OnEndDraw;
        public static void EndDraw(SpriteBatch batch)
        {
            if (OnEndDraw != null)
                OnEndDraw(batch);
        }

        /// <summary>
        /// Called right before DrawMenu
        /// </summary>
        public static event Action<SpriteBatch, HandledEventArgs> OnDrawMenu;
        public static bool DrawMenu(SpriteBatch batch)
        {
            var args = new HandledEventArgs();
            if (OnDrawMenu != null)
                OnDrawMenu(batch, args);
            return args.Handled;
        }
        /// <summary>
        /// Called right after DrawMenu
        /// </summary>
        public static event Action<SpriteBatch> OnEndDrawMenu;
        public static void EndDrawMenu(SpriteBatch batch)
        {
            if (OnEndDrawMenu != null)
                OnEndDrawMenu(batch);
        }

        /// <summary>
        /// arg2 = background
        /// Called right before DrawWater
        /// </summary>
        public static event Action<SpriteBatch, bool, HandledEventArgs> OnDrawWater;
        public static bool DrawWater(SpriteBatch batch, bool bg)
        {
            var args = new HandledEventArgs();
            if (OnDrawWater != null)
                OnDrawWater(batch, bg, args);
            return args.Handled;
        }

        /// <summary>
        /// arg2 = solid
        /// Called right before DrawTiles
        /// </summary>
        public static event Action<SpriteBatch, bool, HandledEventArgs> OnDrawTiles;
        public static bool DrawTiles(SpriteBatch batch, bool solid)
        {
            var args = new HandledEventArgs();
            if (OnDrawTiles != null)
                OnDrawTiles(batch, solid, args);
            return args.Handled;
        }

        /// <summary>
        /// arg2 = behindtiles
        /// Called right before DrawNpcs
        /// </summary>
        public static event Action<SpriteBatch, bool, HandledEventArgs> OnDrawNpcs;
        public static bool DrawNpcs(SpriteBatch batch, bool behindtiles)
        {
            var args = new HandledEventArgs();
            if (OnDrawNpcs != null)
                OnDrawNpcs(batch, behindtiles, args);
            return args.Handled;
        }

        /// <summary>
        /// Called right before DrawGore
        /// </summary>
        public static event Action<SpriteBatch, HandledEventArgs> OnDrawGore;
        public static bool DrawGore(SpriteBatch batch)
        {
            var args = new HandledEventArgs();
            if (OnDrawGore != null)
                OnDrawGore(batch, args);
            return args.Handled;
        }

        /// <summary>
        /// arg2 = player
        /// Called right before DrawNpcs
        /// </summary>
        public static event Action<SpriteBatch, Player, HandledEventArgs> OnDrawPlayer;
        public static bool DrawPlayer(SpriteBatch batch, Player player)
        {
            var args = new HandledEventArgs();
            if (OnDrawPlayer != null)
                OnDrawPlayer(batch, player, args);
            return args.Handled;
        }

        /// <summary>
        /// Called right before DrawInterface
        /// </summary>
        public static event Action<SpriteBatch, HandledEventArgs> OnDrawInterface;
        public static bool DrawInterface(SpriteBatch batch)
        {
            var args = new HandledEventArgs();
            if (OnDrawInterface != null)
                OnDrawInterface(batch, args);
            return args.Handled;
        }

    }

    public class DrawMenuEventArgs : EventArgs
    {

    }
}
