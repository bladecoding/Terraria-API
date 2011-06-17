using System;
using System.Text;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class ServerHooks
    {
        static ServerHooks()
        {
            NetHooks.GetData += NetHooks_GetData;
        }

        private static void NetHooks_GetData(GetDataEventArgs e)
        {
            if (Main.netMode != 2)
                return;

            if (e.MsgID == PacketTypes.ConnectRequest)
            {
                e.Handled = OnJoin(e.Msg.whoAmI);
                if (e.Handled)
                    Netplay.serverSock[e.Msg.whoAmI].kill = true;
            }
            else if (e.MsgID == PacketTypes.ChatText)
            {
                string str = Encoding.ASCII.GetString(e.Msg.readBuffer, e.Index + 0x4, e.Length - 0x5);
                e.Handled = OnChat(e.Msg, e.Msg.whoAmI, str);
            }
        }

        public delegate void CommandD(string cmd, HandledEventArgs e);
        /// <summary>
        /// On console command
        /// </summary>
        public static event CommandD Command;

        public static bool OnCommand(string cmd)
        {
            if (Command == null)
                return false;

            var args = new HandledEventArgs();
            Command(cmd, args);
            return args.Handled;
        }

        /// <summary>
        /// arg1 = WhoAmI
        /// </summary>
        public static event Action<int, HandledEventArgs> Join;

        public static bool OnJoin(int whoami)
        {
            if (Join == null)
                return false;

            var args = new HandledEventArgs();
            Join(whoami, args);
            return args.Handled;
        }

        /// <summary>
        /// arg1 = WhoAmI
        /// </summary>
        public static event Action<int> Leave;

        public static void OnLeave(int whoami)
        {
            if (Leave != null)
                Leave(whoami);
        }

        /// <summary>
        /// arg1 = Msg, arg2 = WhoAmI, arg3 = Text
        /// </summary>
        public static event Action<messageBuffer, int, string, HandledEventArgs> Chat;

        public static bool OnChat(messageBuffer msg, int whoami, string text)
        {
            if (Chat == null)
                return false;

            var args = new HandledEventArgs();
            Chat(msg, whoami, text, args);
            return args.Handled;
        }
    }
}