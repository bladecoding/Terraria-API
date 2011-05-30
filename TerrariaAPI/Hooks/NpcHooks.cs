using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace TerrariaAPI.Hooks
{
    public static class NpcHooks
    {
        public delegate void SetDefaultsIntD(int npctype, NPC npc);
        public delegate void SetDefaultsStringD(string npcname, NPC npc);
        public static event SetDefaultsIntD OnSetDefaultsInt;
        public static event SetDefaultsStringD OnSetDefaultsString;

        public static void SetDefaultsInt(int npctype, NPC npc)
        {
            if (OnSetDefaultsInt != null)
                OnSetDefaultsInt(npctype, npc);
        }
        public static void SetDefaultsString(string npcname, NPC npc)
        {
            if (OnSetDefaultsString != null)
                OnSetDefaultsString(npcname, npc);
        }

        public delegate void StrikeNpcD(NPC npc, int damage, float knockback, int hitdirection, NpcStrikeEventArgs e);
        public static event StrikeNpcD OnStrikeNpc;
        public static bool StrikeNpc(NPC npc, int damage, float knockback, int hitdirection, out double retdamage)
        {
            var args = new NpcStrikeEventArgs();
            if (OnStrikeNpc != null)
                OnStrikeNpc(npc, damage, knockback, hitdirection, args);
            retdamage = args.Damage;
            return args.Handled;
        }


        public class NpcStrikeEventArgs : HandledEventArgs
        {
            public double Damage { get; set; }
        }
    }
}
