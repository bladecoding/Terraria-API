using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class NpcHooks
    {
        public static event SetDefaultsD<NPC, int> SetDefaultsInt;
        public static event SetDefaultsD<NPC, string> SetDefaultsString;

        public static void OnSetDefaultsInt(ref int npctype, NPC npc)
        {
            if (SetDefaultsInt == null)
                return;

            var args = new SetDefaultsEventArgs<NPC, int>()
            {
                Object = npc,
                Info = npctype,
            };

            SetDefaultsInt(args);

            npctype = args.Info;
        }

        public static void OnSetDefaultsString(ref string npcname, NPC npc)
        {
            if (SetDefaultsString == null)
                return;
            var args = new SetDefaultsEventArgs<NPC, string>()
            {
                Object = npc,
                Info = npcname,
            };

            SetDefaultsString(args);

            npcname = args.Info;
        }

        public delegate void StrikeNpcD(NpcStrikeEventArgs e);
        public static event StrikeNpcD StrikeNpc;

        public static bool OnStrikeNpc(NPC npc, ref int damage, ref float knockback, ref int hitdirection, ref double retdamage)
        {
            if (StrikeNpc == null)
                return false;

            var args = new NpcStrikeEventArgs()
            {
                Npc = npc,
                Damage = damage,
                KnockBack = knockback,
                HitDirection = hitdirection,
                ReturnDamage = 0,
            };

            StrikeNpc(args);

            retdamage = args.ReturnDamage;
            damage = args.Damage;
            knockback = args.KnockBack;
            hitdirection = args.HitDirection;

            return args.Handled;
        }
    }

    public class NpcStrikeEventArgs : HandledEventArgs
    {
        public NPC Npc { get; set; }

        public int Damage { get; set; }

        public float KnockBack { get; set; }

        public int HitDirection { get; set; }

        public double ReturnDamage { get; set; }
    }
}