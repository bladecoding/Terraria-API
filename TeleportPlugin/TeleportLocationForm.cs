using System;
using System.Windows.Forms;

namespace TeleportPlugin
{
    public partial class TeleportLocationForm : Form
    {
        public string LocationName { get; private set; }

        public TeleportLocationForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close(false);
        }

        private void Close(bool status)
        {
            DialogResult = status ? DialogResult.OK : DialogResult.Cancel;
            LocationName = txtLocationName.Text;
            Close();
        }
    }
}