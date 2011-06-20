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
            this.btnGiveItem = new System.Windows.Forms.Button();
            this.lvItems = new System.Windows.Forms.ListView();
            this.chItems = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbSortByName = new System.Windows.Forms.CheckBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnThrowOne = new System.Windows.Forms.Button();
            this.btnGiveOne = new System.Windows.Forms.Button();
            this.nudStack = new System.Windows.Forms.NumericUpDown();
            this.lblStack = new System.Windows.Forms.Label();
            this.lblCreatesNewItem = new System.Windows.Forms.Label();
            this.btnGiveN = new System.Windows.Forms.Button();
            this.btnThrowN = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudStack)).BeginInit();
            this.SuspendLayout();
            // 
            // pgItem
            // 
            this.pgItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgItem.HelpVisible = false;
            this.pgItem.Location = new System.Drawing.Point(232, 56);
            this.pgItem.Name = "pgItem";
            this.pgItem.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pgItem.Size = new System.Drawing.Size(376, 312);
            this.pgItem.TabIndex = 2;
            this.pgItem.ToolbarVisible = false;
            // 
            // btnGiveItem
            // 
            this.btnGiveItem.Enabled = false;
            this.btnGiveItem.Location = new System.Drawing.Point(232, 5);
            this.btnGiveItem.Name = "btnGiveItem";
            this.btnGiveItem.Size = new System.Drawing.Size(112, 23);
            this.btnGiveItem.TabIndex = 1;
            this.btnGiveItem.Text = "Give this item";
            this.btnGiveItem.UseVisualStyleBackColor = true;
            this.btnGiveItem.Click += new System.EventHandler(this.btnGive_Click);
            // 
            // lvItems
            // 
            this.lvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItems});
            this.lvItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lvItems.FullRowSelect = true;
            this.lvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvItems.HideSelection = false;
            this.lvItems.Location = new System.Drawing.Point(8, 56);
            this.lvItems.MultiSelect = false;
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(216, 312);
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
            this.cbSortByName.Checked = true;
            this.cbSortByName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSortByName.Location = new System.Drawing.Point(8, 8);
            this.cbSortByName.Name = "cbSortByName";
            this.cbSortByName.Size = new System.Drawing.Size(115, 17);
            this.cbSortByName.TabIndex = 4;
            this.cbSortByName.Text = "Sort items by name";
            this.cbSortByName.UseVisualStyleBackColor = true;
            this.cbSortByName.CheckedChanged += new System.EventHandler(this.cbSortByName_CheckedChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(56, 32);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(168, 20);
            this.txtFilter.TabIndex = 5;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(8, 36);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(44, 13);
            this.lblFilter.TabIndex = 6;
            this.lblFilter.Text = "Search:";
            // 
            // btnThrowOne
            // 
            this.btnThrowOne.Enabled = false;
            this.btnThrowOne.Location = new System.Drawing.Point(552, 6);
            this.btnThrowOne.Name = "btnThrowOne";
            this.btnThrowOne.Size = new System.Drawing.Size(56, 23);
            this.btnThrowOne.TabIndex = 7;
            this.btnThrowOne.Text = "Throw 1";
            this.btnThrowOne.UseVisualStyleBackColor = true;
            this.btnThrowOne.Click += new System.EventHandler(this.btnThrowOne_Click);
            // 
            // btnGiveOne
            // 
            this.btnGiveOne.Enabled = false;
            this.btnGiveOne.Location = new System.Drawing.Point(496, 6);
            this.btnGiveOne.Name = "btnGiveOne";
            this.btnGiveOne.Size = new System.Drawing.Size(56, 23);
            this.btnGiveOne.TabIndex = 8;
            this.btnGiveOne.Text = "Give 1";
            this.btnGiveOne.UseVisualStyleBackColor = true;
            this.btnGiveOne.Click += new System.EventHandler(this.btnGiveOne_Click);
            // 
            // nudStack
            // 
            this.nudStack.Location = new System.Drawing.Point(440, 32);
            this.nudStack.Name = "nudStack";
            this.nudStack.Size = new System.Drawing.Size(48, 20);
            this.nudStack.TabIndex = 9;
            this.nudStack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudStack.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblStack
            // 
            this.lblStack.AutoSize = true;
            this.lblStack.Location = new System.Drawing.Point(400, 36);
            this.lblStack.Name = "lblStack";
            this.lblStack.Size = new System.Drawing.Size(38, 13);
            this.lblStack.TabIndex = 10;
            this.lblStack.Text = "Stack:";
            // 
            // lblCreatesNewItem
            // 
            this.lblCreatesNewItem.AutoSize = true;
            this.lblCreatesNewItem.Location = new System.Drawing.Point(400, 11);
            this.lblCreatesNewItem.Name = "lblCreatesNewItem";
            this.lblCreatesNewItem.Size = new System.Drawing.Size(88, 13);
            this.lblCreatesNewItem.TabIndex = 11;
            this.lblCreatesNewItem.Text = "Creates new item";
            // 
            // btnGiveN
            // 
            this.btnGiveN.Enabled = false;
            this.btnGiveN.Location = new System.Drawing.Point(496, 31);
            this.btnGiveN.Name = "btnGiveN";
            this.btnGiveN.Size = new System.Drawing.Size(56, 23);
            this.btnGiveN.TabIndex = 12;
            this.btnGiveN.Text = "Give *";
            this.btnGiveN.UseVisualStyleBackColor = true;
            this.btnGiveN.Click += new System.EventHandler(this.btnGiveN_Click);
            // 
            // btnThrowN
            // 
            this.btnThrowN.Enabled = false;
            this.btnThrowN.Location = new System.Drawing.Point(552, 31);
            this.btnThrowN.Name = "btnThrowN";
            this.btnThrowN.Size = new System.Drawing.Size(56, 23);
            this.btnThrowN.TabIndex = 13;
            this.btnThrowN.Text = "Throw *";
            this.btnThrowN.UseVisualStyleBackColor = true;
            this.btnThrowN.Click += new System.EventHandler(this.btnThrowN_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Enabled = false;
            this.btnRemoveItem.Location = new System.Drawing.Point(232, 31);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(112, 23);
            this.btnRemoveItem.TabIndex = 14;
            this.btnRemoveItem.Text = "Remove this item";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // ItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 376);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.nudStack);
            this.Controls.Add(this.btnThrowN);
            this.Controls.Add(this.btnGiveN);
            this.Controls.Add(this.lblCreatesNewItem);
            this.Controls.Add(this.lblStack);
            this.Controls.Add(this.btnGiveOne);
            this.Controls.Add(this.btnThrowOne);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cbSortByName);
            this.Controls.Add(this.lvItems);
            this.Controls.Add(this.btnGiveItem);
            this.Controls.Add(this.pgItem);
            this.Name = "ItemForm";
            this.ShowIcon = false;
            this.Text = "Terraria item editor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudStack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgItem;
        private System.Windows.Forms.Button btnGiveItem;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader chItems;
        private System.Windows.Forms.CheckBox cbSortByName;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Button btnThrowOne;
        private System.Windows.Forms.Button btnGiveOne;
        private System.Windows.Forms.NumericUpDown nudStack;
        private System.Windows.Forms.Label lblStack;
        private System.Windows.Forms.Label lblCreatesNewItem;
        private System.Windows.Forms.Button btnGiveN;
        private System.Windows.Forms.Button btnThrowN;
        private System.Windows.Forms.Button btnRemoveItem;
    }
}