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
            this.TalentManagerServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.TalentManagerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // TalentManagerServiceProcessInstaller
            // 
            this.TalentManagerServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.TalentManagerServiceProcessInstaller.Password = null;
            this.TalentManagerServiceProcessInstaller.Username = null;
            // 
            // TalentManagerServiceInstaller
            // 
            this.TalentManagerServiceInstaller.DisplayName = "Agilisium Talent Manager Services";
            this.TalentManagerServiceInstaller.ServiceName = "Agilisium Talent Manager Services";
            this.TalentManagerServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.TalentManagerServiceProcessInstaller,
            this.TalentManagerServiceInstaller});

        }

        #endregion

        public System.ServiceProcess.ServiceProcessInstaller TalentManagerServiceProcessInstaller;
        public System.ServiceProcess.ServiceInstaller TalentManagerServiceInstaller;
    }
}