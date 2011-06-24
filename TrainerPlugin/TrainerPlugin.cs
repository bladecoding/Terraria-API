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
    /// <summary>
    /// F7 = Show trainer form
    /// </summary>
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
            get { return "Just a simple 'trainer'"; }
        }

        private TrainerForm trainerForm;
        private TrainerSettings trainerSettings, defaultSettings, currentSettings;
        private Texture2D gridTexture;
        private Texture2D border;

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
            DrawHooks.DrawTiles += DrawHooks_DrawTiles;
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
            if (Game.IsActive && trainerSettings != null)
            {
                if (InputManager.IsKeyPressed(Keys.F7))
                {
                    trainerForm.Visible = !trainerForm.Visible;
                }
                else if (InputManager.IsControlKeyDown && InputManager.IsKeyPressed(Keys.B) && trainerSettings.AllowBankOpen)
                {
                    TrainerHelper.OpenBank();
                }
                else if (InputManager.IsControlKeyDown && InputManager.IsKeyDown(Keys.Z, 250) && trainerSettings.CreateWater)
                {
                    TrainerHelper.AddLiquidToCursor(true);
                }
                else if (InputManager.IsControlKeyDown && InputManager.IsKeyDown(Keys.X, 250) && trainerSettings.CreateLava)
                {
                    TrainerHelper.AddLiquidToCursor(false);
                }

                if (InputManager.IsMouseButtonDown(MouseButtons.Right, 50) && trainerSettings.CreateTile)
                {
                    Item item = me.inventory[me.selectedItem];

                    if (item.active)
                    {
                        if (item.createTile >= 0)
                        {
                            TrainerHelper.AddTileToCursor(item.createTile, false, trainerSettings.BigBrushSize);
                        }
                        else if (item.createWall >= 0)
                        {
                            TrainerHelper.AddTileToCursor(item.createWall, true, trainerSettings.BigBrushSize);
                        }
                    }
                }
                else if (InputManager.IsMouseButtonDown(MouseButtons.Middle))
                {
                    if (trainerSettings.DestroyTile)
                    {
                        TrainerHelper.DestroyTileFromCursor(false, trainerSettings.BigBrushSize);
                    }

                    if (trainerSettings.DestroyWall)
                    {
                        TrainerHelper.DestroyTileFromCursor(true, trainerSettings.BigBrushSize);
                    }
                }
            }
        }

        private void PlayerHooks_UpdatePhysics(Player obj)
        {
            if (Game.IsActive && trainerSettings != null)
            {
                if (trainerSettings.EnableTrainer)
                {
                    currentSettings = trainerSettings;
                }
                else
                {
                    currentSettings = defaultSettings;
                }

                ApplySettings(currentSettings);
            }
        }

        private void ApplySettings(TrainerSettings settings)
        {
            if (settings.InfiniteHealth)
            {
                me.statLife = me.statLifeMax;
            }

            if (settings.InfiniteMana)
            {
                me.statMana = me.statManaMax;
            }

            if (settings.InfiniteBreath)
            {
                me.breath = me.breathMax;
            }

            if (settings.InfiniteAmmo)
            {
                foreach (Item item in me.inventory)
                {
                    if (item.ammo > 0)
                    {
                        item.stack = item.maxStack - 1;
                    }
                }
            }

            if (settings.NoFallDamage)
            {
                me.noFallDmg = true;
            }

            if (settings.NoKnockback)
            {
                me.noKnockback = true;
            }

            if (settings.JumpBoost)
            {
                me.jumpBoost = true;
            }

            if (settings.DoubleJump)
            {
                me.doubleJump = true;
            }

            if (settings.InfiniteJump)
            {
                me.jumpAgain = true;
            }

            if (settings.RocketBoots)
            {
                me.rocketBoots = true;
            }

            if (settings.UseFlipper)
            {
                me.accFlipper = true;
            }

            if (settings.FireWalk)
            {
                me.fireWalk = true;
            }

            if (settings.SpawnMax)
            {
                me.spawnMax = true;
            }

            if (settings.InstantRespawn)
            {
                me.respawnTimer = 0;
            }

            if (settings.NoPotionCooldown)
            {
                me.potionDelay = 0;
            }

            if (settings.NoManaCost)
            {
                me.manaCost = 0;
            }

            if (settings.LightYourCharacter)
            {
                TrainerHelper.LightCharacter();
            }

            if (settings.LightCursor)
            {
                TrainerHelper.LightCursor();
            }
        }

        private void DrawHooks_DrawTiles(SpriteBatch arg1, bool arg2, HandledEventArgs arg3)
        {
            // TODO: Not working properly

            if (!arg2 && currentSettings.LightTiles)
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
            }
        }

        private void DrawHooks_DrawInterface(SpriteBatch sb, HandledEventArgs e)
        {
            if (trainerSettings.DrawGrid)
            {
                TrainerHelper.DrawGrid(sb, gridTexture);
            }

            if (trainerSettings.DrawGridCursor)
            {
                TrainerHelper.DrawGridCursor(sb, border);
            }
        }
    }
}