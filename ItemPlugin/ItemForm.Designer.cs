namespace ItemPlugin
{
    partial class ItemForm
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
            this.cbItemsList = new System.Windows.Forms.ComboBox();
            this.pgItem = new System.Windows.Forms.PropertyGrid();
            this.btnGive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbItemsList
            // 
            this.cbItemsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbItemsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemsList.FormattingEnabled = true;
            this.cbItemsList.Location = new System.Drawing.Point(8, 8);
            this.cbItemsList.Name = "cbItemsList";
            this.cbItemsList.Size = new System.Drawing.Size(312, 21);
            this.cbItemsList.TabIndex = 0;
            this.cbItemsList.SelectedIndexChanged += new System.EventHandler(this.cbItemsList_SelectedIndexChanged);
            // 
            // pgItem
            // 
            this.pgItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgItem.HelpVisible = false;
            this.pgItem.Location = new System.Drawing.Point(8, 32);
            this.pgItem.Name = "pgItem";
            this.pgItem.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pgItem.Size = new System.Drawing.Size(312, 296);
            this.pgItem.TabIndex = 2;
            this.pgItem.ToolbarVisible = false;
            // 
            // btnGive
            // 
            this.btnGive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGive.Enabled = false;
            this.btnGive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGive.Location = new System.Drawing.Point(248, 332);
            this.btnGive.Name = "btnGive";
            this.btnGive.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnGive.Size = new System.Drawing.Size(72, 33);
            this.btnGive.TabIndex = 1;
            this.btnGive.Text = "Give";
            this.btnGive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGive.UseVisualStyleBackColor = true;
            this.btnGive.Click += new System.EventHandler(this.btnGive_Click);
            // 
            // ItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 372);
            this.Controls.Add(this.btnGive);
            this.Controls.Add(this.pgItem);
            this.Controls.Add(this.cbItemsList);
            this.Name = "ItemForm";
            this.ShowIcon = false;
            this.Text = "Terraria item editor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbItemsList;
        private System.Windows.Forms.PropertyGrid pgItem;
        private System.Windows.Forms.Button btnGive;
    }
}