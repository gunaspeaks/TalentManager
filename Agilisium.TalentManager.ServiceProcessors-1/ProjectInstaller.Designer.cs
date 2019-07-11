namespace Agilisium.TalentManager.WindowServices
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AgilisiumEmailServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.AgilisiumEmailServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // AgilisiumEmailServiceProcessInstaller
            // 
            this.AgilisiumEmailServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.AgilisiumEmailServiceProcessInstaller.Password = null;
            this.AgilisiumEmailServiceProcessInstaller.Username = null;
            // 
            // AgilisiumEmailServiceInstaller
            // 
            this.AgilisiumEmailServiceInstaller.DisplayName = "Agilisium Email Service Installer";
            this.AgilisiumEmailServiceInstaller.ServiceName = "Agilisium - Talent Manager Email Service";
            this.AgilisiumEmailServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.AgilisiumEmailServiceProcessInstaller,
            this.AgilisiumEmailServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller AgilisiumEmailServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller AgilisiumEmailServiceInstaller;
    }
}