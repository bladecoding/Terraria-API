using System;
using System.ComponentModel;
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

        public static event Action Update;
        public static event Action PostUpdate;

        public static void OnUpdate(bool pre)
        {
            if (pre)
            {
                if (Update != null)
                    Update();
            }
            else
            {
                if (PostUpdate != null)
                    PostUpdate();
            }
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

        private static void GameHooks_Update()
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

        public static event Action<HandledEventArgs> GetKeyState;

        public static bool OnGetKeyState()
        {
            if (GetKeyState == null)
                return false;

            var args = new HandledEventArgs();
            GetKeyState(args);
            return args.Handled;
        }
    }
}