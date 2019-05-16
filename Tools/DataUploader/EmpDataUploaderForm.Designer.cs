namespace DataUploader
{
    partial class EmpDataUploaderForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.fileNameText = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.readFileButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.msgStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.rawTab = new System.Windows.Forms.TabPage();
            this.rawDataGrid = new System.Windows.Forms.DataGridView();
            this.extractedTab = new System.Windows.Forms.TabPage();
            this.extractedDataGrid = new System.Windows.Forms.DataGridView();
            this.uploadButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.rawTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rawDataGrid)).BeginInit();
            this.extractedTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extractedDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Excel File Path";
            // 
            // fileNameText
            // 
            this.fileNameText.Location = new System.Drawing.Point(118, 21);
            this.fileNameText.Name = "fileNameText";
            this.fileNameText.Size = new System.Drawing.Size(388, 22);
            this.fileNameText.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseButton.Location = new System.Drawing.Point(527, 19);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "&Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // readFileButton
            // 
            this.readFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.readFileButton.Location = new System.Drawing.Point(608, 19);
            this.readFileButton.Name = "readFileButton";
            this.readFileButton.Size = new System.Drawing.Size(133, 23);
            this.readFileButton.TabIndex = 4;
            this.readFileButton.Text = "&Read Excel File";
            this.readFileButton.UseVisualStyleBackColor = true;
            this.readFileButton.Click += new System.EventHandler(this.ReadFileButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msgStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 422);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(864, 30);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // msgStatusLabel
            // 
            this.msgStatusLabel.BackColor = System.Drawing.Color.SpringGreen;
            this.msgStatusLabel.Name = "msgStatusLabel";
            this.msgStatusLabel.Size = new System.Drawing.Size(849, 25);
            this.msgStatusLabel.Spring = true;
            this.msgStatusLabel.Text = "Ready";
            this.msgStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.rawTab);
            this.tabControl1.Controls.Add(this.extractedTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(843, 378);
            this.tabControl1.TabIndex = 6;
            // 
            // rawTab
            // 
            this.rawTab.Controls.Add(this.rawDataGrid);
            this.rawTab.Location = new System.Drawing.Point(4, 28);
            this.rawTab.Name = "rawTab";
            this.rawTab.Padding = new System.Windows.Forms.Padding(3);
            this.rawTab.Size = new System.Drawing.Size(835, 346);
            this.rawTab.TabIndex = 0;
            this.rawTab.Text = "Raw Excel Data";
            this.rawTab.UseVisualStyleBackColor = true;
            // 
            // rawDataGrid
            // 
            this.rawDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.rawDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rawDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rawDataGrid.Location = new System.Drawing.Point(3, 3);
            this.rawDataGrid.MultiSelect = false;
            this.rawDataGrid.Name = "rawDataGrid";
            this.rawDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.rawDataGrid.Size = new System.Drawing.Size(829, 340);
            this.rawDataGrid.TabIndex = 3;
            // 
            // extractedTab
            // 
            this.extractedTab.Controls.Add(this.extractedDataGrid);
            this.extractedTab.Location = new System.Drawing.Point(4, 28);
            this.extractedTab.Name = "extractedTab";
            this.extractedTab.Padding = new System.Windows.Forms.Padding(3);
            this.extractedTab.Size = new System.Drawing.Size(835, 346);
            this.extractedTab.TabIndex = 1;
            this.extractedTab.Text = "Extracted Data";
            this.extractedTab.UseVisualStyleBackColor = true;
            // 
            // extractedDataGrid
            // 
            this.extractedDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.extractedDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.extractedDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extractedDataGrid.Location = new System.Drawing.Point(3, 3);
            this.extractedDataGrid.MultiSelect = false;
            this.extractedDataGrid.Name = "extractedDataGrid";
            this.extractedDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.extractedDataGrid.Size = new System.Drawing.Size(829, 340);
            this.extractedDataGrid.TabIndex = 4;
            // 
            // uploadButton
            // 
            this.uploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.uploadButton.Location = new System.Drawing.Point(747, 19);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(108, 23);
            this.uploadButton.TabIndex = 8;
            this.uploadButton.Text = "&Upload Data";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // EmpDataUploaderForm
            // 
            this.AcceptButton = this.readFileButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.ClientSize = new System.Drawing.Size(864, 452);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.readFileButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileNameText);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EmpDataUploaderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Data Uploader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.rawTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rawDataGrid)).EndInit();
            this.extractedTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.extractedDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileNameText;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button readFileButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel msgStatusLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage rawTab;
        private System.Windows.Forms.DataGridView rawDataGrid;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.TabPage extractedTab;
        private System.Windows.Forms.DataGridView extractedDataGrid;
    }
}