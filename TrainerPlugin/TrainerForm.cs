using System;
using System.Reflection;
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

        private void TrainerForm_Shown(object sender, EventArgs e)
        {
            MoveSplitterTo(pgTrainer, 200);
        }

        private void MoveSplitterTo(PropertyGrid pg, int x)
        {
            try
            {
                FieldInfo field = typeof(PropertyGrid).GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance);
                field.FieldType.GetMethod("MoveSplitterTo", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(field.GetValue(pg), new object[] { x });
                pgTrainer.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void pgTrainer_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            settings = (TrainerSettings)pgTrainer.SelectedObject;
        }

        private void TrainerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}