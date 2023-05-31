using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KWIJisho.Server
{
    public static class Dictionary
    {
        public static DataTable GetAllWords()
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager
                    .ConnectionStrings[Connection.GetDefaultDatabaseConnectionName()].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("uspGetDictionary", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (sqlCommand)
                {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(sqlCommand.ExecuteReader());

                    sqlConnection.Close();
                    return dataTable;
                }
            }

        }

        public static void InsertWord(string wordName)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager
                    .ConnectionStrings[Connection.GetDefaultDatabaseConnectionName()].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("uspInsertWord", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@Name", wordName);

                using (sqlCommand)
                {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
        }
    }
}
