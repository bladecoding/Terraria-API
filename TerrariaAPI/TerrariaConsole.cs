using Microsoft.Xna.Framework;
using TerrariaAPI.Hooks;

namespace TerrariaAPI
{
    public class TerrariaConsole : XNAConsole
    {
        public TerrariaConsole(Game game)
            : base(game)
        {
            GameHooks.GetKeyState += (args) => args.Handled = Visible;
        }
    }
}