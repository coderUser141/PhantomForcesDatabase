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
using WeaponClasses;


namespace PFDB
{
    public class SQLiteDataAccess
    { 
        //TODO: needs to be fleshed out for all of the classes from WeaponClasses.cs
        //Class, Category, Weapon, Gun, Grenade, Melee, Conversions, FireModes, etc.
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
