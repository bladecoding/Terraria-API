using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class GameHooks
    {
        public static bool IsWorldRunning { get; private set; }

        private static bool oldGameMenu = true;

        static GameHooks()
        {
            GameHooks.OnUpdate += GameHooks_OnUpdate;
        }

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

        private static void GameHooks_OnUpdate(GameTime obj)
        {
            // Ugly workaround but it works
            if (oldGameMenu != Main.gameMenu)
            {
                oldGameMenu = Main.gameMenu;

                if (Main.gameMenu)
                {
                    OnWorldDisconnect();
                }
                else
                {
                    OnWorldConnect();
                }

                IsWorldRunning = !Main.gameMenu;
            }
        }

        public static event Action WorldConnect;

        public static void OnWorldConnect()
        {
            if (WorldConnect != null)
                WorldConnect();
        }

        public static event Action WorldDisconnect;

        public static void OnWorldDisconnect()
        {
            if (WorldDisconnect != null)
                WorldDisconnect();
        }
    }
}