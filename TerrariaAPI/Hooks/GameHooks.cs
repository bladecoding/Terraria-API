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
