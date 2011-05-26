using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TerrariaAPI.Hooks
{
    public static class GameHooks
    {
        public static event Action<GameTime> OnUpdate;
        public static void Update(GameTime time)
        {
            if (OnUpdate != null)
                OnUpdate(time);
        }
        public static event Action<ContentManager> OnLoadContent;
        public static void LoadContent(ContentManager manager)
        {
            if (OnLoadContent != null)
                OnLoadContent(manager);
        }

        /// <summary>
        /// Called right after SpriteBatch.Begin
        /// </summary>
        public static event Action<GameTime, SpriteBatch> OnBeginDraw;
        public static void BeginDraw(GameTime time, SpriteBatch batch)
        {
            if (OnBeginDraw != null)
                OnBeginDraw(time, batch);
        }
        /// <summary>
        /// Called right before SpriteBatch.End
        /// </summary>
        public static event Action<GameTime, SpriteBatch> OnEndDraw;
        public static void EndDraw(GameTime time, SpriteBatch batch)
        {
            if (OnEndDraw != null)
                OnEndDraw(time, batch);
        }

        public static event Action OnPreInitialize;
        public static void PreInitialize()
        {
            if (OnPreInitialize != null)
                OnPreInitialize();
        }
        public static event Action OnPostInitialize;
        public static void PostInitialize()
        {
            if (OnPostInitialize != null)
                OnPostInitialize();
        }
    }
}
