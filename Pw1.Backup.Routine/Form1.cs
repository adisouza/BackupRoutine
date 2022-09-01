using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pw1.Backup.Routine.DATA;

namespace Pw1.Backup.Routine {
	public partial class Form1: Form {
		public Form1() {
			InitializeComponent();
			PreencherCampos();
		}
		void PreencherCampos() {
			string[] Fiedls = ReadFile.PullFile();
			txtInstancia.Text = Fiedls[0];
			txtNomeBase.Text = Fiedls[1];
			txtUsuario.Text = Fiedls[2];
			txtSenha.Text = Fiedls[3];
			txtHorario.Text = Fiedls[4];
			txtDiaMes.Text = Fiedls[5];
			if(Fiedls[6].Contains("Overwrite?")) {
				ckbOverwrite.Checked = true;
			}
			string[] dias = Fiedls[7].Split(' ');


			if(dias.Contains("SEG")) {
				chkSeg.Checked = true;
			}
			if(dias.Contains("TER")) {
				chkTer.Checked = true;
			}
			if(dias.Contains("QUA")) {
				chkQua.Checked = true;
			}
			if(dias.Contains("QUI")) {
				chkQui.Checked = true;
			}
			if(dias.Contains("SEX")) {
				chkSex.Checked = true;
			}
			if(dias.Contains("SAB")) {
				chkSab.Checked = true;
			}
			if(dias.Contains("DOM")) {
				chkDom.Checked = true;
			}
		}
		private void btnSalvar_Click(object sender, EventArgs e) {
			string[] Fields = new string[12];
			Fields[0] = txtInstancia.Text;
			Fields[1] = txtNomeBase.Text;
			Fields[2] = txtUsuario.Text;
			Fields[3] = txtSenha.Text;
			Fields[6] = ckbOverwrite.Text;
			if(!string.IsNullOrEmpty(txtDiaMes.Text)) {
				if(!uint.TryParse(txtDiaMes.Text, out uint diaMes) && diaMes <= 30) {
					MessageBox.Show("Favor digitar um valor entre 1 a 30");
					return;
				}
			}
				Fields[4] = txtHorario.Text;
				Fields[5] = txtDiaMes.Text;
			foreach(Control item in this.Controls) {
				if(item is CheckBox) {
					CheckBox c = item as CheckBox;
					if(c.Text != "Overwrite?") {
						if(c != null && c.Checked) {
							Fields[7] += c.Text + " ";
						}
					}
				}
			}
			new ReadFile().CatchParameters(Fields);

			//            string executar = @"/C " + @"cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
			//InstallUtil.exe /u %~dp0\Pw1.Backup.Routine.Service.exe";

			//            //Executa o comando no cmd do windows e aguarda a execução do mesmo para fechá-lo
			//            System.Diagnostics.Process.Start("CMD.exe", executar).WaitForExit();
			//            executar = @"/C " + @"cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
			//InstallUtil.exe  %~dp0\Pw1.Backup.Routine.Service.exe";
			//            System.Diagnostics.Process.Start("CMD.exe", executar).WaitForExit();

			MessageBox.Show("Dados Salvos com Sucesso");
			//  var name = new Criptografia().Criptografar("adilson");
			//MessageBox.Show(name);
			//name= new Criptografia().Descriptografar(name);
			//MessageBox.Show(name);
		}

		private void btnCriarbackupJa_Click(object sender, EventArgs e) {
			var result = new Pw1.Backup.Routine.DATA.ConnectionSQL().CreateBackup();
			MessageBox.Show("Backup criado com sucesso");
		}
	}
}
