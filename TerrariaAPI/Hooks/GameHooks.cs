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
            GameHooks.Update += GameHooks_Update;
        }

        public static event Action<GameTime> Update;
        public static event Action<GameTime> PostUpdate;

        public static void OnUpdate(bool pre, GameTime time)
        {
            if (pre)
            {
                if (Update != null)
                    Update(time);
            }
            else
            {
                if (PostUpdate != null)
                    PostUpdate(time);
            }
        }

        public static event Action<ContentManager> LoadContent;

        public static void OnLoadContent(ContentManager manager)
        {
            if (LoadContent != null)
                LoadContent(manager);
        }

        public static event Action Initialize;
        public static event Action PostInitialize;

        public static void OnInitialize(bool pre)
        {
            if (pre)
            {
                if (Initialize != null)
                    Initialize();
            }
            else
            {
                if (PostInitialize != null)
                    PostInitialize();
            }
        }

        private static void GameHooks_Update(GameTime obj)
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