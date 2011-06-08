using System;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;

namespace TempPlugin
{
    public class TempPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "?"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 2); }
        }

        public override string Author
        {
            get { return "?"; }
        }

        public override string Description
        {
            get { return "?"; }
        }

        public TempPlugin(Main game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            GameHooks.Update += GameHooks_Update;
        }

        public override void DeInitialize()
        {
            GameHooks.Update -= GameHooks_Update;
        }

        private void GameHooks_Update(GameTime obj)
        {
            throw new NotImplementedException();
        }
    }
}