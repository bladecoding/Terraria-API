using System.Windows.Forms;

namespace TrainerPlugin
{
    public partial class TrainerForm : Form
    {
        private TrainerSettings settings;

        public TrainerForm(TrainerSettings trainerSettings)
        {
            InitializeComponent();
            settings = trainerSettings;
            pgTrainer.SelectedObject = settings;
        }

        private void TrainerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }

        private void pgTrainer_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            settings = (TrainerSettings)pgTrainer.SelectedObject;
        }
    }
}