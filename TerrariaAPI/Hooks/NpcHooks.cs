using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class NpcHooks
    {
        public static event SetDefaultsD<NPC, int> OnSetDefaultsInt;
        public static event SetDefaultsD<NPC, string> OnSetDefaultsString;

        public static void SetDefaultsInt(ref int npctype, NPC npc)
        {
            var args = new SetDefaultsEventArgs<NPC, int>()
            {
                Object = npc,
                Info = npctype,
            };

            if (OnSetDefaultsInt != null)
                OnSetDefaultsInt(args);

            npctype = args.Info;
        }
        public static void SetDefaultsString(ref string npcname, NPC npc)
        {
            var args = new SetDefaultsEventArgs<NPC, string>()
            {
                Object = npc,
                Info = npcname,
            };

            if (OnSetDefaultsString != null)
                OnSetDefaultsString(args);

            npcname = args.Info;
        }

        public delegate void StrikeNpcD(NpcStrikeEventArgs e);
        public static event StrikeNpcD OnStrikeNpc;
        public static bool StrikeNpc(NPC npc, ref int damage, ref float knockback, ref int hitdirection, ref double retdamage)
        {
            var args = new NpcStrikeEventArgs()
            {
                Npc = npc,
                Damage = damage,
                KnockBack = knockback,
                HitDirection = hitdirection,
                ReturnDamage = 0,
            };

            if (OnStrikeNpc != null)
                OnStrikeNpc(args);

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
