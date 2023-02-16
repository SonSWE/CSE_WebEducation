using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SQLHelper
    {
        #region Execute As Dataset
        //Execute to dataset with connection string with null parameter
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        // Excecute to dataset with connection string and have parameters
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            if ((connectionString == null || connectionString.Length == 0)) throw new ArgumentNullException("connectionString");

            // Create and connect to database
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                //Call the overload with connection place of the connection string     
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
            finally
            {
                //dispose connection
                if ((connection != null)) connection.Dispose();
            }
        }

        //Execute to dataset with connection with null parameter
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
        }

        // Excecute to dataset with connection
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if ((connection == null)) throw new ArgumentNullException("connection");
            // create command and prepare for execute
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdatpter = new SqlDataAdapter();
            bool mustCloseConnection = false;

            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, ref mustCloseConnection);

            try
            {
                // Create the DataAdapter & DataSet
                dataAdatpter.SelectCommand = cmd;

                // Fill the DataSet using default values for DataTable names, etc
                dataAdatpter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //dispose connection
                if (((dataAdatpter != null))) dataAdatpter.Dispose();
            }
            if ((mustCloseConnection)) connection.Close();

            // Return the dataset
            return ds;
        }

        // Excecute to dataset with transaction null parameters
        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, (SqlParameter[])null);
        }

        // Excecute to dataset with transaction have paramaters
        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if ((transaction == null)) throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null)) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter dataAdatpter = new SqlDataAdapter();
            bool mustCloseConnection = false;

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);

            try
            {
                // Create the DataAdapter & DataSet
                dataAdatpter.SelectCommand = cmd;

                // Fill the DataSet using default values for DataTable names, etc
                dataAdatpter.Fill(ds);
            }
            finally
            {
                // Dispose connection
                if (((dataAdatpter != null))) dataAdatpter.Dispose();
            }

            return ds;
        }

        #endregion

        #region Execute non data
        public static int Commit(string connectionString)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, "Commit", (SqlParameter[])null);
        }

        public static int Rollback(string connectionString)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, "RollBack", (SqlParameter[])null);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
        }
        // ExecuteNonQuery
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null || connectionString.Length == 0)) throw new ArgumentNullException("connectionString");
            // Create & open a OracleConnection, and dispose of it after we are done
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
            finally
            {
                if ((connection != null)) connection.Dispose();
            }
        }
        // ExecuteNonQuery
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, (SqlParameter[])null);
        }
        // ExecuteNonQuery
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            if ((connection == null)) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();

            int retval = 0;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, ref mustCloseConnection);

            // Finally, execute the command
            retval = cmd.ExecuteNonQuery();

            if ((mustCloseConnection)) connection.Close();

            return retval;
        }
        // ExecuteNonQuery
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of OracleParameters
            return ExecuteNonQuery(transaction, commandType, commandText, (SqlParameter[])null);
        }
        // ExecuteNonQuery
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {

            if ((transaction == null)) throw new ArgumentNullException("transaction");
            if ((transaction != null) && (transaction.Connection == null)) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            int retval = 0;
            bool mustCloseConnection = false;

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);

            // Finally, execute the command
            retval = cmd.ExecuteNonQuery();

            return retval;
        }
        #endregion

        #region ExcuteBatchNonQuery
        public static int ExcuteBatchNonQuery(string connectionString, CommandType commandType, string commandText, int numItem, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null || connectionString.Length == 0)) throw new ArgumentNullException("connectionString");
            // Create & open a OracleConnection, and dispose of it after we are done
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExcuteBatchNonQuery(connection, commandType, commandText, numItem, commandParameters);
            }
            finally
            {
                if ((connection != null)) connection.Dispose();
            }
        }

        public static int ExcuteBatchNonQuery(SqlConnection connection, CommandType commandType, string commandText, int numItem, params SqlParameter[] commandParameters)
        {

            if ((connection == null)) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();

            int retval = 0;
            bool mustCloseConnection = false;

            PrepareBatchCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, numItem, commandParameters, ref mustCloseConnection);

            // Finally, execute the command
            retval = cmd.ExecuteNonQuery();

            if ((mustCloseConnection)) connection.Close();

            return retval;
        }

        public static int ExcuteBatchNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, int numItem, params SqlParameter[] commandParameters)
        {
            try
            {
                if ((transaction == null)) throw new ArgumentNullException("transaction");
                if ((transaction != null) && (transaction.Connection == null)) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

                // Create a command and prepare it for execution
                SqlCommand cmd = new SqlCommand();

                int retval = 0;
                bool mustCloseConnection = false;

                PrepareBatchCommand(cmd, transaction.Connection, transaction, commandType, commandText, numItem, commandParameters, ref mustCloseConnection);

                // Finally, execute the command
                retval = cmd.ExecuteNonQuery();

                return retval;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        private static void PrepareBatchCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, int numItem, SqlParameter[] commandParameters, ref bool mustCloseConnection)
        {

            if ((command == null)) throw new ArgumentNullException("command");
            if ((commandText == null || commandText.Length == 0)) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                mustCloseConnection = true;
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or Oracle statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it.
            if ((transaction != null))
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            command.Transaction = transaction;

            // Set the command type
            command.CommandType = commandType;


            // Attach the command parameters if they are provided
            if ((commandParameters != null))
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }
        #endregion

        #region function support
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, ref bool mustCloseConnection)
        {

            if ((command == null)) throw new ArgumentNullException("command");
            if ((commandText == null || commandText.Length == 0)) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                mustCloseConnection = true;
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or Oracle statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it.
            if ((transaction != null))
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }

            command.Transaction = transaction;

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if ((commandParameters != null))
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            try
            {
                if ((command == null)) throw new ArgumentNullException("command is null");
                if (((commandParameters != null)))
                {
                    foreach (SqlParameter p in commandParameters)
                    {
                        if (p != null)
                        {
                            // Check for derived output value with no value assigned
                            if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                            {
                                p.Value = DBNull.Value;
                            }
                            command.Parameters.Add(p);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var er = e.Message;
            }

        }
        #endregion
    }
}
