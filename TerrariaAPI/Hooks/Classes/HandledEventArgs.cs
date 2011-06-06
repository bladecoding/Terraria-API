using System;

namespace TerrariaAPI.Hooks
{
    public class HandledEventArgs : EventArgs
    {
        public bool Handled { get; set; }

        public HandledEventArgs()
        {
            Handled = false;
        }
    }
}