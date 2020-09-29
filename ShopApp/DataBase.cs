using System.Data;
using System.Data.SqlClient;

namespace ShopApp
{
    internal enum SqlOperation { ExecuteNonQuery, ExecuteScalar, ExecuteDataTable }

    internal static class DataBase
    {
        internal static object Execute(string cmdText, string connectionStringName, SqlOperation sqlOperation)
        {
            using (var connection = new SqlConnection(connectionStringName))
            {
                connection.Open();

                using (var command = new SqlCommand(cmdText, connection))
                {
                    switch (sqlOperation)
                    {
                        case SqlOperation.ExecuteNonQuery:
                            return command.ExecuteNonQuery();
                        case SqlOperation.ExecuteScalar:
                            return command.ExecuteScalar();
                        case SqlOperation.ExecuteDataTable:
                            using (var sqlDataAdapter = new SqlDataAdapter(cmdText, connection))
                            {
                                var result = new DataSet();
                                sqlDataAdapter.Fill(result);
                                return result.Tables[0];
                            }
                        default:
                            return new object();
                    }
                }
            }
        }

        //internal static int ExecuteNonQuery(string cmdText, string connectionStringName)
        //{
        //    using (var connection = new SqlConnection(connectionStringName))
        //    {
        //        connection.Open();

        //        using (var command = new SqlCommand(cmdText, connection))
        //        {
        //            return command.ExecuteNonQuery();
        //        }
        //    }
        //}

        //internal static object ExecuteScalar(string cmdText, string connectionStringName)
        //{
        //    using (var connection = new SqlConnection(connectionStringName))
        //    {
        //        connection.Open();

        //        using (var command = new SqlCommand(cmdText, connection))
        //        {
        //            return command.ExecuteScalar();
        //        }
        //    }
        //}

        //internal static DataTable ExecuteDataTable(string cmdText, string connectionStringName)
        //{
        //    using (var connection = new SqlConnection(connectionStringName))
        //    {
        //        connection.Open();

        //        using (var sqlDataAdapter = new SqlDataAdapter(cmdText, connection))
        //        {
        //            var result = new DataSet();
        //            sqlDataAdapter.Fill(result);
        //            return result.Tables[0];
        //        }
        //    }
        //}
    }
}