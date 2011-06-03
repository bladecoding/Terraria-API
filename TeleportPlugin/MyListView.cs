using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace TeleportPlugin
{
    public class MyListView : ListView
    {
        private const int WM_PAINT = 0xF;
        private const int WM_ERASEBKGND = 0x14;

        [DefaultValue(false)]
        public bool AutoFillColumn { get; set; }

        [DefaultValue(-1)]
        public int AutoFillColumnIndex { get; set; }

        public MyListView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);

            AutoFillColumn = false;
            AutoFillColumnIndex = -1;
            FullRowSelect = true;
            View = View.Details;
        }

        public void FillColumn(int index)
        {
            if (View == View.Details && Columns.Count > 0 && index >= -1 && index < Columns.Count)
            {
                if (index == -1)
                {
                    index = Columns.Count - 1;
                }

                int width = 0;

                for (int i = 0; i < Columns.Count; i++)
                {
                    if (i != index) width += Columns[i].Width;
                }

                Columns[index].Width = ClientSize.Width - width;
            }
        }

        public void FillLastColumn()
        {
            FillColumn(-1);
        }

        [DebuggerStepThrough]
        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg == WM_PAINT && !DesignMode && AutoFillColumn)
            {
                FillColumn(AutoFillColumnIndex);
            }

            if (m.Msg != WM_ERASEBKGND)
            {
                base.OnNotifyMessage(m);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (MultiSelect && e.Control && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem lvi in Items)
                {
                    lvi.Selected = true;
                }
            }

            base.OnKeyDown(e);
        }
    }
}