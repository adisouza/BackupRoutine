using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Pw1.Backup.Routine.DATA{
    public class ConnectionSQL{

        public int ShrinkDataBase(){
           try { 
                using (SqlConnection connection = new SqlConnection(ReadFile.StringConnectionSQL())){
                string nameDB = ReadFile.PullFile()[1];
                string sql = $@"DBCC SHRINKDATABASE ([{nameDB}])";
                SqlCommand cmd = new SqlCommand(sql,connection);
                cmd.Connection.Open();
                cmd.CommandTimeout = 600;
                return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) {
                ReadFile.LogServices($"ERROR SQL -{e}");
                throw e;
            }
        }
        public int CreateBackup(bool overwrite = false) {
            var result = 100000;
            result = ShrinkDataBase();
            string fileName = "";
            try {
                if(result == 100000) {
                    throw new Exception();
                }
                    using(SqlConnection connection = new SqlConnection(ReadFile.StringConnectionSQL())) {
                    string nameDB = ReadFile.PullFile()[1] ;
                    if(overwrite) {
                        fileName = nameDB;
                    } else {
                        fileName = $"{nameDB}_{ DateTime.Now.ToString("yyyyMMddHHmmss")}";
                    }
                        string sql = $@"BACKUP DATABASE [{nameDB}]
TO DISK = '{AppDomain.CurrentDomain.BaseDirectory}\{nameDB}.bak'
   WITH FORMAT,
      MEDIANAME = 'SQLServerBackups',
      NAME = 'Full Backup of {fileName}';
";
                        SqlCommand cmd = new SqlCommand(sql,connection);
                        cmd.Connection.Open();
                        cmd.CommandTimeout = 600;
                        return cmd.ExecuteNonQuery();
                    }
            
            } catch(Exception e) {
                ReadFile.LogServices($"ERROR SQL -{e}");
                throw e;
            }
        }
    }
}
