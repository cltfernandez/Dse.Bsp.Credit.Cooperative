using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Net;
using Jc.Scripts.Cryptography;
using Jc.Scripts.Database;
using Loan.Application.Infrastructure.Data;
using Loan.Application.Infrastructure.Enumerations.Sql;
using Loan.Application.Infrastructure.Enumerations.DropDownItems;

namespace Loan.Application.Infrastructure.Data
{
    public class Database
    {

        private string connectionString;
        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;
        private SqlCommand sqlCommand;

        public Database()
		{
			connectionString = GetConnectionString();
			sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand();
		}

        public SqlCommand Command
        {
            get { return sqlCommand; }
            set { sqlCommand = value; }
        }

        public static string GetConnectionString()
        {
            return GetConnectionString(false);
        }

        public static string GetConnectionString(bool isRestore)
        {
            string cipherText;
            string hostName = Dns.GetHostName().ToString().ToUpper();

            switch (hostName)
            {
                case "AS100383":
                    cipherText = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString.Test");
                    break;
                case "AMD":
                    cipherText = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString.Home");
                    break;
                default:
                    cipherText = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString.Prod" + (isRestore ? ".Restore" : ""));
                    break;
            }

            return GetDecryptedText(cipherText);
        }

        public static string GetDecryptedText(string cipherText)
        {
            string key = ConfigurationManager.AppSettings.Get("Key");
            string iv = Base64.Encrypt(key);
            return Symmetric.Decrypt(Symmetric.Algorithms.Aes, cipherText, key, iv);
        }

        public static string GetEncryptedText(string plainText)
        {
            string key = ConfigurationManager.AppSettings.Get("Key");
            string iv = Base64.Encrypt(key);
            return Symmetric.Encrypt(Symmetric.Algorithms.Aes, plainText, key, iv);
        }

        public static SqlCommand GetSqlCommand(CommandType commandType, string commandText)
        {
            return SqlServer.GetSqlCommand(SqlServer.GetSqlConnection(GetConnectionString()), commandType, commandText);
        }

        private void OpenConnection()
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.ConnectionString = connectionString;
                sqlConnection.Open();
            }
        }

        private void CloseConnection()
        {
            if (sqlConnection.State != ConnectionState.Closed) { sqlConnection.Close(); }
        }

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            try
            {
                sqlTransaction = SqlServer.BeginTransaction(sqlConnection, isolationLevel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                SqlServer.CommitTransaction(sqlTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                SqlServer.RollbackTransaction(sqlTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecuteNonQuerySp(NonQuery nonQuery, params object[] parameters)
        {
            ExecuteNonQuerySp(nonQuery.ToString(), parameters);
        }

        public void ExecuteNonQuerySp(String spName, params object[] parameters)
        {
            SetStoredProcedureParameters(spName, parameters);
            ExecuteQuery(spName, CommandType.StoredProcedure);
        }

        public DataSet ExecuteQuerySp(String spName, params object[] parameters)
        {
            SetStoredProcedureParameters(spName, parameters);
            return ExecuteQuery(spName, CommandType.StoredProcedure);
        }

        private void SetStoredProcedureParameters(String spName, params object[] parameters)
        {
            int index = 0;
            DataTable dt = GetStoreProcedureParameters(spName);
            
            foreach (object parameter in parameters)
            {
                AddParameter(dt.Rows[index][0].ToString(), parameter);
                index++;
            }
        }

        private DataTable GetStoreProcedureParameters(string spName)
        {
            const string sql = "select sc.name from sys.sysobjects so inner join sys.syscolumns sc on so.id = sc.id where so.name = '{0}' and so.type like '%P%' order by sc.colorder";
            DataTable dt = ExecuteQuery(String.Format(sql, spName), CommandType.Text).Tables[0];
            return dt;
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            sqlCommand.CommandText = commandText;
            sqlCommand.CommandType = commandType;

            try
            {
                int affected = SqlServer.ExecuteNonQuery(sqlConnection, sqlTransaction == null ? null : sqlTransaction, sqlCommand);
                sqlCommand = new SqlCommand();
                return affected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteQuery(string commandText, CommandType commandType)
        {
            sqlCommand.CommandText = commandText;
            sqlCommand.CommandType = commandType;

            try
            {
                DataSet ds = SqlServer.ExecuteDataAdapter(sqlConnection, sqlTransaction == null ? null : sqlTransaction, sqlCommand);
                sqlCommand = new SqlCommand();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddParameter(string ParameterName, object Value)
        {
            sqlCommand.Parameters.AddWithValue(ParameterName, Value);
        }

        public static DataTable GetDetails(Query type, params object[] parameters)
        {
            int count = 0;
            DataSet ds;
            
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandText = "s3p_Common_GetDetails";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@QueryType", type.ToString());

                    foreach (object param in parameters)
                    {
                        if (param.GetType() == typeof(DateTime))
                        {
                            sqlCommand.Parameters.AddWithValue("@dt1", param);
                        }
                        else
                        {
                            count++;
                            sqlCommand.Parameters.AddWithValue(string.Format("@v{0}", count), param.ToString());
                        }
                    }

                    ds = SqlServer.ExecuteDataAdapter(sqlConnection, sqlCommand);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0];
                    }
                    else
                    {
                        return new DataTable();
                    }
                }
            }
        }

        public static T GetObject<T>(params object[] parameters)
        {
            Query type = getQueryType<T>();
            return GetObject<T>(type, parameters);
        }

        public static T GetObject<T>(Query type, params object[] parameters)
        {
            DataTable dt;
            T entity = (T)Activator.CreateInstance(typeof(T));
            dt = Loan.Application.Infrastructure.Data.Database.GetDetails(type, parameters);

            if (dt.Rows.Count > 0)
            {
                SetObject<T>(entity, dt.Rows[0]);
                return entity;
            }

            return default(T);
        }

        public static List<T> GetObjectList<T>(params object[] parameters)
        {
            Query type = getQueryType<T>();
            return GetObjectList<T>(type, parameters);
        }

        public static List<T> GetObjectList<T>(Query type, params object[] parameters)
        {
            List<T> list = new List<T>();
            DataTable dt;
            dt = Loan.Application.Infrastructure.Data.Database.GetDetails(type, parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T entity = (T)Activator.CreateInstance(typeof(T));
                    SetObject<T>(entity, dr);
                    list.Add(entity);
                }
                return list;
            }

            return list;
        }

        public static List<T> GetObjectList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T entity = (T)Activator.CreateInstance(typeof(T));
                    SetObject<T>(entity, dr);
                    list.Add(entity);
                }
            }

            return list;
        }

        public static List<string> GetStringList(Query type, params object[] parameters)
        {
            List<string> list = new List<string>();
            DataTable dt;
            
            dt = Loan.Application.Infrastructure.Data.Database.GetDetails(type, parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr[0].ToString());
                }
            }

            return list;
        }

        private static void SetObject<T>(T entity, DataRow dr)
        {
            const string getEnumFromDescriptionMethod = "GetEnumFromDescription";

            PropertyInfo[] properties = entity.GetType().GetProperties().Where(p => p.CanWrite).ToArray();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.IsEnum)
                {
                    MethodInfo method = typeof(Parser).GetMethod(getEnumFromDescriptionMethod);
                    MethodInfo generic = method.MakeGenericMethod(property.PropertyType);
                    property.SetValue(entity, generic.Invoke(null, new object[] { dr[property.Name].ToString() }), null);
                }
                else
                {
                    property.SetValue(entity, dr[property.Name] == DBNull.Value ? null : dr[property.Name], null);
                }
            }
        }

        private static void SetDataRow<T>(T entity, DataRow dr)
        {
            Object value;
            PropertyInfo[] properties = entity.GetType().GetProperties().Where(p => p.CanWrite).ToArray();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.IsEnum)
                {
                    value = Parser.GetDescriptionFromEnum(property.GetValue(entity, null));
                    dr[property.Name] = value.ToString() == String.Empty ? DBNull.Value : value;
                }
                else
                {
                    value = property.GetValue(entity, null);
                    if (property.PropertyType == typeof(DateTime)) 
                    {
                        dr[property.Name] = ((DateTime)value) == DateTime.MinValue ? DBNull.Value : value;
                    }
                    else
                    {
                        dr[property.Name] = value;
                    }
                }
            }
        }

        public bool InsertRecord<T>(T entity)
        {
            Query type = getQueryType<T>();
            return InsertRecord<T>(type, entity);
        }

        public bool InsertRecord<T>(Enumerations.Sql.Query type, T entity)
        {
            const string expression = "7 != 7";
            return ExecuteAdapter<T>(sqlTransaction, type, entity, expression, 0);
        }

        public bool UpdateRecord<T>(T entity, string expression)
        {
            Query type = getQueryType<T>();
            return ExecuteAdapter<T>(sqlTransaction, type, entity, expression, 1);
        }

        public bool UpdateRecord<T>(Enumerations.Sql.Query type, T entity, string expression)
        {
            return ExecuteAdapter<T>(sqlTransaction, type, entity, expression, 1);
        }

        private bool ExecuteAdapter<T>(SqlTransaction sqlTransaction, Enumerations.Sql.Query type, T entity, string expression, int minRow)
        {
            DataRow dr;
            string table = String.Format("[{0}]", Data.Parser.GetDescriptionFromEnum(type));
            if (table.Length == 0) return false;

            sqlCommand.CommandText = string.Format("select * from {0} where {1}", table, expression);
            sqlCommand.CommandType = CommandType.Text;
            if (sqlTransaction != null) { sqlCommand.Transaction = sqlTransaction; }            
            using (SqlDataAdapter da = SqlServer.GetSqlDataAdapter(sqlCommand, sqlConnection, sqlTransaction))
            {
                using (DataSet ds = new DataSet())
                {
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count == minRow)
                    {
                        if (minRow == 0)
                        {
                            dr = ds.Tables[0].NewRow();
                            SetDataRow<T>(entity, dr);
                            ds.Tables[0].Rows.Add(dr);
                            da.Update(ds);
                        }
                        else
                        {
                            dr = ds.Tables[0].Rows[0];
                            SetDataRow<T>(entity, dr);
                            da.Update(ds);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static Query getQueryType<T>()
        {
            foreach (Query type in Enum.GetValues(typeof(Query)))
            {
                if (typeof(T).Name == type.ToString())
                {
                    return type;
                }
            }
            return new Query();
        }
    
    }
}
