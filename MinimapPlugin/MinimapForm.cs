using System.Windows.Forms;

namespace MinimapPlugin
{
    public partial class MinimapForm : Form
    {
        private MinimapSettings settings;

        public MinimapForm(MinimapSettings minimapSettings)
        {
            InitializeComponent();
            settings = minimapSettings;
            pgSettings.SelectedObject = settings;
        }

        private void pgSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            settings = pgSettings.SelectedObject as MinimapSettings;
        }
    }
}