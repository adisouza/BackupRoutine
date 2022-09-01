
namespace Pw1.Backup.Routine.Service
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
			this.serviceProcessPw1Backup = new System.ServiceProcess.ServiceProcessInstaller();
			this.BackupPw1 = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessPw1Backup
			// 
			this.serviceProcessPw1Backup.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessPw1Backup.Password = null;
			this.serviceProcessPw1Backup.Username = null;
			// 
			// BackupPw1
			// 
			this.BackupPw1.Description = "cria um backup da base do pw1 dentro da pasta de instalacao desse app";
			this.BackupPw1.DisplayName = "Pw1BackupRoutine";
			this.BackupPw1.ServiceName = "Pw1BackupRoutine.Service";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessPw1Backup,
            this.BackupPw1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessPw1Backup;
        private System.ServiceProcess.ServiceInstaller BackupPw1;
    }
}