namespace TrainerPlugin
{
    partial class TrainerForm
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
            this.GrabSunChk = new System.Windows.Forms.CheckBox();
            this.GodModeChk = new System.Windows.Forms.CheckBox();
            this.StopSpawnsChk = new System.Windows.Forms.CheckBox();
            this.DumbAIChk = new System.Windows.Forms.CheckBox();
            this.LightTilesChk = new System.Windows.Forms.CheckBox();
            this.InfManaChk = new System.Windows.Forms.CheckBox();
            this.InfBreathChk = new System.Windows.Forms.CheckBox();
            this.InfAmmoChk = new System.Windows.Forms.CheckBox();
            this.DebugChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // GrabSunChk
            // 
            this.GrabSunChk.AutoSize = true;
            this.GrabSunChk.Location = new System.Drawing.Point(12, 12);
            this.GrabSunChk.Name = "GrabSunChk";
            this.GrabSunChk.Size = new System.Drawing.Size(71, 17);
            this.GrabSunChk.TabIndex = 0;
            this.GrabSunChk.Text = "Grab Sun";
            this.GrabSunChk.UseVisualStyleBackColor = true;
            this.GrabSunChk.CheckedChanged += new System.EventHandler(this.GrabSunChk_CheckedChanged);
            // 
            // GodModeChk
            // 
            this.GodModeChk.AutoSize = true;
            this.GodModeChk.Location = new System.Drawing.Point(12, 35);
            this.GodModeChk.Name = "GodModeChk";
            this.GodModeChk.Size = new System.Drawing.Size(76, 17);
            this.GodModeChk.TabIndex = 1;
            this.GodModeChk.Text = "God Mode";
            this.GodModeChk.UseVisualStyleBackColor = true;
            this.GodModeChk.CheckedChanged += new System.EventHandler(this.GodModeChk_CheckedChanged);
            // 
            // StopSpawnsChk
            // 
            this.StopSpawnsChk.AutoSize = true;
            this.StopSpawnsChk.Location = new System.Drawing.Point(12, 58);
            this.StopSpawnsChk.Name = "StopSpawnsChk";
            this.StopSpawnsChk.Size = new System.Drawing.Size(89, 17);
            this.StopSpawnsChk.TabIndex = 2;
            this.StopSpawnsChk.Text = "Stop Spawns";
            this.StopSpawnsChk.UseVisualStyleBackColor = true;
            this.StopSpawnsChk.CheckedChanged += new System.EventHandler(this.StopSpawnsChk_CheckedChanged);
            // 
            // DumbAIChk
            // 
            this.DumbAIChk.AutoSize = true;
            this.DumbAIChk.Location = new System.Drawing.Point(12, 81);
            this.DumbAIChk.Name = "DumbAIChk";
            this.DumbAIChk.Size = new System.Drawing.Size(67, 17);
            this.DumbAIChk.TabIndex = 3;
            this.DumbAIChk.Text = "Dumb AI";
            this.DumbAIChk.UseVisualStyleBackColor = true;
            this.DumbAIChk.CheckedChanged += new System.EventHandler(this.DumbAIChk_CheckedChanged);
            // 
            // LightTilesChk
            // 
            this.LightTilesChk.AutoSize = true;
            this.LightTilesChk.Location = new System.Drawing.Point(12, 104);
            this.LightTilesChk.Name = "LightTilesChk";
            this.LightTilesChk.Size = new System.Drawing.Size(74, 17);
            this.LightTilesChk.TabIndex = 4;
            this.LightTilesChk.Text = "Light Tiles";
            this.LightTilesChk.UseVisualStyleBackColor = true;
            this.LightTilesChk.CheckedChanged += new System.EventHandler(this.LightTilesChk_CheckedChanged);
            // 
            // InfManaChk
            // 
            this.InfManaChk.AutoSize = true;
            this.InfManaChk.Location = new System.Drawing.Point(103, 12);
            this.InfManaChk.Name = "InfManaChk";
            this.InfManaChk.Size = new System.Drawing.Size(87, 17);
            this.InfManaChk.TabIndex = 5;
            this.InfManaChk.Text = "Infinite Mana";
            this.InfManaChk.UseVisualStyleBackColor = true;
            // 
            // InfBreathChk
            // 
            this.InfBreathChk.AutoSize = true;
            this.InfBreathChk.Location = new System.Drawing.Point(103, 35);
            this.InfBreathChk.Name = "InfBreathChk";
            this.InfBreathChk.Size = new System.Drawing.Size(91, 17);
            this.InfBreathChk.TabIndex = 6;
            this.InfBreathChk.Text = "Infinite Breath";
            this.InfBreathChk.UseVisualStyleBackColor = true;
            // 
            // InfAmmoChk
            // 
            this.InfAmmoChk.AutoSize = true;
            this.InfAmmoChk.Location = new System.Drawing.Point(103, 58);
            this.InfAmmoChk.Name = "InfAmmoChk";
            this.InfAmmoChk.Size = new System.Drawing.Size(89, 17);
            this.InfAmmoChk.TabIndex = 7;
            this.InfAmmoChk.Text = "Infinite Ammo";
            this.InfAmmoChk.UseVisualStyleBackColor = true;
            // 
            // DebugChk
            // 
            this.DebugChk.AutoSize = true;
            this.DebugChk.Location = new System.Drawing.Point(103, 81);
            this.DebugChk.Name = "DebugChk";
            this.DebugChk.Size = new System.Drawing.Size(88, 17);
            this.DebugChk.TabIndex = 8;
            this.DebugChk.Text = "Debug Mode";
            this.DebugChk.UseVisualStyleBackColor = true;
            this.DebugChk.CheckedChanged += new System.EventHandler(this.DebugChk_CheckedChanged);
            // 
            // TrainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 131);
            this.Controls.Add(this.DebugChk);
            this.Controls.Add(this.InfAmmoChk);
            this.Controls.Add(this.InfBreathChk);
            this.Controls.Add(this.InfManaChk);
            this.Controls.Add(this.LightTilesChk);
            this.Controls.Add(this.DumbAIChk);
            this.Controls.Add(this.StopSpawnsChk);
            this.Controls.Add(this.GodModeChk);
            this.Controls.Add(this.GrabSunChk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TrainerForm";
            this.Text = "TrainerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrainerForm_FormClosing);
            this.Load += new System.EventHandler(this.TrainerForm_Load);
            this.Shown += new System.EventHandler(this.TrainerForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox GrabSunChk;
        private System.Windows.Forms.CheckBox GodModeChk;
        private System.Windows.Forms.CheckBox StopSpawnsChk;
        private System.Windows.Forms.CheckBox DumbAIChk;
        private System.Windows.Forms.CheckBox LightTilesChk;
        private System.Windows.Forms.CheckBox InfManaChk;
        private System.Windows.Forms.CheckBox InfBreathChk;
        private System.Windows.Forms.CheckBox InfAmmoChk;
        private System.Windows.Forms.CheckBox DebugChk;
    }
}