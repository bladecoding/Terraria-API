using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using XNAHelpers;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TrainerPlugin
{
    [APIVersion(1, 5)]
    public class TrainerPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Trainer"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override string Author
        {
            get { return "High / Jaex"; }
        }

        public override string Description
        {
            get { return "F7 = Open trainer window"; }
        }

        private TrainerForm trainerForm;
        private TrainerSettings trainerSettings, defaultSettings, currentSettings;
        private Texture2D gridTexture, border;

        private Player me
        {
            get { return Main.player[Main.myPlayer]; }
        }

        public TrainerPlugin(Main game)
            : base(game)
        {
            trainerSettings = new TrainerSettings();
            defaultSettings = new TrainerSettings();
            trainerForm = new TrainerForm(trainerSettings);
        }

        public override void Initialize()
        {
            GameHooks.LoadContent += GameHooks_LoadContent;
            GameHooks.Update += TerrariaHooks_Update;
            PlayerHooks.UpdatePhysics += PlayerHooks_UpdatePhysics;
            DrawHooks.DrawInterface += DrawHooks_DrawInterface;
        }

        private void GameHooks_LoadContent(ContentManager obj)
        {
            gridTexture = TrainerHelper.CreateGrid(Game.GraphicsDevice, 0.1f);
            border = DrawingHelper.CreateOnePixelTexture(Game.GraphicsDevice, Color.White);
        }

        public override void DeInitialize()
        {
            GameHooks.LoadContent -= GameHooks_LoadContent;
            GameHooks.Update -= TerrariaHooks_Update;
            PlayerHooks.UpdatePhysics -= PlayerHooks_UpdatePhysics;
            DrawHooks.DrawInterface -= DrawHooks_DrawInterface;
        }

        private void TerrariaHooks_Update(GameTime gameTime)
        {
            if (trainerSettings != null && Game.IsActive)
            {
                if (trainerSettings.EnableTrainer)
                {
                    currentSettings = trainerSettings;
                }
                else
                {
                    currentSettings = defaultSettings;
                }

                if (InputManager.IsKeyPressed(Keys.F7))
                {
                    trainerForm.Visible = !trainerForm.Visible;
                }

                if (GameHooks.IsWorldRunning)
                {
                    if (InputManager.IsControlKeyDown && InputManager.IsKeyPressed(Keys.B) && currentSettings.AllowBankOpen)
                    {
                        TrainerHelper.OpenBank();
                    }
                    else if (InputManager.IsControlKeyDown && InputManager.IsKeyDown(Keys.Z, 250) && currentSettings.CreateWater)
                    {
                        TrainerHelper.AddLiquidToCursor(true);
                    }
                    else if (InputManager.IsControlKeyDown && InputManager.IsKeyDown(Keys.X, 250) && currentSettings.CreateLava)
                    {
                        TrainerHelper.AddLiquidToCursor(false);
                    }

                    if (InputManager.IsMouseButtonDown(MouseButtons.Right, 50) && currentSettings.CreateTile)
                    {
                        Item item = me.inventory[me.selectedItem];

                        if (item.active)
                        {
                            if (item.createTile >= 0)
                            {
                                TrainerHelper.AddTileToCursor(item.createTile, false, currentSettings.BigBrushSize);
                            }
                            else if (item.createWall >= 0)
                            {
                                TrainerHelper.AddTileToCursor(item.createWall, true, currentSettings.BigBrushSize);
                            }
                        }
                    }
                    else if (InputManager.IsMouseButtonDown(MouseButtons.Middle))
                    {
                        if (currentSettings.DestroyTile)
                        {
                            TrainerHelper.DestroyTileFromCursor(false, currentSettings.BigBrushSize);
                        }

                        if (currentSettings.DestroyWall)
                        {
                            TrainerHelper.DestroyTileFromCursor(true, currentSettings.BigBrushSize);
                        }
                    }
                }
            }
        }

        private void PlayerHooks_UpdatePhysics(Player obj)
        {
            if (GameHooks.IsWorldRunning && currentSettings != null)
            {
                #region Abilities

                if (currentSettings.InfiniteHealth)
                {
                    me.statLife = me.statLifeMax;
                }

                if (currentSettings.InfiniteMana)
                {
                    me.statMana = me.statManaMax;
                }

                if (currentSettings.InfiniteBreath)
                {
                    me.breath = me.breathMax;
                }

                if (currentSettings.InfiniteAmmo)
                {
                    foreach (Item item in me.inventory)
                    {
                        if (item.ammo > 0)
                        {
                            item.stack = item.maxStack - 1;
                        }
                    }
                }

                if (currentSettings.InfiniteBuffTime)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (me.buffType[i] > 0 && me.buffTime[i] > 0)
                        {
                            me.buffTime[i] = 3600;
                        }
                    }
                }

                if (currentSettings.NoFallDamage)
                {
                    me.noFallDmg = true;
                }

                if (currentSettings.NoKnockback)
                {
                    me.noKnockback = true;
                }

                if (currentSettings.JumpBoost)
                {
                    me.jumpBoost = true;
                }

                if (currentSettings.DoubleJump)
                {
                    me.doubleJump = true;
                }

                if (currentSettings.InfiniteJump)
                {
                    me.jumpAgain = true;
                }

                if (currentSettings.RocketBoots)
                {
                    me.rocketBoots = true;
                }

                if (currentSettings.UseFlipper)
                {
                    me.accFlipper = true;
                }

                if (currentSettings.FireWalk)
                {
                    me.fireWalk = true;
                }

                if (currentSettings.SpawnMax)
                {
                    me.spawnMax = true;
                }

                if (currentSettings.InstantRespawn)
                {
                    me.respawnTimer = 0;
                }

                if (currentSettings.NoPotionCooldown)
                {
                    me.potionDelay = 0;
                }

                if (currentSettings.NoManaCost)
                {
                    me.manaCost = 0;
                }

                me.SpeedModifier *= currentSettings.MovementSpeed;

                #endregion Abilities

                #region Other

                if (currentSettings.LightYourCharacter)
                {
                    TrainerHelper.LightCharacter();
                }

                if (currentSettings.LightCursor)
                {
                    TrainerHelper.LightCursor();
                }

                if (currentSettings.AllowKillGuide)
                {
                    me.killGuide = true;
                }

                if (currentSettings.DeathAura)
                {
                    TrainerHelper.KillWhoTouchingMe();
                }

                #endregion Other

                #region Potions

                if (currentSettings.ObsidianSkin)
                {
                    me.lavaImmune = true;
                    me.fireWalk = true;
                }

                if (currentSettings.Regeneration)
                {
                    me.lifeRegen += 4;
                }

                if (currentSettings.Swiftness)
                {
                    me.SpeedModifier *= 1.25f;
                }

                if (currentSettings.Gills)
                {
                    me.gills = true;
                }

                if (currentSettings.Ironskin)
                {
                    me.statDefense += 10;
                }

                if (currentSettings.ManaRegeneration)
                {
                    me.manaRegen += 20;
                }

                if (currentSettings.MagicPower)
                {
                    me.magicBoost *= 1.2f;
                }

                if (currentSettings.Featherfall)
                {
                    me.slowFall = true;
                }

                if (currentSettings.Spelunker)
                {
                    me.findTreasure = true;
                }

                if (currentSettings.Invisibility)
                {
                    me.invis = true;
                }

                if (currentSettings.Shine)
                {
                    TrainerHelper.LightCharacter();
                }

                if (currentSettings.NightOwl)
                {
                    me.nightVision = true;
                }

                if (currentSettings.Battle)
                {
                    me.enemySpawns = true;
                }

                if (currentSettings.Thorns)
                {
                    me.thorns = true;
                }

                if (currentSettings.WaterWalking)
                {
                    me.waterWalk = true;
                }

                if (currentSettings.Archery)
                {
                    me.archer = true;
                }

                if (currentSettings.Hunter)
                {
                    me.detectCreature = true;
                }

                if (currentSettings.Gravitation)
                {
                    me.gravControl = true;
                }

                #endregion Potions
            }
        }

        private void DrawHooks_DrawTiles(SpriteBatch arg1, bool arg2, HandledEventArgs arg3)
        {
            // TODO: LightTiles

            /*if (!arg2 && currentSettings.LightTiles)
            {
                for (int l = Lighting.firstToLightX; l < Lighting.lastToLightX; l++)
                {
                    for (int m = Lighting.firstToLightY; m < Lighting.lastToLightY; m++)
                    {
                        if (l >= 0 && l < Main.maxTilesX && m >= 0 && m < Main.maxTilesY)
                        {
                            Lighting.color[l - Lighting.firstTileX + 21, m - Lighting.firstTileY + 21] = 1f;
                        }
                    }
                }
            }*/
        }

        private void DrawHooks_DrawInterface(SpriteBatch sb, HandledEventArgs e)
        {
            if (GameHooks.IsWorldRunning && currentSettings != null)
            {
                if (currentSettings.DrawGrid)
                {
                    TrainerHelper.DrawGrid(sb, gridTexture);
                }

                if (currentSettings.DrawGridCursor)
                {
                    TrainerHelper.DrawGridCursor(sb, border);
                }
            }
        }
    }
}