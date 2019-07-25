namespace WindowsServiceExecutor
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
            this.emailReportingServiceButton = new System.Windows.Forms.Button();
            this.allocationsUpdatorServiceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // emailReportingServiceButton
            // 
            this.emailReportingServiceButton.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.emailReportingServiceButton.Location = new System.Drawing.Point(13, 13);
            this.emailReportingServiceButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.emailReportingServiceButton.Name = "emailReportingServiceButton";
            this.emailReportingServiceButton.Size = new System.Drawing.Size(207, 55);
            this.emailReportingServiceButton.TabIndex = 0;
            this.emailReportingServiceButton.Text = "Run Email Reporting Service";
            this.emailReportingServiceButton.UseVisualStyleBackColor = false;
            this.emailReportingServiceButton.Click += new System.EventHandler(this.EmailReportingServiceButton_Click);
            // 
            // allocationsUpdatorServiceButton
            // 
            this.allocationsUpdatorServiceButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.allocationsUpdatorServiceButton.Location = new System.Drawing.Point(228, 13);
            this.allocationsUpdatorServiceButton.Margin = new System.Windows.Forms.Padding(4);
            this.allocationsUpdatorServiceButton.Name = "allocationsUpdatorServiceButton";
            this.allocationsUpdatorServiceButton.Size = new System.Drawing.Size(207, 55);
            this.allocationsUpdatorServiceButton.TabIndex = 1;
            this.allocationsUpdatorServiceButton.Text = "Run Allocations Updator Service";
            this.allocationsUpdatorServiceButton.UseVisualStyleBackColor = false;
            this.allocationsUpdatorServiceButton.Click += new System.EventHandler(this.allocationsUpdatorServiceButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 336);
            this.Controls.Add(this.allocationsUpdatorServiceButton);
            this.Controls.Add(this.emailReportingServiceButton);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Windows Services Executor - Manual";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button emailReportingServiceButton;
        private System.Windows.Forms.Button allocationsUpdatorServiceButton;
    }
}

