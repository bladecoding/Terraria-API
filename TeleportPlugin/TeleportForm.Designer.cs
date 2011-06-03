namespace TeleportPlugin
{
    partial class TeleportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbPlayers = new System.Windows.Forms.GroupBox();
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.chPlayersName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnTeleportPlayer = new System.Windows.Forms.Button();
            this.gbLocations = new System.Windows.Forms.GroupBox();
            this.lvLocations = new System.Windows.Forms.ListView();
            this.chLocationsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddLocation = new System.Windows.Forms.Button();
            this.btnTeleportLocation = new System.Windows.Forms.Button();
            this.btnShowInfo = new System.Windows.Forms.Button();
            this.btnTeleportHome = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRemoveLocation = new System.Windows.Forms.Button();
            this.gbPlayers.SuspendLayout();
            this.gbLocations.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPlayers
            // 
            this.gbPlayers.Controls.Add(this.btnRefresh);
            this.gbPlayers.Controls.Add(this.lvPlayers);
            this.gbPlayers.Controls.Add(this.btnTeleportPlayer);
            this.gbPlayers.Location = new System.Drawing.Point(296, 48);
            this.gbPlayers.Name = "gbPlayers";
            this.gbPlayers.Size = new System.Drawing.Size(192, 224);
            this.gbPlayers.TabIndex = 0;
            this.gbPlayers.TabStop = false;
            this.gbPlayers.Text = "Players";
            // 
            // lvPlayers
            // 
            this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPlayersName});
            this.lvPlayers.Location = new System.Drawing.Point(8, 16);
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(176, 168);
            this.lvPlayers.TabIndex = 1;
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            this.lvPlayers.View = System.Windows.Forms.View.Details;
            // 
            // chPlayersName
            // 
            this.chPlayersName.Text = "Name";
            this.chPlayersName.Width = 172;
            // 
            // btnTeleportPlayer
            // 
            this.btnTeleportPlayer.Location = new System.Drawing.Point(8, 192);
            this.btnTeleportPlayer.Name = "btnTeleportPlayer";
            this.btnTeleportPlayer.Size = new System.Drawing.Size(88, 23);
            this.btnTeleportPlayer.TabIndex = 0;
            this.btnTeleportPlayer.Text = "Teleport";
            this.btnTeleportPlayer.UseVisualStyleBackColor = true;
            this.btnTeleportPlayer.Click += new System.EventHandler(this.btnTeleportPlayer_Click);
            // 
            // gbLocations
            // 
            this.gbLocations.Controls.Add(this.btnRemoveLocation);
            this.gbLocations.Controls.Add(this.btnAddLocation);
            this.gbLocations.Controls.Add(this.lvLocations);
            this.gbLocations.Controls.Add(this.btnTeleportLocation);
            this.gbLocations.Location = new System.Drawing.Point(8, 48);
            this.gbLocations.Name = "gbLocations";
            this.gbLocations.Size = new System.Drawing.Size(280, 224);
            this.gbLocations.TabIndex = 1;
            this.gbLocations.TabStop = false;
            this.gbLocations.Text = "Locations";
            // 
            // lvLocations
            // 
            this.lvLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLocationsName});
            this.lvLocations.Location = new System.Drawing.Point(8, 16);
            this.lvLocations.Name = "lvLocations";
            this.lvLocations.Size = new System.Drawing.Size(264, 168);
            this.lvLocations.TabIndex = 2;
            this.lvLocations.UseCompatibleStateImageBehavior = false;
            this.lvLocations.View = System.Windows.Forms.View.Details;
            // 
            // chLocationsName
            // 
            this.chLocationsName.Text = "Name";
            this.chLocationsName.Width = 260;
            // 
            // btnAddLocation
            // 
            this.btnAddLocation.Location = new System.Drawing.Point(96, 192);
            this.btnAddLocation.Name = "btnAddLocation";
            this.btnAddLocation.Size = new System.Drawing.Size(88, 23);
            this.btnAddLocation.TabIndex = 1;
            this.btnAddLocation.Text = "Add";
            this.btnAddLocation.UseVisualStyleBackColor = true;
            this.btnAddLocation.Click += new System.EventHandler(this.btnAddLocation_Click);
            // 
            // btnTeleportLocation
            // 
            this.btnTeleportLocation.Location = new System.Drawing.Point(8, 192);
            this.btnTeleportLocation.Name = "btnTeleportLocation";
            this.btnTeleportLocation.Size = new System.Drawing.Size(88, 23);
            this.btnTeleportLocation.TabIndex = 0;
            this.btnTeleportLocation.Text = "Teleport";
            this.btnTeleportLocation.UseVisualStyleBackColor = true;
            this.btnTeleportLocation.Click += new System.EventHandler(this.btnTeleportLocation_Click);
            // 
            // btnShowInfo
            // 
            this.btnShowInfo.Location = new System.Drawing.Point(8, 8);
            this.btnShowInfo.Name = "btnShowInfo";
            this.btnShowInfo.Size = new System.Drawing.Size(184, 23);
            this.btnShowInfo.TabIndex = 2;
            this.btnShowInfo.Text = "Show position/depth/players";
            this.btnShowInfo.UseVisualStyleBackColor = true;
            this.btnShowInfo.Click += new System.EventHandler(this.btnShowInfo_Click);
            // 
            // btnTeleportHome
            // 
            this.btnTeleportHome.Location = new System.Drawing.Point(200, 8);
            this.btnTeleportHome.Name = "btnTeleportHome";
            this.btnTeleportHome.Size = new System.Drawing.Size(120, 23);
            this.btnTeleportHome.TabIndex = 3;
            this.btnTeleportHome.Text = "Teleport to home";
            this.btnTeleportHome.UseVisualStyleBackColor = true;
            this.btnTeleportHome.Click += new System.EventHandler(this.btnTeleportHome_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(96, 192);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRemoveLocation
            // 
            this.btnRemoveLocation.Location = new System.Drawing.Point(184, 192);
            this.btnRemoveLocation.Name = "btnRemoveLocation";
            this.btnRemoveLocation.Size = new System.Drawing.Size(88, 23);
            this.btnRemoveLocation.TabIndex = 3;
            this.btnRemoveLocation.Text = "Remove";
            this.btnRemoveLocation.UseVisualStyleBackColor = true;
            this.btnRemoveLocation.Click += new System.EventHandler(this.btnRemoveLocation_Click);
            // 
            // TeleportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 279);
            this.Controls.Add(this.btnTeleportHome);
            this.Controls.Add(this.btnShowInfo);
            this.Controls.Add(this.gbLocations);
            this.Controls.Add(this.gbPlayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TeleportForm";
            this.Text = "Terraria teleport plugin";
            this.TopMost = true;
            this.gbPlayers.ResumeLayout(false);
            this.gbLocations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPlayers;
        private System.Windows.Forms.Button btnTeleportPlayer;
        private System.Windows.Forms.GroupBox gbLocations;
        private System.Windows.Forms.Button btnTeleportLocation;
        private System.Windows.Forms.Button btnAddLocation;
        private System.Windows.Forms.Button btnShowInfo;
        private System.Windows.Forms.Button btnTeleportHome;
        private System.Windows.Forms.ListView lvPlayers;
        private System.Windows.Forms.ListView lvLocations;
        private System.Windows.Forms.ColumnHeader chPlayersName;
        private System.Windows.Forms.ColumnHeader chLocationsName;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRemoveLocation;
    }
}