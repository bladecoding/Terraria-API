using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class ItemHooks
    {
        public static event SetDefaultsD<Item, int> OnSetDefaultsInt;
        public static event SetDefaultsD<Item, string> OnSetDefaultsString;

        public static void SetDefaultsInt(ref int itemtype, Item item)
        {
            var args = new SetDefaultsEventArgs<Item, int>()
            {
                Object = item,
                Info = itemtype,
            };

            if (OnSetDefaultsInt != null)
                OnSetDefaultsInt(args);

            itemtype = args.Info;
        }

        public static void SetDefaultsString(ref string itemname, Item item)
        {
            var args = new SetDefaultsEventArgs<Item, string>()
            {
                Object = item,
                Info = itemname,
            };

            if (OnSetDefaultsString != null)
                OnSetDefaultsString(args);

            itemname = args.Info;
        }
    }
}