using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrariaAPI
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
