namespace DataMigrationTool
{
    partial class MainForm
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
            this.statusList = new System.Windows.Forms.ListBox();
            this.migrateDataButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusList
            // 
            this.statusList.FormattingEnabled = true;
            this.statusList.ItemHeight = 16;
            this.statusList.Location = new System.Drawing.Point(12, 21);
            this.statusList.Name = "statusList";
            this.statusList.Size = new System.Drawing.Size(592, 260);
            this.statusList.TabIndex = 0;
            // 
            // migrateDataButton
            // 
            this.migrateDataButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.migrateDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.migrateDataButton.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.migrateDataButton.Location = new System.Drawing.Point(473, 287);
            this.migrateDataButton.Name = "migrateDataButton";
            this.migrateDataButton.Size = new System.Drawing.Size(131, 31);
            this.migrateDataButton.TabIndex = 1;
            this.migrateDataButton.Text = "Migrate Data";
            this.migrateDataButton.UseVisualStyleBackColor = false;
            this.migrateDataButton.Click += new System.EventHandler(this.MigrateDataButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 338);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(628, 30);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = false;
            this.statusLabel.BackColor = System.Drawing.Color.LightGreen;
            this.statusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(582, 25);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "Ready";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 368);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.migrateDataButton);
            this.Controls.Add(this.statusList);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Migration Tool";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox statusList;
        private System.Windows.Forms.Button migrateDataButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}

