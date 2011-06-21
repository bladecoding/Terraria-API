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
            this.pgTrainer = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pgTrainer
            // 
            this.pgTrainer.CommandsActiveLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.pgTrainer.CommandsDisabledLinkColor = System.Drawing.SystemColors.ControlDark;
            this.pgTrainer.CommandsLinkColor = System.Drawing.SystemColors.ActiveCaption;
            this.pgTrainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgTrainer.Location = new System.Drawing.Point(0, 0);
            this.pgTrainer.Name = "pgTrainer";
            this.pgTrainer.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgTrainer.Size = new System.Drawing.Size(308, 468);
            this.pgTrainer.TabIndex = 0;
            this.pgTrainer.ToolbarVisible = false;
            this.pgTrainer.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgTrainer_PropertyValueChanged);
            // 
            // TrainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 468);
            this.Controls.Add(this.pgTrainer);
            this.MaximizeBox = false;
            this.Name = "TrainerForm";
            this.ShowIcon = false;
            this.Text = "Terraria Trainer";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrainerForm_FormClosing);
            this.Shown += new System.EventHandler(this.TrainerForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgTrainer;

    }
}