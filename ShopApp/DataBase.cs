using System.Data.SqlClient;

namespace ShopApp
{
    internal static class DataBase
    {
        internal static int ExecuteNonQuery(string cmdText, string connectionStringName)
        {
            using (var connection = new SqlConnection(connectionStringName))
            {
                connection.Open();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        internal static object ExecuteScalar(string cmdText, string connectionStringName)
        {
            using (var connection = new SqlConnection(connectionStringName))
            {
                connection.Open();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    return command.ExecuteScalar();
                }
            }
        }
    }
}