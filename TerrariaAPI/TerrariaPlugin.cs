using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;

namespace TerrariaAPI
{
    public abstract class TerrariaPlugin : IDisposable
    {
        public abstract string Name { get; }
        public abstract Version Version { get; }
        public abstract string Author { get; }
        public abstract string Description { get; }
        protected Main Game { get; private set; }

        protected TerrariaPlugin(Main game)
        {
            Game = game;
        }
        public virtual void Dispose()
        {
        }
    }

    public class ExamplePlugin : TerrariaPlugin
    {
        public ExamplePlugin(Main game)
            : base(game)
        {
            TerrariaHooks.OnUpdate += TerrariaHooks_OnUpdate;
        }
        public override void Dispose()
        {
            TerrariaHooks.OnUpdate -= TerrariaHooks_OnUpdate;
        }

        void TerrariaHooks_OnUpdate(GameTime obj)
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { return "ExamplePlugin"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override string Author
        {
            get { return "high"; }
        }

        public override string Description
        {
            get { return "just an example implementation"; }
        }
    }
}
