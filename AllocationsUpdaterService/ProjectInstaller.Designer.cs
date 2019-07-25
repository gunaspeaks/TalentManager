namespace AllocationsUpdaterService
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
            this.AllocationsUpdaterServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.AllocationsUpdaterServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // AllocationsUpdaterServiceProcessInstaller
            // 
            this.AllocationsUpdaterServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.AllocationsUpdaterServiceProcessInstaller.Password = null;
            this.AllocationsUpdaterServiceProcessInstaller.Username = null;
            // 
            // AllocationsUpdaterServiceInstaller
            // 
            this.AllocationsUpdaterServiceInstaller.DelayedAutoStart = true;
            this.AllocationsUpdaterServiceInstaller.Description = "A Windows service designed by Agilisium for sending alerts and updating project a" +
    "llocations";
            this.AllocationsUpdaterServiceInstaller.DisplayName = "Agilisium - Allocations Updater Service";
            this.AllocationsUpdaterServiceInstaller.ServiceName = "Agilisium-AllocationsUpdaterService";
            this.AllocationsUpdaterServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.AllocationsUpdaterServiceInstaller,
            this.AllocationsUpdaterServiceProcessInstaller});

        }

        #endregion
        public System.ServiceProcess.ServiceInstaller AllocationsUpdaterServiceInstaller;
        public System.ServiceProcess.ServiceProcessInstaller AllocationsUpdaterServiceProcessInstaller;
    }
}