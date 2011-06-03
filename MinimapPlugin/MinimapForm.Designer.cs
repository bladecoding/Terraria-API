namespace MinimapPlugin
{
    partial class MinimapForm
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
            this.pgSettings = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pgSettings
            // 
            this.pgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSettings.Location = new System.Drawing.Point(0, 0);
            this.pgSettings.Name = "pgSettings";
            this.pgSettings.Size = new System.Drawing.Size(335, 309);
            this.pgSettings.TabIndex = 0;
            this.pgSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgSettings_PropertyValueChanged);
            // 
            // MinimapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 309);
            this.Controls.Add(this.pgSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MinimapForm";
            this.Text = "Terraria minimap settings";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgSettings;
    }
}