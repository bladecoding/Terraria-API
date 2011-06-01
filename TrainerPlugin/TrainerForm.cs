using System;
using System.Windows.Forms;
using Terraria;

namespace TrainerPlugin
{
    public partial class TrainerForm : Form
    {
        public bool InfMana { get { return InfManaChk.Checked; } }
        public bool InfBreath { get { return InfBreathChk.Checked; } }
        public bool InfAmmo { get { return InfAmmoChk.Checked; } }

        public TrainerForm()
        {
            InitializeComponent();
        }

        private void TrainerForm_Shown(object sender, EventArgs e)
        {
            GrabSunChk.Checked = Main.grabSun;
            GodModeChk.Checked = Main.godMode;
            StopSpawnsChk.Checked = Main.stopSpawns;
            DumbAIChk.Checked = Main.dumbAI;
            LightTilesChk.Checked = Main.lightTiles;
            DebugChk.Checked = Main.debugMode;
        }

        private void GrabSunChk_CheckedChanged(object sender, EventArgs e)
        {
            Main.grabSun = GrabSunChk.Checked;
        }

        private void GodModeChk_CheckedChanged(object sender, EventArgs e)
        {
            Main.godMode = GodModeChk.Checked;
        }

        private void StopSpawnsChk_CheckedChanged(object sender, EventArgs e)
        {
            Main.stopSpawns = StopSpawnsChk.Checked;
        }

        private void DumbAIChk_CheckedChanged(object sender, EventArgs e)
        {
            Main.dumbAI = DumbAIChk.Checked;
        }

        private void LightTilesChk_CheckedChanged(object sender, EventArgs e)
        {
            Main.lightTiles = LightTilesChk.Checked;
        }

        private void TrainerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void TrainerForm_Load(object sender, EventArgs e)
        {
        }

        private void DebugChk_CheckedChanged(object sender, EventArgs e)
        {
            Main.debugMode = DebugChk.Checked;
        }
    }
}