using System;
using System.ComponentModel;
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
        public static event Action<SpriteBatch> EndDraw;

        public static void OnEndDraw(SpriteBatch batch)
        {
            if (EndDraw != null)
                EndDraw(batch);
        }

        /// <summary>
        /// Called right after SpriteBatch.End
        /// </summary>
        public static event Action<GameTime> DrawEnd;

        public static void OnDrawEnd(GameTime batch)
        {
            if (DrawEnd != null)
                DrawEnd(batch);
        }

        /// <summary>
        /// Called after RealDraw ScreenPosition set
        /// </summary>
        public static event Action RealDrawAfterScreenPosition;

        public static void OnRealDrawAfterScreenPosition()
        {
            if (RealDrawAfterScreenPosition != null)
                RealDrawAfterScreenPosition();
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