using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Jc.Scripts.Database
{
    public static class SqlServer
    {
        public enum ExecuteType
        {
            NonQuery,
            Scalar,
            DataAdapter,
            DataReader
        }

        public static SqlConnection GetSqlConnection(string server, string database, string userId, string password)
        {
            return GetSqlConnection(String.Format("Server={0};Database={1};User Id={2};Password={3};", server, database, userId, password));
        }

        public static SqlConnection GetSqlConnection(string server, string database)
        {
            return GetSqlConnection(String.Format("Server={0};Database={1};Trusted_Connection=True;", server, database));
        }

        public static SqlConnection GetSqlConnection(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            if (sqlConnection.State != ConnectionState.Open) { sqlConnection.Open(); }            
            return sqlConnection;
        }

        public static SqlCommand GetSqlCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);
            if (sqlTransaction != null) sqlCommand.Transaction = sqlTransaction;
            sqlCommand.CommandType = commandType;
            if (parameters != null && parameters.Length > 0) sqlCommand.Parameters.AddRange(parameters);
            return sqlCommand;
        }

        public static SqlCommand GetSqlCommand(SqlConnection sqlConnection, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            return GetSqlCommand(sqlConnection, null, commandType, commandText, parameters);
        }
        
        public static SqlDataReader GetSqlDataReader(SqlCommand sqlCommand)
        {
            return sqlCommand.ExecuteReader();
        }

        public static SqlDataAdapter GetSqlDataAdapter(SqlCommand sqlCommand, SqlConnection sqlConnection)
        {
            return GetSqlDataAdapter(sqlCommand, sqlConnection, null);
        }

        public static SqlDataAdapter GetSqlDataAdapter(SqlCommand sqlCommand, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlDataAdapter sqlDataAdapter;
            SqlCommandBuilder sqlCommandBuilder;

            sqlCommand.Connection = sqlConnection;
            if (sqlTransaction != null) { sqlCommand.Transaction = sqlTransaction; }
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
            try
            {
                sqlDataAdapter.InsertCommand = sqlCommandBuilder.GetInsertCommand();
                sqlDataAdapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
                sqlDataAdapter.DeleteCommand = sqlCommandBuilder.GetDeleteCommand();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return sqlDataAdapter;
        }

        private static object ExecuteCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlCommand sqlCommand, ExecuteType executeType)
        {
            object executeSql = null;

            sqlCommand.CommandTimeout = 0;
            sqlCommand.Connection = sqlConnection;
            if (sqlConnection.State != ConnectionState.Open) { sqlConnection.Open(); }
            if (sqlTransaction != null) { sqlCommand.Transaction = sqlTransaction; }

            switch (executeType)
            {
                case ExecuteType.NonQuery:
                    executeSql = sqlCommand.ExecuteNonQuery();
                    break;
                case ExecuteType.Scalar:
                    executeSql = sqlCommand.ExecuteScalar();
                    break;
                case ExecuteType.DataAdapter:
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        DataSet dataSet = new DataSet();
                        sqlAdapter.Fill(dataSet);
                        executeSql = dataSet;
                    }
                    break;
                case ExecuteType.DataReader:
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(sqlDataReader);
                        executeSql = dataTable;
                    }
                    break;
            }

            return executeSql;
        }

        public static int ExecuteNonQuery(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlCommand sqlCommand)
        {
            return (int)ExecuteCommand(sqlConnection, sqlTransaction, sqlCommand, ExecuteType.NonQuery);
        }

        public static int ExecuteNonQuery(SqlConnection sqlConnection, SqlCommand sqlCommand)
        {
            return (int)ExecuteCommand(sqlConnection, null, sqlCommand, ExecuteType.NonQuery);
        }

        public static object ExecuteScalar(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlCommand sqlCommand)
        {
            return (object)ExecuteCommand(sqlConnection, sqlTransaction, sqlCommand, ExecuteType.Scalar);
        }

        public static object ExecuteScalar(SqlConnection sqlConnection, SqlCommand sqlCommand)
        {
            return (object)ExecuteCommand(sqlConnection, null, sqlCommand, ExecuteType.Scalar);
        }

        public static DataSet ExecuteDataAdapter(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlCommand sqlCommand)
        {
            return (DataSet)ExecuteCommand(sqlConnection, sqlTransaction, sqlCommand, ExecuteType.DataAdapter);
        }

        public static DataSet ExecuteDataAdapter(SqlConnection sqlConnection, SqlCommand sqlCommand)
        {
            return (DataSet)ExecuteCommand(sqlConnection, null, sqlCommand, ExecuteType.DataAdapter);
        }

        public static DataTable ExecuteDataReader(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlCommand sqlCommand)
        {
            return (DataTable)ExecuteCommand(sqlConnection, sqlTransaction, sqlCommand, ExecuteType.DataReader);
        }

        public static DataTable ExecuteDataReader(SqlConnection sqlConnection, SqlCommand sqlCommand)
        {
            return (DataTable)ExecuteCommand(sqlConnection, null, sqlCommand, ExecuteType.DataReader);
        }

        public static IEnumerable<IDataRecord> IterateDataReader(SqlConnection sqlConnection, SqlCommand sqlCommand)
        {
            return IterateDataReader(sqlConnection, null, sqlCommand);
        }

        public static IEnumerable<IDataRecord> IterateDataReader(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlCommand sqlCommand)
        {
            sqlCommand.Connection = sqlConnection;
            if (sqlConnection.State != ConnectionState.Open) { sqlConnection.Open(); }
            if (sqlTransaction != null) { sqlCommand.Transaction = sqlTransaction; }

            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    yield return sqlReader;
                }
            }
        }

        public static SqlTransaction BeginTransaction(SqlConnection sqlConnection)
        {
            return BeginTransaction(sqlConnection, IsolationLevel.ReadCommitted);
        }

        public static SqlTransaction BeginTransaction(SqlConnection sqlConnection, IsolationLevel isolationLevel)
        {
            if (sqlConnection.State != ConnectionState.Open) { sqlConnection.Open(); }
            return sqlConnection.BeginTransaction(isolationLevel);
        }

        public static void CommitTransaction(SqlTransaction sqlTransaction)
        {
            sqlTransaction.Commit();
        }

        public static void RollbackTransaction(SqlTransaction sqlTransaction)
        {
            sqlTransaction.Rollback();
        }
    }
}
