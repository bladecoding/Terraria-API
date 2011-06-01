using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;

namespace TeleportPlugin
{
    public class TeleportPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Teleport"; }
        }

        public override Version Version
        {
            get { return new Version(2, 0); }
        }

        public override string Author
        {
            get { return "Jaex / Fox-Face"; }
        }

        public override string Description
        {
            get { return "Plugin that allows teleportation"; }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 1); }
        }

        private const int ChatText = 0x19;

        private InputManager input = new InputManager();
        private TeleportHelper helper = new TeleportHelper();
        private TeleportForm teleportForm;

        public TeleportPlugin(Main game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            GameHooks.OnUpdate += GameHooks_OnUpdate;
            NetHooks.OnPreSendData += NetHooks_OnPreSendData;
            DrawHooks.OnEndDraw += DrawHooks_OnEndDraw;
        }

        public override void DeInitialize()
        {
            GameHooks.OnUpdate -= GameHooks_OnUpdate;
            NetHooks.OnPreSendData -= NetHooks_OnPreSendData;
            DrawHooks.OnEndDraw -= DrawHooks_OnEndDraw;
        }

        public void GameHooks_OnUpdate(GameTime gameTime)
        {
            if (Game.IsActive)
            {
                input.Update();

                if (input.IsKeyUp(Keys.F4, true))
                {
                    // TODO: TeleportForm need to use TeleportHelper also need redesign for show player list etc.
                    if (teleportForm == null)
                    {
                        teleportForm = new TeleportForm();
                    }

                    teleportForm.Show();
                    teleportForm.BringToFront();
                }
                else if (input.IsKeyDown(Keys.F5))
                {
                    helper.TeleportToLastPlayer();
                }
                else if (input.IsKeyDown(Keys.F6))
                {
                    helper.TeleportToLastLocation();
                }
                else if (input.IsKeyDown(Keys.F7))
                {
                    helper.TeleportToHome();
                }
                else if (input.IsKeyDown(Keys.F8, true))
                {
                    ShowHelp();
                }
            }
        }

        private void NetHooks_OnPreSendData(SendDataEventArgs e)
        {
            if (Main.netMode == 1 && e.msgType == ChatText)
            {
                e.Handled = OnMessageSend(e.number, e.text);
            }
        }

        private bool OnMessageSend(int playerIndex, string text)
        {
            if (playerIndex == Main.myPlayer)
            {
                ChatCommand msg = ChatCommand.Parse(text);

                if (msg != null)
                {
                    switch (msg.Command.ToLowerInvariant())
                    {
                        case "tp":
                        case "teleport":
                            if (!string.IsNullOrEmpty(msg.Parameter))
                            {
                                helper.TeleportToLocation(msg.Parameter);
                            }
                            else
                            {
                                helper.TeleportToLastLocation();
                            }

                            return true;
                        case "settp":
                        case "setteleport":
                            if (!string.IsNullOrEmpty(msg.Parameter))
                            {
                                helper.AddCurrentLocation(msg.Parameter);
                            }

                            return true;
                        case "tplist":
                        case "teleportlist":
                        case "locationlist":
                            string locationList = string.Join(", ", helper.Locations);
                            Main.NewText("Locations: " + locationList, 0, 255, 0);

                            return true;
                        case "ptp":
                        case "playerteleport":
                        case "partyteleport":
                            if (!string.IsNullOrEmpty(msg.Parameter))
                            {
                                helper.TeleportToPlayer(msg.Parameter);
                            }
                            else
                            {
                                helper.TeleportToLastPlayer();
                            }

                            return true;
                        case "plist":
                        case "playerlist":
                            List<string> players = helper.GetPlayerList();
                            string playerList = string.Join(", ", players);
                            Main.NewText("Players: " + playerList, 0, 255, 0);

                            return true;
                        case "home":
                            helper.TeleportToHome();

                            return true;
                        case "sethome":
                            // TODO: Sethome not working correctly yet

                            return true;
                        case "tphelp":
                            ShowHelp();

                            return true;
                    }
                }
            }

            return false;
        }

        private void DrawHooks_OnEndDraw(SpriteBatch obj)
        {
            if (Game.IsActive && !Main.playerInventory)
            {
                int depth = helper.GetDepth();

                string depthText;

                if (depth > 0)
                {
                    depthText = depth + " feet below";
                }
                else if (depth < 0)
                {
                    depth *= -1;
                    depthText = depth + " feet above";
                }
                else
                {
                    depthText = "Level";
                }

                string text = string.Format("Position: X {0}, Y {1}\r\nDepth: {2}", (int)helper.Me.position.X, (int)helper.Me.position.Y, depthText);
                DrawInfoTextWithShadow(text);
            }
        }

        private void DrawInfoTextWithShadow(string text)
        {
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

                Vector2 newPosition = new Vector2((float)(22 + x), (float)(74 + 22 + y));
                Game.spriteBatch.DrawString(Main.fontMouseText, text, newPosition, color);
            }
        }

        private void ShowHelp()
        {
            Main.NewText("Teleport plugin commands:", 0, 200, 50);
            Main.NewText("/tp [LocationName] (/teleport) - Teleports to location", 0, 255, 0);
            Main.NewText("/settp [LocationName] (/setteleport) - Current location will be added with name", 0, 255, 0);
            Main.NewText("/tplist (/teleportlist, /locationlist) - Lists saved location names", 0, 255, 0);
            Main.NewText("/ptp [PlayerName] (/playerteleport, /partyteleport) - Teleport to player position", 0, 255, 0);
            Main.NewText("/plist (/playerlist) - Shows online players names in server", 0, 255, 0);
            Main.NewText("/home - Teleports to spawn point", 0, 255, 0);
            Main.NewText("/sethome - Changes spawn point to your location", 0, 255, 0);
            Main.NewText("/tphelp - Shows this texts :)", 0, 255, 0);
        }
    }
}