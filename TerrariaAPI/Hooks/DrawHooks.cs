using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class DrawHooks
    {
        /// <summary>
        /// Called right before SpriteBatch.End
        /// </summary>
        public static event Action<SpriteBatch> EndDraw;

        public static void OnEndDraw(SpriteBatch batch)
        {
            if (EndDraw != null)
                EndDraw(batch);
        }

        /// <summary>
        /// Called right after SpriteBatch.End
        /// </summary>
        public static event Action<SpriteBatch> DrawEnd;

        public static void OnDrawEnd(SpriteBatch batch)
        {
            if (DrawEnd != null)
                DrawEnd(batch);
        }

        /// <summary>
        /// Called right before DrawMenu
        /// </summary>
        public static event Action<SpriteBatch, HandledEventArgs> DrawMenu;

        public static bool OnDrawMenu(SpriteBatch batch)
        {
            if (DrawMenu == null)
                return false;

            var args = new HandledEventArgs();
            DrawMenu(batch, args);
            return args.Handled;
        }

        /// <summary>
        /// Called right after DrawMenu
        /// </summary>
        public static event Action<SpriteBatch> EndDrawMenu;

        public static void OnEndDrawMenu(SpriteBatch batch)
        {
            if (EndDrawMenu != null)
                EndDrawMenu(batch);
        }

        /// <summary>
        /// arg2 = background
        /// Called right before DrawWater
        /// </summary>
        public static event Action<SpriteBatch, bool, HandledEventArgs> DrawWater;

        public static bool OnDrawWater(SpriteBatch batch, bool bg)
        {
            if (DrawWater == null)
                return false;
            var args = new HandledEventArgs();
            DrawWater(batch, bg, args);
            return args.Handled;
        }

        /// <summary>
        /// arg2 = solid
        /// Called right before DrawTiles
        /// </summary>
        public static event Action<SpriteBatch, bool, HandledEventArgs> DrawTiles;

        public static bool OnDrawTiles(SpriteBatch batch, bool solid)
        {
            if (DrawTiles == null)
                return false;
            var args = new HandledEventArgs();
            DrawTiles(batch, solid, args);
            return args.Handled;
        }

        /// <summary>
        /// arg2 = behindtiles
        /// Called right before DrawNpcs
        /// </summary>
        public static event Action<SpriteBatch, bool, HandledEventArgs> DrawNpcs;

        public static bool OnDrawNpcs(SpriteBatch batch, bool behindtiles)
        {
            if (DrawNpcs == null)
                return false;
            var args = new HandledEventArgs();

            DrawNpcs(batch, behindtiles, args);
            return args.Handled;
        }

        /// <summary>
        /// Called right before DrawGore
        /// </summary>
        public static event Action<SpriteBatch, HandledEventArgs> DrawGore;

        public static bool OnDrawGore(SpriteBatch batch)
        {
            if (DrawGore == null)
                return false;
            var args = new HandledEventArgs();
            DrawGore(batch, args);
            return args.Handled;
        }

        /// <summary>
        /// arg2 = player
        /// Called right before DrawNpcs
        /// </summary>
        public static event Action<SpriteBatch, Player, HandledEventArgs> DrawPlayer;

        public static bool OnDrawPlayer(SpriteBatch batch, Player player)
        {
            if (DrawPlayer == null)
                return false;
            var args = new HandledEventArgs();
            DrawPlayer(batch, player, args);
            return args.Handled;
        }

        /// <summary>
        /// Called right before DrawInterface
        /// </summary>
        public static event Action<SpriteBatch, HandledEventArgs> DrawInterface;

        public static bool OnDrawInterface(SpriteBatch batch)
        {
            if (DrawInterface == null)
                return false;
            var args = new HandledEventArgs();
            DrawInterface(batch, args);
            return args.Handled;
        }
    }
}