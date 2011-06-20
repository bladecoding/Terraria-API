using System.ComponentModel;

namespace TrainerPlugin
{
    public class TrainerSettings
    {
        [DefaultValue(true), Description("For be able to enable/disable all settings quickly.")]
        public bool EnableTrainer { get; set; }
        [DefaultValue(false), Description("If health < max health then health = max health.")]
        public bool InfiniteHealth { get; set; }
        [DefaultValue(false), Description("If mana < max mana then mana = max mana.")]
        public bool InfiniteMana { get; set; }
        [DefaultValue(false), Description("If breath < max breath then breath = max breath.")]
        public bool InfiniteBreath { get; set; }
        [DefaultValue(false), Description("All your ammo items in your inventory will be max possible ammo stack. Including fallen star.")]
        public bool InfiniteAmmo { get; set; }
        [DefaultValue(false), Description("You won't take fall damage. (Lucky Horseshoe)")]
        public bool NoFallDamage { get; set; }
        [DefaultValue(false), Description("Prevents knockback, so you won't push backward when you take damage. (Cobalt Shield)")]
        public bool NoKnockback { get; set; }
        [DefaultValue(false), Description("Jump height and jump speed increase. (Shiny Red Balloon)")]
        public bool JumpBoost { get; set; }
        [DefaultValue(false), Description("You can double jump. (Cloud in a Bottle)")]
        public bool DoubleJump { get; set; }
        [DefaultValue(false), Description("You can jump more than two times with double jump. Double jump is required. You can't use rocket boots when this active.")]
        public bool InfiniteJump { get; set; }
        [DefaultValue(false), Description("You can fly with mana. (Rocket Boots)")]
        public bool RocketBoots { get; set; }
        [DefaultValue(false), Description("You can swim upwards in water by pressing the jump button. (Flipper)")]
        public bool UseFlipper { get; set; }
        [DefaultValue(false), Description("Grants immunity to fire blocks, i.e. Meteorite and Hellstone. (Obsidian Skull)")]
        public bool FireWalk { get; set; }
        [DefaultValue(false), Description("When you die you will respawn with maximum health and mana. (Nature's Gift)")]
        public bool SpawnMax { get; set; }
        [DefaultValue(false), Description("When you die you will respawn instantly.")]
        public bool InstantRespawn { get; set; }
        [DefaultValue(false), Description("Disables potion cooldown.")]
        public bool NoPotionCooldown { get; set; }
        [DefaultValue(false), Description("Items won't use mana.")]
        public bool NoManaCost { get; set; }
        [DefaultValue(false), Description("All tiles will be visible but also disables lighting.")]
        public bool LightTiles { get; set; }
        [DefaultValue(false), Description("Your character will emit light. Like orb of light.")]
        public bool LightYourCharacter { get; set; }
        [DefaultValue(false), Description("Your cursor position will emit light.")]
        public bool LightCursor { get; set; }
        [DefaultValue(false), Description("You can grab the sun and move it.")]
        public bool GrabSun { get; set; }
        [DefaultValue(false), Description("Stops npc spawns.")]
        public bool StopSpawns { get; set; }
        [DefaultValue(false), Description("Draws tile grid.")]
        public bool DrawGrid { get; set; }
        [DefaultValue(true), Description("When you press Ctrl + B piggy bank will open. Only works in single player.")]
        public bool AllowBankOpen { get; set; }
        [DefaultValue(true), Description("When you press Ctrl + Z creates water to where cursor position is.")]
        public bool CreateWater { get; set; }
        [DefaultValue(true), Description("When you press Ctrl + X creates Lava to where cursor position is.")]
        public bool CreateLava { get; set; }

        public TrainerSettings()
        {
            EnableTrainer = true;
            AllowBankOpen = true;
            CreateWater = true;
            CreateLava = true;
        }
    }
}