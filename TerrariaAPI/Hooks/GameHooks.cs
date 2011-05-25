using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TerrariaAPI.Hooks
{
    public static class GameHooks
    {
        public static event Action<GameTime> OnUpdate;
        public static void Update(GameTime time)
        {
            if (OnUpdate != null)
                OnUpdate(time);
        }
        public static event Action<GameTime> OnDraw;
        public static void Draw(GameTime time)
        {
            if (OnDraw != null)
                OnDraw(time);
        }
    }
}
