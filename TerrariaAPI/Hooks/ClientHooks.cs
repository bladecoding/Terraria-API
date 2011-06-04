using System;
using System.Text;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class ClientHooks
    {
        static ClientHooks()
        {
            NetHooks.OnPreSendData += NetHooks_OnPreSendData;
        }

        static void NetHooks_OnPreSendData(SendDataEventArgs e)
        {
            if (Main.netMode == 2)
                return;

            if (e.msgType == MsgTypes.ChatMessage)
            {
                string msg = e.text;
                e.Handled = Chat(ref msg);
                e.text = msg;

                if (Main.netMode == 0)
                    e.Handled = true; //Might as well, there is no where for the packet to go.
            }
        }

        public delegate void OnChatD(ref string msg, HandledEventArgs e);
        public static event OnChatD OnChat;

        public static bool Chat(ref string msg)
        {
            var args = new HandledEventArgs();
            if (OnChat != null)
                OnChat(ref msg, args);
            return args.Handled;
        }
    }
}