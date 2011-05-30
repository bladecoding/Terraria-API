using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace TerrariaAPI.Hooks
{
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
        public static bool GetData(byte id, messageBuffer msg, int idx, int length)
        {
            var args = new HandledEventArgs();
            if (OnPreGetData != null)
                OnPreGetData(id, msg, idx, length, args);
            return args.Handled;
        }

        public delegate void GreetPlayerD(int who, HandledEventArgs e);
        public static event GreetPlayerD OnGreetPlayer;
        public static bool GreetPlayer(int who)
        {
            var args = new HandledEventArgs();
            if (OnGreetPlayer != null)
                OnGreetPlayer(who, args);
            return args.Handled;
        }
    }
}
