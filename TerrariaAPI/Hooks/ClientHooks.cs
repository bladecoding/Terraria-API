using System;
using System.ComponentModel;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class ClientHooks
    {
        static ClientHooks()
        {
            NetHooks.GetData += NetHooks_GetData;
            NetHooks.SendData += NetHooks_SendData;
        }

        private static void NetHooks_GetData(GetDataEventArgs e)
        {
            if (Main.netMode != 2 && e.MsgID == PacketTypes.ChatText && e.Length > 3)
            {
                byte playerID = e.Msg.readBuffer[e.Index];
                Color color = new Color(e.Msg.readBuffer[e.Index + 1] << 16, e.Msg.readBuffer[e.Index + 2] << 8, e.Msg.readBuffer[e.Index + 3]);
                string message = Encoding.ASCII.GetString(e.Msg.readBuffer, e.Index + 4, e.Length - 5);
                OnChatReceived(playerID, color, message);
            }
        }

        public delegate void ChatReceivedEventHandler(byte playerID, Color color, string message);
        public static event ChatReceivedEventHandler ChatReceived;

        public static void OnChatReceived(byte playerID, Color color, string message)
        {
#if CLIENT
            // 255 = Server
            if (playerID == 255)
            {
                Console.WriteLine("<Server> {0}", message);
            }
            else if (Main.player[playerID].active)
            {
                Console.WriteLine("<{0}> {1}", Main.player[playerID].name, message);
            }
#endif

            if (ChatReceived != null)
            {
                ChatReceived(playerID, color, message);
            }
        }

        private static void NetHooks_SendData(SendDataEventArgs e)
        {
            if (Main.netMode != 2 && e.MsgID == PacketTypes.ChatText)
            {
                string msg = e.text;
                e.Handled = OnChat(ref msg);
                e.text = msg;
            }
        }

        public delegate void OnChatD(ref string msg, HandledEventArgs e);
        public static event OnChatD Chat;

        public static bool OnChat(ref string msg)
        {
            if (Chat != null)
            {
                HandledEventArgs args = new HandledEventArgs();
                Chat(ref msg, args);
                return args.Handled;
            }

            return false;
        }
    }
}