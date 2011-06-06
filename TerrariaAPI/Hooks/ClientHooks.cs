using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class ClientHooks
    {
        static ClientHooks()
        {
            NetHooks.OnPreSendData += NetHooks_OnPreSendData;
        }

        private static void NetHooks_OnPreSendData(SendDataEventArgs e)
        {
            if (Main.netMode != 2)
            {
                if (e.msgType == MsgTypes.ChatMessage)
                {
                    string msg = e.text;
                    e.Handled = Chat(ref msg);
                    e.text = msg;
                }
            }
        }

        public delegate void OnChatD(ref string msg, HandledEventArgs e);
        public static event OnChatD OnChat;

        public static bool Chat(ref string msg)
        {
            HandledEventArgs args = new HandledEventArgs();
            if (OnChat != null)
                OnChat(ref msg, args);
            return args.Handled;
        }
    }
}