using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using XNAHelpers;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MinimapPlugin
{
    /// <summary>
    /// F5 = Show/Hide minimap
    /// F6 = Show minimap settings form
    /// </summary>
    [APIVersion(1, 5)]
    public class MinimapPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Minimap"; }
        }

        public override Version Version
        {
            get { return new Version(2, 0); }
        }

        public override string Author
        {
            get { return "High / Jaex"; }
        }

        public override string Description
        {
            get { return "Its a minimap, what do you think?"; }
        }

        public const string SettingsFilename = "MinimapSettings.xml";

        private WorldRenderer rend;
        private Texture2D minimap;
        // private Texture2D chest;
        private Thread renderthread;
        private MinimapSettings settings;
        private MinimapForm settingsForm;

        private bool IsDrawingAllowed
        {
            get
            {
                return Game.IsActive && settings != null && settings.ShowMinimap && rend != null;
            }
        }

        public MinimapPlugin(Main main)
            : base(main)
        {
        }

        public override void Initialize()
        {
            // GameHooks.OnLoadContent += GameHooks_OnLoadContent;
            GameHooks.Update += GameHooks_Update;
            GameHooks.WorldConnect += GameHooks_WorldConnect;
            GameHooks.WorldDisconnect += GameHooks_WorldDisconnect;
            DrawHooks.EndDraw += DrawHooks_EndDraw;
            renderthread = new Thread(RenderMap);
            renderthread.Start();

            string path = Path.Combine(Program.PluginSettingsPath, SettingsFilename);
            ThreadPool.QueueUserWorkItem(state => settings = SettingsHelper.Load<MinimapSettings>(path));
        }

        public override void DeInitialize()
        {
            renderthread = null;
            // GameHooks.OnLoadContent -= GameHooks_OnLoadContent;
            GameHooks.Update -= GameHooks_Update;
            GameHooks.WorldConnect -= GameHooks_WorldConnect;
            GameHooks.WorldDisconnect -= GameHooks_WorldDisconnect;
            DrawHooks.EndDraw -= DrawHooks_EndDraw;

            if (settings != null)
            {
                string path = Path.Combine(Program.PluginSettingsPath, SettingsFilename);
                SettingsHelper.Save(settings, path);
            }
        }

        private void GameHooks_OnLoadContent(ContentManager obj)
        {
            // chest = BitmapToTexture(Game.GraphicsDevice, Properties.Resources.chest);
        }

        private void GameHooks_Update(GameTime gameTime)
        {
            if (Game.IsActive && settings != null)
            {
                if (InputManager.IsKeyPressed(Keys.F5))
                {
                    settings.ShowMinimap = !settings.ShowMinimap;
                }
                else if (InputManager.IsKeyPressed(Keys.F6))
                {
                    if (settingsForm == null || settingsForm.IsDisposed)
                    {
                        settingsForm = new MinimapForm(settings);
                    }

                    settingsForm.Show();
                    settingsForm.BringToFront();
                }
            }
        }

        private void GameHooks_WorldConnect()
        {
            rend = new WorldRenderer(Main.tile, Main.maxTilesX, Main.maxTilesY, Main.worldSurface);
        }

        private void GameHooks_WorldDisconnect()
        {
            rend = null;
        }

        private void DrawHooks_EndDraw(SpriteBatch arg1)
        {
            if (IsDrawingAllowed && minimap != null && !Main.playerInventory)
            {
                Vector2 position;

                if (settings.MinimapPosition == MinimapPosition.RightBottom)
                {
                    position = new Vector2(Main.screenWidth - minimap.Width - settings.MinimapPositionOffset,
                        Main.screenHeight - minimap.Height - settings.MinimapPositionOffset);
                }
                else
                {
                    position = new Vector2(settings.MinimapPositionOffset, Main.screenHeight - minimap.Height - settings.MinimapPositionOffset);
                }

                Game.spriteBatch.Draw(minimap, position, Color.White * settings.MinimapTransparency);
                // DrawPlayers();
            }
        }

        private void RenderMap()
        {
            while (renderthread != null)
            {
                if (IsDrawingAllowed)
                {
                    int curx = (int)(Main.player[Main.myPlayer].position.X / 16) + settings.PositionOffsetX;
                    int cury = (int)(Main.player[Main.myPlayer].position.Y / 16) + settings.PositionOffsetY;
                    int width = settings.MinimapWidth;
                    int height = settings.MinimapHeight;

                    int[] img = rend.GenerateMinimap(curx, cury, width, height, settings.MinimapZoom, settings.ShowSky, settings.ShowBorder, settings.ShowCrosshair);

                    minimap = DrawingHelper.IntsToTexture(Game.GraphicsDevice, img, width, height);
                }

                Thread.Sleep(100);
            }
        }

        /*private void DrawPlayers()
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].active)
                {
                    int mex = (int)(Main.player[Main.myPlayer].position.X / 16);
                    int mey = (int)(Main.player[Main.myPlayer].position.Y / 16);
                    int targetx = (int)(Main.player[i].position.X / 16);
                    int targety = (int)(Main.player[i].position.Y / 16);

                    if (targetx < mex - 100)
                        continue;
                    if (targetx > mex + 100)
                        continue;
                    if (targety < mey - 100)
                        continue;
                    if (targety > mey + 100)
                        continue;

                    targetx = targetx - mex + 100;
                    targety = targety - mey + 100;

                    targetx -= Main.player[i].width / 2;
                    targety -= Main.player[i].height;

                    Game.spriteBatch.Draw(chest, new Vector2(Main.screenWidth - minimap.Width + targetx, Main.screenHeight - minimap.Height + targety), Color.White);
                }
            }
        }*/
    }
}