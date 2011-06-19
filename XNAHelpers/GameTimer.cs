using System;
using Microsoft.Xna.Framework;

namespace XNAHelpers
{
    public class GameTimer
    {
        public float Interval { get; set; }

        public TimeSpan ElapsedTime { get; private set; }

        public float ElapsedSeconds
        {
            get { return (float)ElapsedTime.TotalSeconds; }
        }

        public GameTimer(float interval)
        {
            Interval = interval;
            Reset();
        }

        public bool Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime;

            if (ElapsedSeconds >= Interval)
            {
                Reset();
                return true;
            }

            return false;
        }

        public void Reset()
        {
            ElapsedTime = TimeSpan.Zero;
        }
    }
}