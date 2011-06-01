using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
            get { return "Fox-Face / Jaex"; }
        }

        public override string Description
        {
            get { return "Plugin that allows teleportation"; }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 1); }
        }

        private const int chatMessage = 0x19;

        private List<TeleportLocation> teleports = new List<TeleportLocation>();
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
            NetHooks.OnPreSendData += new NetHooks.SendDataD(NetHooks_OnPreSendData);
        }

        public override void DeInitialize()
        {
            GameHooks.OnUpdate -= GameHooks_OnUpdate;
            NetHooks.OnPreSendData -= NetHooks_OnPreSendData;
        }

        public void GameHooks_OnUpdate(GameTime gameTime)
        {
            if (Game.IsActive)
            {
                input.Update();

                if (input.IsKeyUp(Keys.F4, true))
                {
                    if (teleportForm == null)
                        teleportForm = new TeleportForm();
                    teleportForm.Show();
                    teleportForm.BringToFront();
                }
                else if (input.IsKeyDown(Keys.F5))
                {
                    helper.TeleportToLastPlayer();
                }

                if (teleportForm != null && teleportForm.Visible)
                {
                    teleportForm.curPosLabel.Text = string.Format("Current position - X:{0} Y:{1}", Main.player[Main.myPlayer].position.X, Main.player[Main.myPlayer].position.Y);
                }
            }
        }

        private void NetHooks_OnPreSendData(SendDataEventArgs e)
        {
            if (Main.netMode == 1 && e.msgType == chatMessage)
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
                            if (!string.IsNullOrEmpty(msg.Parameter))
                            {
                                helper.TeleportToPlayerByName(msg.Parameter);
                            }
                            else
                            {
                                // If /tp then use last player
                                helper.TeleportToLastPlayer();
                            }

                            return true;
                    }
                }
            }

            return false;
        }
    }
}