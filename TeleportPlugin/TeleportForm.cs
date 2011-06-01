using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Terraria;

namespace TeleportPlugin
{
    public partial class TeleportForm : Form
    {
        private const string SAVE_FOLDER = "Plugins/Teleport";
        private const string SAVE_FILE_PATH = SAVE_FOLDER + "/Locations.txt";

        public TeleportForm()
        {
            InitializeComponent();
        }

        private void TeleportForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(SAVE_FOLDER))
                Directory.CreateDirectory(SAVE_FOLDER);

            if (!File.Exists(SAVE_FILE_PATH))
                return;

            string[] saveFile = File.ReadAllLines(SAVE_FILE_PATH);

            Match locationMatch;
            bool errorOccured = false;

            foreach (string s in saveFile)
            {
                locationMatch = Regex.Match(s, @"(\w+):\s(\d+\.?\d+)\s(\d+\.?\d+)");
                if (locationMatch.Success)
                    lbLocations.Items.Add(new TeleportLocation(locationMatch.Groups[1].Value,
                                                            new Vector2(float.Parse(locationMatch.Groups[2].Value),
                                                                        float.Parse(locationMatch.Groups[3].Value))));
                else
                {
                    errorOccured = true;
                }
            }

            if (errorOccured)
                MessageBox.Show("Error loading saved locations, some previous saved locations will not be available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TeleportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;

            StringBuilder sb = new StringBuilder();

            foreach (var item in lbLocations.Items)
            {
                var castItem = (TeleportLocation)item;
                sb.Append(castItem.Name);
                sb.Append(": ");
                sb.Append(castItem.Position.X);
                sb.Append(" ");
                sb.Append(castItem.Position.Y);
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(SAVE_FILE_PATH, sb.ToString());
        }

        private void btnTeleport_Click(object sender, EventArgs e)
        {
            if (lbLocations.SelectedIndex >= 0)
            {
                TeleportLocation selectedLoc = (TeleportLocation)lbLocations.SelectedItem;
                Main.player[Main.myPlayer].position = selectedLoc.Position;
            }
        }

        private void btnSaveLoc_Click(object sender, EventArgs e)
        {
            SaveLocForm saveLocForm = new SaveLocForm();
            saveLocForm.ShowDialog();
            if (saveLocForm.TelLoc != null)
            {
                lbLocations.Items.Add(saveLocForm.TelLoc);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbLocations.SelectedIndex >= 0)
            {
                SaveLocForm saveLocForm = new SaveLocForm((TeleportLocation)lbLocations.SelectedItem);
                saveLocForm.ShowDialog();
                if (saveLocForm.TelLoc != null)
                {
                    int index = lbLocations.SelectedIndex;
                    lbLocations.Items.RemoveAt(index);
                    lbLocations.Items.Insert(index, saveLocForm.TelLoc);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbLocations.SelectedIndex >= 0)
            {
                lbLocations.Items.RemoveAt(lbLocations.SelectedIndex);
            }
        }
    }
}