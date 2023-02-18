using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SQLite;

namespace NETCoreWPF
{
    public partial class MainWindow
    {
        /*
        public class DBInfo
        {
            [PrimaryKey,AutoIncrement]
            public int id { get; set; }
            public string Column1 { get; set; }
            public string Column2 { get; set; }

            public DBInfo() { }

            public DBInfo(string column1, string column2)
            {
                Column1 = column1;
                Column2 = column2;
            } 
        }*/

        public static void ConnectionBS()
        {
            /*
                //SQLiteConnectionString connectionString = "";
                SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();
                //connectionStringBuilder.Password = "Menskirts#";
                connectionStringBuilder.ConnectionString = @"Data Source=C:\Irma.db";
                Console.WriteLine(connectionStringBuilder.ToString());
                SqliteConnection cnn = new SqliteConnection(connectionStringBuilder.ConnectionString);
                cnn.Open();
                MessageBox.Show("Connection open");
                cnn.Close();
                */

            /*
            FbConnectionStringBuilder fbConnectionStringBuilder = new FbConnectionStringBuilder();
            fbConnectionStringBuilder.Database = @"C:\Barakh.fdb";
            fbConnectionStringBuilder.DataSource = "localhost";
            fbConnectionStringBuilder.UserID = "SYSDBA";
            fbConnectionStringBuilder.Password = "Ordcrayt31()";
            FbConnection fbn = new FbConnection(@"Server=192.168.1.131;User=SYSDBA;Password=Ordcrayt31();Charser=NONE;Database=C:\Barakh.fdb".ToString());
            //fbn.ConnectionString = fbConnectionStringBuilder.ToString();
            //fbn.ConnectionString = @"Server=localhost;User=SYSDBA;Password=Ordcrayt31();Charser=NONE;Database=C:\Barakh.fdb";
            string s = "";
            try
            {
                fbn.Open();
                FbCommand com = new("CREATE TABLE testtable(column1 int, column2 varchar(20), PRIMARY KEY(column1));");
                com.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                s = ex.Message;
                MessageBox.Show(s);
            }
            */
            /*
            string s = "";
            SqlConnectionStringBuilder connectionStringBuilder = new(@"Server=localhost;User=SYSDBA;Password=Ordcrayt31();Database=C:\Barakh.fdb");
            SqlConnection connection = new(connectionStringBuilder.ToString());
            try
            {
                connection.Open();
                SqlCommand com = new("CREATE TABLE testtable(column1 int, column2 varchar(20), PRIMARY KEY(column1));");
                com.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex)
            {
                s = ex.Message;
                MessageBox.Show(s);
            }
            */
            //FbConnection fbn = new(fbConnectionStringBuilder.ConnectionString);
            /*
            if (File.Exists(@"C:\Databases\Ketto.db"))
            {
                //do nothing
            }
            else
            {
                var db = new SQLiteConnection(@"C:\Databases\Ketto.db");
                db.CreateTable<DBInfo>();
                db.Close();
            }

            DBInfo temp = new DBInfo("bruh", "moment");
            var dbe = new SQLiteConnection(@"C:\Databases\Ketto.db");
            dbe.Insert(temp);
            dbe.Close();
            */
        }
    }
    /*
    public class BaseDataAccess
    {
        protected string ConnectionString { get; set; }
        /*
        public BaseDataAccess()
        {
        }
        
        public BaseDataAccess(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        protected DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
            command.CommandType = commandType;
            return command;
        }

        protected SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value);
            parameterObject.Direction = ParameterDirection.Input;
            return parameterObject;
        }

        protected SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type); ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }

            return parameterObject;
        }

        protected int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to ExecuteNonQuery for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }

        protected object ExecuteScalar(string procedureName, List<SqlParameter> parameters)
        {
            object returnValue = null;

            try
            {
                using (DbConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, CommandType.StoredProcedure);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to ExecuteScalar for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }

        protected DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DbDataReader ds;

            try
            {
                DbConnection connection = this.GetConnection();
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                return ds;
            }
            catch (Exception ex)
            {
                //LogException("Failed to GetDataReader for " + procedureName, ex, parameters);
                throw;
            }


        }

    }

    public class Test
    {
        public object TestId { get; internal set; }
        public object Name { get; internal set; }
    }

    
    public class DDataAccess : BaseDataAccess
    {
        public DDataAccess(string connectionString) : base(connectionString)
        {
        }

        public List<Test> GetTests()
        {
            List<DbParameter> parameterList = new List<DbParameter>();
            List<Test> Tests = new List<Test>();
            Test TestItem = null;


            using (DbDataReader dataReader = base.ExecuteReader("Test_GetAll", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        TestItem = new Test();
                    }
                    TestItem.TestId = (int)dataReader["TestId"];
                    TestItem.Name = (string)dataReader["Name"];

                    Tests.Add(TestItem);
                }
            }
            return Tests;
        }

        parameterList.Add(TestIdParamter);
    public Test CreateTest(Test Test)
        {
            List<DbParameter> parameterList = new List<DbParameter>();

            DbParameter TestIdParamter = base.GetParameterOut("TestId", SqlDbType.Int, Test.TestId);
        }
        parameterList.Add(base.GetParameter("Name", Test.Name));
 
        base.ExecuteNonQuery("Test_Create", parameterList, CommandType.StoredProcedure);

        Test.TestId = (int) TestIdParamter.Value;
 
        return Test;
 }*/
}
