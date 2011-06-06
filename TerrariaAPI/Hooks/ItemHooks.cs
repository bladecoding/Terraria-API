using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class ItemHooks
    {
        public static event SetDefaultsD<Item, int> SetDefaultsInt;
        public static event SetDefaultsD<Item, string> SetDefaultsString;

        public static void OnSetDefaultsInt(ref int itemtype, Item item)
        {
            if (SetDefaultsInt == null)
                return;

            var args = new SetDefaultsEventArgs<Item, int>()
            {
                Object = item,
                Info = itemtype,
            };

            SetDefaultsInt(args);

            itemtype = args.Info;
        }

        public static void OnSetDefaultsString(ref string itemname, Item item)
        {
            if (SetDefaultsString == null)
                return;
            var args = new SetDefaultsEventArgs<Item, string>()
            {
                Object = item,
                Info = itemname,
            };

            SetDefaultsString(args);

            itemname = args.Info;
        }
    }
}