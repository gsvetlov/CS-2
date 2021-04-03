using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EmployeeViewerWebService.DataProvider
{
    public abstract class BaseDbProvider
    {
        protected string connectionString;
        protected SqlConnection connection;

        protected BaseDbProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected virtual void Connect()
        {
            try
            {
                connection = connection ?? new SqlConnection(connectionString);
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
            }
            catch (Exception)
            {
                Disconnect();
                throw;
            }
        }
        protected virtual void Disconnect()
        {
            try
            {
                connection?.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual int Command(string command)
        {
            Connect();
            var sqlCommand = new SqlCommand(command, connection);
            try
            {
                var result = sqlCommand.ExecuteNonQuery();
                return result;
            } 
            catch (Exception)
            {
                Disconnect();
                throw;
            }
        }

        protected virtual TResult Query<TResult>(string query, Func<SqlDataReader, TResult> process)
        {
            Connect();
            var command = new SqlCommand(query, connection);
            try
            {
                using (var reader = command.ExecuteReader())
                    return process(reader);
            } catch (Exception)
            {
                Disconnect();
                throw;
            }
            
        }
    }
}