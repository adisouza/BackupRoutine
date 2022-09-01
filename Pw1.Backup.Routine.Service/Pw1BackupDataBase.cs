using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Runtime.InteropServices;
using Pw1.Backup.Routine.DATA;
namespace Pw1.Backup.Routine.Service {
	public partial class Pw1BackupDataBase: ServiceBase {
		static int result = 0;
		public Pw1BackupDataBase() {
			InitializeComponent();
			eventLog1 = new EventLog();

			if(!System.Diagnostics.EventLog.SourceExists("Pw1BackupSource")) {
				System.Diagnostics.EventLog.CreateEventSource(
					"Pw1BackupSource", "Pw1BackupLog");
			}
			eventLog1.Source = "Pw1BackupSource";
			eventLog1.Log = "Pw1BackupLog";
		}

		protected override void OnStart(string[] args) {

			Timer timer = new Timer();
			timer.Interval = 60000; // 60 seconds
			timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
			timer.Start();
		}

		private void OnTimer(object sender, ElapsedEventArgs e) {
			try {
				ReadFile.LogServices("ultimaLeitura - " + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString());
				ServiceStatus serviceStatus = new ServiceStatus();
				serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
				serviceStatus.dwWaitHint = 100000;
				SetServiceStatus(this.ServiceHandle, ref serviceStatus);
				string[] tmp = Pw1.Backup.Routine.DATA.ReadFile.PullFile();
				var dtnow = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
				bool overwrite = false;
				if(tmp.Contains("Overwrite")) { 
					overwrite = true; 
				}
				if((Convert.ToDateTime(tmp[4]) == Convert.ToDateTime(dtnow))) {
					ReadFile.LogServices("hora de executar - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
					if(!string.IsNullOrEmpty(tmp[5].ToString()) || !string.IsNullOrWhiteSpace(tmp[5].ToString()) || tmp[5].ToString() != string.Empty || tmp[5].ToString() != "") {
						if(int.Parse(tmp[5]) == DateTime.Now.Day) {
							result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup(overwrite);
							ReadFile.LogServices("rotina Mensal - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
						}
					} else {
						string[] dias = tmp[7].Split(' ');
						if(dias.Contains("SEG")) {
							if(DateTime.Now.DayOfWeek == DayOfWeek.Monday) {
								result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup(overwrite);
								ReadFile.LogServices("rotina Semanal - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
							}
						}
						if(dias.Contains("TER")) {
							if(DateTime.Now.DayOfWeek == DayOfWeek.Tuesday) {
								result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup(overwrite);
								ReadFile.LogServices("rotina Semanal - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
							}
						}
						if(dias.Contains("QUA")) {
							if(DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) {
								result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup(overwrite);
								ReadFile.LogServices("rotina Semanal - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
							}
						}
						if(dias.Contains("QUI")) {
							if(DateTime.Now.DayOfWeek == DayOfWeek.Thursday) {
								result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup(overwrite);
								ReadFile.LogServices("rotina Semanal - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
							}
						}
						if(dias.Contains("SEX")) {
							if(DateTime.Now.DayOfWeek == DayOfWeek.Friday) {
								result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup(overwrite);
								ReadFile.LogServices("rotina Semanal - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
							}
						}
						if(dias.Contains("SAB")) {
							if(DateTime.Now.DayOfWeek == DayOfWeek.Saturday) {
								result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup(overwrite);
								ReadFile.LogServices("rotina Semanal - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
							}
						}
						if(dias.Contains("DOM")) {
							if(DateTime.Now.DayOfWeek == DayOfWeek.Sunday) {
								result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup(overwrite);
								ReadFile.LogServices("rotina Semanal - " + DateTime.Now.ToLongDateString() + " terminou as " + DateTime.Now.ToLongTimeString());
							}
						}
					}
					serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
					SetServiceStatus(this.ServiceHandle, ref serviceStatus);
				} else {
					result = 0;
				}
			} catch(Exception ex) {
				ReadFile.LogServices($"Erro no Temporizador -{ex.Message} === {ex.StackTrace}");
			}
		}

		protected override void OnStop() {
			// Update the service state to Stop Pending.
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
			serviceStatus.dwWaitHint = 100000;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);

			// Update the service state to Stopped.
			serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);
		}
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
		public enum ServiceState {
			SERVICE_STOPPED = 0x00000001,
			SERVICE_START_PENDING = 0x00000002,
			SERVICE_STOP_PENDING = 0x00000003,
			SERVICE_RUNNING = 0x00000004,
			SERVICE_CONTINUE_PENDING = 0x00000005,
			SERVICE_PAUSE_PENDING = 0x00000006,
			SERVICE_PAUSED = 0x00000007,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ServiceStatus {
			public int dwServiceType;
			public ServiceState dwCurrentState;
			public int dwControlsAccepted;
			public int dwWin32ExitCode;
			public int dwServiceSpecificExitCode;
			public int dwCheckPoint;
			public int dwWaitHint;
		};
	}
}
