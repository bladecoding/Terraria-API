using System;
using System.Windows.Forms;

namespace TeleportPlugin
{
    public partial class TeleportForm : Form
    {
        private TeleportHelper helper;

        public TeleportForm(TeleportHelper teleportHelper)
        {
            helper = teleportHelper;
            InitializeComponent();

            UpdateAll();
        }

        public void UpdateAll()
        {
            UpdateInfoText();
            UpdateLocationList();
            UpdatePlayerList();
        }

        public void UpdateInfoText()
        {
            if (helper.ShowInfoText)
            {
                btnShowInfo.Text = "Hide position/depth/players";
            }
            else
            {
                btnShowInfo.Text = "Show position/depth/players";
            }
        }

        public void UpdateLocationList()
        {
            lvLocations.Items.Clear();

            foreach (TeleportLocation location in helper.GetCurrentWorldLocations())
            {
                ListViewItem lvi = new ListViewItem(location.Name);
                lvi.Tag = location;
                lvLocations.Items.Add(lvi);
            }
        }

        public void UpdatePlayerList()
        {
            lvPlayers.Items.Clear();

            foreach (string playerName in helper.GetPlayerList())
            {
                lvPlayers.Items.Add(playerName);
            }
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            helper.ShowInfoText = !helper.ShowInfoText;
            UpdateInfoText();
        }

        private void btnTeleportHome_Click(object sender, EventArgs e)
        {
            helper.TeleportToHome();
        }

        private void btnTeleportLocation_Click(object sender, EventArgs e)
        {
            if (lvLocations.SelectedItems.Count > 0)
            {
                TeleportLocation location = lvLocations.SelectedItems[0].Tag as TeleportLocation;
                helper.TeleportToLocation(location);
            }
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            using (TeleportLocationForm locationForm = new TeleportLocationForm())
            {
                if (locationForm.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(locationForm.LocationName))
                {
                    helper.AddCurrentLocation(locationForm.LocationName);
                    UpdateLocationList();
                }
            }
        }

        private void btnRemoveLocation_Click(object sender, EventArgs e)
        {
            if (lvLocations.SelectedItems.Count > 0)
            {
                TeleportLocation location = lvLocations.SelectedItems[0].Tag as TeleportLocation;
                lvLocations.Items.Remove(lvLocations.SelectedItems[0]);
                helper.Locations.Remove(location);
            }
        }

        private void btnTeleportPlayer_Click(object sender, EventArgs e)
        {
            if (lvPlayers.SelectedItems.Count > 0)
            {
                string name = lvPlayers.SelectedItems[0].Text;
                helper.TeleportToPlayer(name);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdatePlayerList();
        }
    }
}