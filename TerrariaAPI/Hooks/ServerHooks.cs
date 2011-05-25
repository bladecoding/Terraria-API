using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrariaAPI.Hooks
{
    public class JoinEventArgs : EventArgs
    {
        public bool Allow { get; set; }
        public JoinEventArgs()
        {
            Allow = true;
        }
    }
    public class ChatEventArgs : EventArgs
    {
        public bool Handled { get; set; }
        public ChatEventArgs()
        {
            Handled = false;
        }
    }
    public static class ServerHooks
    {
        /// <summary>
        /// arg1 = WhoAmI
        /// </summary>
        public static event Action<int, JoinEventArgs> OnJoin;
        public static bool Join(int whoami)
        {
            var args = new JoinEventArgs();
            if (OnJoin != null)
                OnJoin(whoami, args);
            return args.Allow;
        }
        /// <summary>
        /// arg1 = WhoAmI
        /// </summary>
        public static event Action<int> OnLeave;
        public static void Leave(int whoami)
        {
            if (OnLeave != null)
                OnLeave(whoami);
        }

        /// <summary>
        /// arg1 = WhoAmI, arg2 = Message
        /// </summary>
        public static event Action<int, string, ChatEventArgs> OnChat;
        public static bool Chat(int whoami, string msg)
        {
            var args = new ChatEventArgs();
            if (OnChat != null)
                OnChat(whoami, msg, args);
            return args.Handled;
        }
    }
}
