using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TerrariaAPI
{
    public static class TerrariaHooks
    {
        public static event Action<GameTime> OnUpdate;
        public static void Update(GameTime time)
        {
            if (OnUpdate != null)
                OnUpdate(time);
        }
    }
}
