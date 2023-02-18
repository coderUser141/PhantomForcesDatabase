using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NETCoreWPF
{
    public class SQLiteDataAccess
    {
        public static List<Class> loadClass()
        {
            using (IDbConnection cnn = new SQLiteConnection(loadConnectionString())) {
                var output = cnn.Query<Class>("select * from ID", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void saveClass(Class cls)
        {
            using (IDbConnection cnn = new SQLiteConnection(loadConnectionString()))
            {
                cnn.Execute("insert into ID (ClassName) values (@ClassName)", cls);
                
            }

        }

        private static string loadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
