using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public class SendDataEventArgs : HandledEventArgs
    {
        public int msgType { get; set; }
        public int remoteClient { get; set; }
        public int ignoreClient { get; set; }
        public string text { get; set; }
        public int number { get; set; }
        public float number2 { get; set; }
        public float number3 { get; set; }
        public float number4 { get; set; }
    }
    public class GetDataEventArgs : HandledEventArgs
    {
        public byte MsgID { get; set; }
        public messageBuffer Msg { get; set; }
        public int Index { get; set; }
        public int Length { get; set; }
    }
    public static class NetHooks
    {
        public delegate void SendDataD(SendDataEventArgs e);
        public static event SendDataD OnPreSendData;
        public static event SendDataD OnPostSendData;
        public static bool SendData(bool pre, ref int msgType, ref int remoteClient, ref int ignoreClient, ref string text, ref int number, ref float number2, ref float number3, ref float number4)
        {
            var args = new SendDataEventArgs()
            {
                msgType = msgType,
                remoteClient = remoteClient,
                ignoreClient = ignoreClient,
                text = text,
                number = number,
                number2 = number2,
                number3 = number3,
                number4 = number4,
            };

            if (pre)
            {
                if (OnPreSendData != null)
                    OnPreSendData(args);
            }
            else
            {
                if (OnPostSendData != null)
                    OnPostSendData(args);
            }

            msgType = args.msgType;
            remoteClient = args.remoteClient;
            ignoreClient = args.ignoreClient;
            text = args.text;
            number = args.number;
            number2 = args.number2;
            number3 = args.number3;
            number4 = args.number4;

            return args.Handled;
        }



        public delegate void GetDataD(GetDataEventArgs e);
        public static event GetDataD OnPreGetData;
        public static bool GetData(ref byte msgid, messageBuffer msg, ref int idx, ref int length)
        {
            var args = new GetDataEventArgs()
            {
                MsgID = msgid,
                Msg = msg,
                Index = idx,
                Length = length
            };

            if (OnPreGetData != null)
                OnPreGetData(args);

            msgid = args.MsgID;
            idx = args.Index;
            length = args.Length;

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
