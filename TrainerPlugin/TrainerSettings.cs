using System.ComponentModel;

namespace TrainerPlugin
{
    public class TrainerSettings
    {
        [Category("\tGeneral"), DefaultValue(true), Description("For be able to enable/disable all settings quickly.")]
        public bool EnableTrainer { get; set; }

        [Category("Abilities"), DefaultValue(false), Description("If health < max health then health = max health.")]
        public bool InfiniteHealth { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("If mana < max mana then mana = max mana.")]
        public bool InfiniteMana { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("If breath < max breath then breath = max breath.")]
        public bool InfiniteBreath { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("All your ammo items in your inventory will be max possible ammo stack. Including fallen star.")]
        public bool InfiniteAmmo { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("Active buffs time set to 1 minute and freeze.")]
        public bool InfiniteBuffTime { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("You won't take fall damage. (Lucky Horseshoe)")]
        public bool NoFallDamage { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("Prevents knockback, so you won't push backward when you take damage. (Cobalt Shield)")]
        public bool NoKnockback { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("Jump height and jump speed increase. (Shiny Red Balloon)")]
        public bool JumpBoost { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("You can double jump. (Cloud in a Bottle)")]
        public bool DoubleJump { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("You can jump more than two times with double jump. Double jump is required. You can't use rocket boots when this active.")]
        public bool InfiniteJump { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("You can fly with mana. (Rocket Boots)")]
        public bool RocketBoots { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("You can swim upwards in water by pressing the jump button. (Flipper)")]
        public bool UseFlipper { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("Grants immunity to fire blocks, i.e. Meteorite and Hellstone. (Obsidian Skull)")]
        public bool FireWalk { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("When you die you will respawn with maximum health and mana. (Nature's Gift)")]
        public bool SpawnMax { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("When you die you will respawn instantly.")]
        public bool InstantRespawn { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("Disables potion cooldown.")]
        public bool NoPotionCooldown { get; set; }
        [Category("Abilities"), DefaultValue(false), Description("Items won't use mana.")]
        public bool NoManaCost { get; set; }
        [Category("Abilities"), DefaultValue(1f), Description("Movement speed will be multiplied with this number.")]
        public float MovementSpeed { get; set; }

        [Category("Building"), DefaultValue(true), Description("When you press right mouse button tile will be created to where cursor is. Tile type will be your current selected inventory item, if it can create tile.")]
        public bool CreateTile { get; set; }
        [Category("Building"), DefaultValue(true), Description("When you press middle mouse button tile will be destroyed from where cursor is.")]
        public bool DestroyTile { get; set; }
        [Category("Building"), DefaultValue(true), Description("When you press middle mouse button wall will be destroyed from where cursor is.")]
        public bool DestroyWall { get; set; }
        [Category("Building"), DefaultValue(false), Description("When using CreateTile/DestroyTile/DestroyWall 3x3 (9) tile/wall will be created/destroyed from where cursor is instead of 1x1 (1).")]
        public bool BigBrushSize { get; set; }
        [Category("Building"), DefaultValue(true), Description("When you press Ctrl + Z creates water to where cursor position is.")]
        public bool CreateWater { get; set; }
        [Category("Building"), DefaultValue(true), Description("When you press Ctrl + X creates Lava to where cursor position is.")]
        public bool CreateLava { get; set; }

        //[Category("Other"), DefaultValue(false), Description("All tiles will be visible and light.")]
        //public bool LightTiles { get; set; }
        [Category("Other"), DefaultValue(false), Description("Your character will emit light. Like orb of light.")]
        public bool LightYourCharacter { get; set; }
        [Category("Other"), DefaultValue(false), Description("Your cursor position will emit light.")]
        public bool LightCursor { get; set; }
        [Category("Other"), DefaultValue(false), Description("Draws tile grid.")]
        public bool DrawGrid { get; set; }
        [Category("Other"), DefaultValue(false), Description("Draws tile grid to where cursor position is.")]
        public bool DrawGridCursor { get; set; }
        [Category("Other"), DefaultValue(true), Description("When you press Ctrl + B piggy bank will open. Only works in single player.")]
        public bool AllowBankOpen { get; set; }
        [Category("Other"), DefaultValue(false), Description("You can kill guide npc.")]
        public bool AllowKillGuide { get; set; }
        [Category("Other"), DefaultValue(false), Description("If enemy NPC touch you, it will die instantly.")]
        public bool DeathAura { get; set; }

        [Category("Potions"), DefaultValue(false), Description("Provides immunity to lava.")]
        public bool ObsidianSkin { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Provides life regeneration.")]
        public bool Regeneration { get; set; }
        [Category("Potions"), DefaultValue(false), Description("%25 increased movement speed.")]
        public bool Swiftness { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Breathe water instead of air.")]
        public bool Gills { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Increase defense by 10.")]
        public bool Ironskin { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Increased mana regeneration.")]
        public bool ManaRegeneration { get; set; }
        [Category("Potions"), DefaultValue(false), Description("20% increased magic damage.")]
        public bool MagicPower { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Slows falling speed.")]
        public bool Featherfall { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Shows the location of treasure and ore.")]
        public bool Spelunker { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Grants invisibility.")]
        public bool Invisibility { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Emit an aura of light.")]
        public bool Shine { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Increased night vision.")]
        public bool NightOwl { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Increase enemy spawn rate.")]
        public bool Battle { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Attackers also take damage.")]
        public bool Thorns { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Allows the ability to walk on water.")]
        public bool WaterWalking { get; set; }
        [Category("Potions"), DefaultValue(false), Description("%15 increase arrow speed and damage.")]
        public bool Archery { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Shows the location of enemies.")]
        public bool Hunter { get; set; }
        [Category("Potions"), DefaultValue(false), Description("Allows the control of gravity.")]
        public bool Gravitation { get; set; }

        public TrainerSettings()
        {
            EnableTrainer = true;
            MovementSpeed = 1f;
            AllowBankOpen = true;
            CreateWater = true;
            CreateLava = true;
            CreateTile = true;
            DestroyTile = true;
            DestroyWall = true;
        }
    }
}