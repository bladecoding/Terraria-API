using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class ItemHooks
    {
        public delegate void SetDefaultsIntD(int itemtype, Item item);
        public delegate void SetDefaultsStringD(string itemname, Item item);
        public static event SetDefaultsIntD OnSetDefaultsInt;
        public static event SetDefaultsStringD OnSetDefaultsString;

        public static void SetDefaultsInt(int itemtype, Item item)
        {
            if (OnSetDefaultsInt != null)
                OnSetDefaultsInt(itemtype, item);
        }
        public static void SetDefaultsString(string itemname, Item item)
        {
            if (OnSetDefaultsString != null)
                OnSetDefaultsString(itemname, item);
        }
    }
}
