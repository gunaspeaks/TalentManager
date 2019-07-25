namespace AllocationsMessengerService
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
            this.AllocationsMessengerServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.AllocationsMessengerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // AllocationsMessengerServiceProcessInstaller
            // 
            this.AllocationsMessengerServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.AllocationsMessengerServiceProcessInstaller.Password = null;
            this.AllocationsMessengerServiceProcessInstaller.Username = null;
            // 
            // AllocationsMessengerServiceInstaller
            // 
            this.AllocationsMessengerServiceInstaller.DelayedAutoStart = true;
            this.AllocationsMessengerServiceInstaller.Description = "A Windows service designed by Agilisium for sending allocation details in email w" +
    "ith attachement and also summary";
            this.AllocationsMessengerServiceInstaller.DisplayName = "Agilisium-Allocations Messenger Service";
            this.AllocationsMessengerServiceInstaller.ServiceName = "Agilisium-AllocationsMessengerService";
            this.AllocationsMessengerServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.AllocationsMessengerServiceInstaller,
            this.AllocationsMessengerServiceProcessInstaller});

        }

        #endregion
        public System.ServiceProcess.ServiceInstaller AllocationsMessengerServiceInstaller;
        public System.ServiceProcess.ServiceProcessInstaller AllocationsMessengerServiceProcessInstaller;
    }
}