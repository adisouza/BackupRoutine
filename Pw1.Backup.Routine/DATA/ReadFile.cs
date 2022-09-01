using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pw1.Backup.Routine.DATA{
    public class ReadFile{
        static string path = AppDomain.CurrentDomain.BaseDirectory + @"\stringSQL.txt";
        public static void LogServices(string parametros)
        {
            try {
                using(StreamWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\LogService.txt")) {
                   // var lines = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\LogService.txt");
                    file.WriteLine(parametros);
                }
            } catch(Exception ex) {
                if(!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\erro.txt")){
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"\erro.txt");
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\erro.txt",$"{ex.Message} === {ex.StackTrace}");
            }
        }
        public static string[] PullFile() {
            using(StreamReader sr = new StreamReader(path)) {
                var file = sr.ReadToEnd();
                string[] parametros = file.Split(';');
                return parametros;
             }
        }
         void SaveFile(string parametros) {
            File.WriteAllText(path,parametros);
        }
        public void CatchParameters(string[]Fields) { 
            var newString =  $"{Fields[0]} ;{Fields[1]};{Fields[2]};{Fields[3]};{Fields[4]};{Fields[5]};{Fields[6]};{Fields[7]}";
            SaveFile(newString);
        }

        public static string StringConnectionSQL() {
            var stringConnection = PullFile();
            return "Data Source=" + stringConnection[0] + ";Initial Catalog=" + stringConnection[1] + ";User Id=" + stringConnection[2] + ";Password=" + stringConnection[3]+";";
        }

    }
}
