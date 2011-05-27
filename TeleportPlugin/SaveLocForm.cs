using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Terraria;

namespace TeleportPlugin
{
    public partial class SaveLocForm : Form
    {
        public TeleportLocation TelLoc;

        public SaveLocForm(TeleportLocation itemToEdit)
        {
            InitializeComponent();
            this.Text = "Edit item";
            nameTextBox.Text = itemToEdit.Name;
            XTextBox.Text = itemToEdit.Position.X.ToString();
            YTextBox.Text = itemToEdit.Position.Y.ToString();
        }

        public SaveLocForm()
        {
            InitializeComponent();
            XTextBox.Text = Main.player[Main.myPlayer].position.X.ToString();
            YTextBox.Text = Main.player[Main.myPlayer].position.Y.ToString();
            nameTextBox.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(XTextBox.Text) || string.IsNullOrEmpty(YTextBox.Text))
            {
                MessageBox.Show("No field can be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            float x = 0f;
            float y = 0f;

            if (!float.TryParse(XTextBox.Text, out x) || !float.TryParse(YTextBox.Text, out y))
            {
                MessageBox.Show("X and Y can only contain numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TelLoc = new TeleportLocation(nameTextBox.Text, new Vector2(x, y));
            this.Close();
        }
    }
}
