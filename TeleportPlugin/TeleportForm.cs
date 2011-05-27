using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Terraria;

namespace TeleportPlugin
{
    public partial class TeleportForm : Form
    {
        private const string SAVE_FOLDER = "plugins/Teleport";
        private const string SAVE_FILE_PATH = "plugins/Teleport/save.txt";

        public TeleportForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < 0)
                return;

            var selectedLoc = (TeleportLocation)listBox1.SelectedItem;
            Main.player[Main.myPlayer].position = selectedLoc.Position;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < 0)
                return;

            SaveLocForm saveLocForm = new SaveLocForm((TeleportLocation)listBox1.SelectedItem);
            saveLocForm.ShowDialog();
            if (saveLocForm.TelLoc != null)
            {
                int index = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(index);
                listBox1.Items.Insert(index, saveLocForm.TelLoc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveLocForm saveLocForm = new SaveLocForm();
            saveLocForm.ShowDialog();
            if (saveLocForm.TelLoc != null)
                listBox1.Items.Add(saveLocForm.TelLoc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < 0)
                return;

            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void TeleportForm_Load(object sender, EventArgs e)
        {
            if(!Directory.Exists(SAVE_FOLDER))
                Directory.CreateDirectory(SAVE_FOLDER);

            if(!File.Exists(SAVE_FILE_PATH))
                return;

            string[] saveFile = File.ReadAllLines(SAVE_FILE_PATH);

            Match locationMatch;
            bool errorOccured = false;
            foreach (string s in saveFile)
            {
                locationMatch = Regex.Match(s, @"(\w+):\s(\d+\.?\d+)\s(\d+\.?\d+)");
                if (locationMatch.Success)
                    listBox1.Items.Add(new TeleportLocation(locationMatch.Groups[1].Value,
                                                            new Vector2(float.Parse(locationMatch.Groups[2].Value),
                                                                        float.Parse(locationMatch.Groups[3].Value))));
                else
                {
                    errorOccured = true;
                }
            }
            if(errorOccured)
                MessageBox.Show("Error loading saved locations, some previous saved locations will not be available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TeleportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;

            if (!File.Exists(SAVE_FILE_PATH))
                File.Create(SAVE_FILE_PATH);

            var sb = new StringBuilder();

            foreach (var item in listBox1.Items)
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
    }
}
