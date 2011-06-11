using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TeleportPlugin
{
    /// <summary>
    /// F1 = Teleport to last player
    /// F2 = Teleport to last location
    /// F3 = Teleport to cursor position
    /// F4 = Open teleport form
    /// </summary>
    public class TeleportPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Teleport"; }
        }

        public override Version Version
        {
            get { return new Version(2, 1); }
        }

        public override string Author
        {
            get { return "Jaex"; }
        }

        public override string Description
        {
            get { return "Plugin that allows teleportation"; }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 2); }
        }

        public const string SettingsFilename = "TeleportSettings.xml";

        private InputManager input = new InputManager();
        private TeleportHelper helper;
        private TeleportForm teleportForm;

        public TeleportPlugin(Main game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            GameHooks.Update += GameHooks_Update;
            GameHooks.WorldConnect += GameHooks_WorldConnect;
            ClientHooks.Chat += ClientHooks_Chat;
            DrawHooks.EndDraw += DrawHooks_EndDraw;

            ThreadPool.QueueUserWorkItem(state => helper = TerrariaAPI.SettingsHelper.Load<TeleportHelper>(SettingsFilename));
        }

        public override void DeInitialize()
        {
            GameHooks.Update -= GameHooks_Update;
            GameHooks.WorldConnect -= GameHooks_WorldConnect;
            ClientHooks.Chat -= ClientHooks_Chat;
            DrawHooks.EndDraw -= DrawHooks_EndDraw;

            if (helper != null)
            {
                TerrariaAPI.SettingsHelper.Save(helper, SettingsFilename);
            }
        }

        public void GameHooks_Update(GameTime gameTime)
        {
            if (Game.IsActive && helper != null)
            {
                input.Update();

                if (input.IsKeyDown(Keys.F1))
                {
                    helper.TeleportToLastPlayer();
                }
                else if (input.IsKeyDown(Keys.F2))
                {
                    helper.TeleportToLastLocation();
                }
                else if (input.IsKeyDown(Keys.F3, true))
                {
                    helper.TeleportToCursor();
                }
                else if (input.IsKeyDown(Keys.F4, true))
                {
                    if (teleportForm == null || teleportForm.IsDisposed)
                    {
                        teleportForm = new TeleportForm(helper);
                    }

                    UpdateForm();
                    teleportForm.Show();
                    teleportForm.BringToFront();
                }
            }
        }

        private void GameHooks_WorldConnect()
        {
            UpdateForm();
        }

        private void ClientHooks_Chat(ref string msg, HandledEventArgs e)
        {
            if (helper != null)
            {
                ChatCommand chat = ChatCommand.Parse(msg);

                if (chat != null)
                {
                    switch (chat.Command.ToLowerInvariant())
                    {
                        case "tp":
                        case "teleport":
                            if (!string.IsNullOrEmpty(chat.Parameter))
                            {
                                helper.TeleportToLocation(chat.Parameter);
                            }
                            else
                            {
                                helper.TeleportToLastLocation();
                            }

                            e.Handled = true;
                            break;
                        case "settp":
                        case "setteleport":
                            if (!string.IsNullOrEmpty(chat.Parameter))
                            {
                                helper.AddCurrentLocation(chat.Parameter);
                                UpdateForm();
                            }

                            e.Handled = true;
                            break;
                        case "tplist":
                        case "teleportlist":
                        case "locationlist":
                            string locationList = string.Join(", ", helper.GetCurrentWorldLocations());
                            Main.NewText("Locations: " + locationList, 0, 255, 0);

                            e.Handled = true;
                            break;
                        case "ptp":
                        case "playerteleport":
                        case "partyteleport":
                            if (!string.IsNullOrEmpty(chat.Parameter))
                            {
                                helper.TeleportToPlayer(chat.Parameter);
                            }
                            else
                            {
                                helper.TeleportToLastPlayer();
                            }

                            e.Handled = true;
                            break;
                        case "plist":
                        case "playerlist":
                            List<string> players = helper.GetPlayerList();
                            string playerList = string.Join(", ", players);
                            Main.NewText("Players: " + playerList, 0, 255, 0);

                            e.Handled = true;
                            break;
                        case "home":
                            helper.TeleportToHome();

                            e.Handled = true;
                            break;
                        case "sethome":
                            // TODO: Sethome not working correctly yet

                            e.Handled = true;
                            break;
                        case "tpinfo":
                            helper.ShowInfoText = !helper.ShowInfoText;
                            UpdateForm();

                            e.Handled = true;
                            break;
                        case "tphelp":
                            ShowHelp(chat.Parameter);

                            e.Handled = true;
                            break;
                    }
                }
            }
        }

        private void DrawHooks_EndDraw(SpriteBatch obj)
        {
            if (Game.IsActive && helper != null && helper.ShowInfoText && !Main.playerInventory)
            {
                string text = string.Format("Position: X {0}, Y {1}\r\nDepth: {2}\r\nTime: {3}",
                    (int)helper.Me.position.X, (int)helper.Me.position.Y, helper.GetDepthText(), helper.GetTimeText());

                if (Main.netMode == 1)
                {
                    List<string> players = helper.GetPlayerList();
                    string playerList = string.Join(", ", players);
                    text += "\r\nPlayers: " + playerList;
                }

                DrawInfoTextWithShadow(text);
            }
        }

        private void DrawInfoTextWithShadow(string text)
        {
            int lineOffset = 0;

            for (int i = 0; i < 5; i++)
            {
                int x = 0;
                int y = 0;
                Color color = Color.Black;

                switch (i)
                {
                    case 0:
                        x = -2;
                        break;
                    case 1:
                        x = 2;
                        break;
                    case 2:
                        y = -2;
                        break;
                    case 3:
                        y = 2;
                        break;
                    case 4:
                        color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                        break;
                }

                Vector2 newPosition = new Vector2((float)(22 + x), (float)(74 + 22 * lineOffset + y));
                Game.spriteBatch.DrawString(Main.fontMouseText, text, newPosition, color);
            }
        }

        private void UpdateForm()
        {
            if (teleportForm != null && !teleportForm.IsDisposed)
            {
                teleportForm.UpdateAll();
            }
        }

        private void ShowHelp(string page)
        {
            if (string.IsNullOrEmpty(page) || page == "1")
            {
                Main.NewText("/tp [LocationName] (/teleport) - Teleports to location", 0, 255, 0);
                Main.NewText("/tp (F2, /teleport) - Teleports to last location", 0, 255, 0);
                Main.NewText("/settp [LocationName] (/setteleport) - Current location will be added with name", 0, 255, 0);
                Main.NewText("/tplist (/teleportlist, /locationlist) - Lists saved location names", 0, 255, 0);
                Main.NewText("/ptp [PlayerName] (/playerteleport, /partyteleport) - Teleports to player position", 0, 255, 0);
                Main.NewText("/ptp (F1, /playerteleport, /partyteleport) - Teleports to last player position", 0, 255, 0);
                Main.NewText("Page 1 / 2", 0, 255, 0);
            }
            else if (page == "2")
            {
                Main.NewText("/plist (/playerlist) - Shows online players names in server", 0, 255, 0);
                Main.NewText("/home (F3) - Teleports to spawn point", 0, 255, 0);
                Main.NewText("/sethome - Changes spawn point to your location", 0, 255, 0);
                Main.NewText("/tpinfo (F4) - Shows position, depth, player list in left top corner of screen", 0, 255, 0);
                Main.NewText("/tphelp [PageNumber] - Shows this texts :)", 0, 255, 0);
                Main.NewText("Page 2 / 2", 0, 255, 0);
            }
        }
    }
}