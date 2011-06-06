using System.ComponentModel;

namespace TrainerPlugin
{
    public class TrainerSettings
    {
        [DefaultValue(true), Description("For be able to enable/disable all settings quickly.")]
        public bool EnableTrainer { get; set; }
        [DefaultValue(false), Description("Unlimited life.")]
        public bool GodMode { get; set; }
        [DefaultValue(false), Description("If mana < max mana then mana = max mana.")]
        public bool InfiniteMana { get; set; }
        [DefaultValue(false), Description("If breath < max breath then breath = max breath.")]
        public bool InfiniteBreath { get; set; }
        [DefaultValue(false), Description("All your ammo items will be max possible ammo stack (Including fallen star).")]
        public bool InfiniteAmmo { get; set; }
        [DefaultValue(false), Description("All tiles will be visible but also disables lighting.")]
        public bool LightTiles { get; set; }
        [DefaultValue(false), Description("Your character will emit light. Like orb of light.")]
        public bool LightYourCharacter { get; set; }
        [DefaultValue(false), Description("Your cursor position will emit light.")]
        public bool LightCursor { get; set; }
        [DefaultValue(false), Description("Use arrow keys to navigate screen position, also your cursor position will emit light.")]
        public bool DebugMode { get; set; }
        [DefaultValue(false), Description("You can grab sun and move it.")]
        public bool GrabSub { get; set; }
        [DefaultValue(false), Description("Stops npc spawns.")]
        public bool StopSpawns { get; set; }
        [DefaultValue(false), Description("Dumb npcs.")]
        public bool DumbAI { get; set; }

        public TrainerSettings()
        {
            EnableTrainer = true;
        }
    }
}