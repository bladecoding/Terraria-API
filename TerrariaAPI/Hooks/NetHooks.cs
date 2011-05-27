using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public class AllowEventArgs : EventArgs
    {
        public bool Allow { get; set; }
        public AllowEventArgs()
        {
            Allow = true;
        }
    }
    public class HandledEventArgs : EventArgs
    {
        public bool Handled { get; set; }
        public HandledEventArgs()
        {
            Handled = false;
        }
    }
    public static class ServerHooks
    {
        /// <summary>
        /// arg1 = WhoAmI
        /// </summary>
        public static event Action<int, AllowEventArgs> OnJoin;
        public static bool Join(int whoami)
        {
            var args = new AllowEventArgs();
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
        public static event Action<int, string, HandledEventArgs> OnChat;
        public static bool Chat(int whoami, string msg)
        {
            var args = new HandledEventArgs();
            if (OnChat != null)
                OnChat(whoami, msg, args);
            return args.Handled;
        }
    }

    public static class NetHooks
    {
        public delegate void SendDataD(int msgType, int remoteClient, int ignoreClient, string text, int number, float number2, float number3, float number4, HandledEventArgs e);
        public static event SendDataD OnPreSendData;
        public static event SendDataD OnPostSendData;
        public static bool SendData(bool pre, int msgType, int remoteClient, int ignoreClient, string text, int number, float number2, float number3, float number4)
        {
            var args = new HandledEventArgs();
            if (pre)
            {
                if (OnPreSendData != null)
                    OnPreSendData(msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, args);
            }
            else
            {
                if (OnPostSendData != null)
                    OnPostSendData(msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, args);
            }
            return args.Handled;
        }
        public delegate void GetDataD(byte id, messageBuffer msg, int idx, int length, HandledEventArgs e);
        public static event GetDataD OnPreGetData;
        /// <summary>
        /// DO NOT USE. Will rarely get called due to GetData returning before the end.
        /// </summary>
        public static event GetDataD OnPostGetData;
        public static bool GetData(byte id, messageBuffer msg, int idx, int length)
        {
            var args = new HandledEventArgs();
            if (OnPreGetData != null)
                OnPreGetData(id, msg, idx, length, args);
            return args.Handled;
        }

    }
}
