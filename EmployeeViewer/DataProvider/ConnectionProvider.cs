using System.Data.SqlClient;

namespace EmployeeViewer.DataProvider
{
    static class ConnectionProvider
    {
        internal static string GetConnectionSting()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                IntegratedSecurity = false,
                InitialCatalog = "employeesDb",
                DataSource = @"(localdb)\MSSQLLocalDb",
                MultipleActiveResultSets = true,
                ConnectTimeout = 3,
                UserID = "sql-test-connection",
                Password = "sqlTest1234"
            };
            return builder.ConnectionString;
        }
    }
}
