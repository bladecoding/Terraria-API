using System;

namespace TerrariaAPI.Hooks
{
    public class AllowEventArgs : EventArgs
    {
        public bool Allow { get; set; }

        public AllowEventArgs()
        {
            Allow = true;
        }
    }

    public class HandledEventArgs : EventArgs
    {
        public bool Handled { get; set; }

        public HandledEventArgs()
        {
            Handled = false;
        }
    }
}