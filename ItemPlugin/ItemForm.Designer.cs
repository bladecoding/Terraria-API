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
            this.pgItem = new System.Windows.Forms.PropertyGrid();
            this.btnGive = new System.Windows.Forms.Button();
            this.lvItems = new System.Windows.Forms.ListView();
            this.chItems = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbSortByName = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pgItem
            // 
            this.pgItem.HelpVisible = false;
            this.pgItem.Location = new System.Drawing.Point(232, 48);
            this.pgItem.Name = "pgItem";
            this.pgItem.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pgItem.Size = new System.Drawing.Size(376, 320);
            this.pgItem.TabIndex = 2;
            this.pgItem.ToolbarVisible = false;
            // 
            // btnGive
            // 
            this.btnGive.Enabled = false;
            this.btnGive.Location = new System.Drawing.Point(232, 8);
            this.btnGive.Name = "btnGive";
            this.btnGive.Size = new System.Drawing.Size(56, 32);
            this.btnGive.TabIndex = 1;
            this.btnGive.Text = "Give";
            this.btnGive.UseVisualStyleBackColor = true;
            this.btnGive.Click += new System.EventHandler(this.btnGive_Click);
            // 
            // lvItems
            // 
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItems});
            this.lvItems.FullRowSelect = true;
            this.lvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvItems.HideSelection = false;
            this.lvItems.Location = new System.Drawing.Point(8, 8);
            this.lvItems.MultiSelect = false;
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(216, 360);
            this.lvItems.TabIndex = 3;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            this.lvItems.SelectedIndexChanged += new System.EventHandler(this.lvItems_SelectedIndexChanged);
            // 
            // chItems
            // 
            this.chItems.Width = 195;
            // 
            // cbSortByName
            // 
            this.cbSortByName.AutoSize = true;
            this.cbSortByName.Location = new System.Drawing.Point(296, 16);
            this.cbSortByName.Name = "cbSortByName";
            this.cbSortByName.Size = new System.Drawing.Size(115, 17);
            this.cbSortByName.TabIndex = 4;
            this.cbSortByName.Text = "Sort items by name";
            this.cbSortByName.UseVisualStyleBackColor = true;
            this.cbSortByName.CheckedChanged += new System.EventHandler(this.cbSortByName_CheckedChanged);
            // 
            // ItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 376);
            this.Controls.Add(this.cbSortByName);
            this.Controls.Add(this.lvItems);
            this.Controls.Add(this.btnGive);
            this.Controls.Add(this.pgItem);
            this.Name = "ItemForm";
            this.ShowIcon = false;
            this.Text = "Terraria item editor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgItem;
        private System.Windows.Forms.Button btnGive;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader chItems;
        private System.Windows.Forms.CheckBox cbSortByName;
    }
}