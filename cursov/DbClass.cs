using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursov
{
    internal class DbClass
    {
        public static class Database
        {
            private static readonly string connectionString =
                "Server=localhost;Database=sport_club;Uid=root;Pwd=jokit007;";

            private static MySqlConnection connection;

            public static MySqlConnection GetConnection()
            {
                if (connection == null)
                    connection = new MySqlConnection(connectionString);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                return connection;
            }

            public static MySqlCommand GetCommand(string sql)
            {
                return new MySqlCommand(sql, GetConnection());
            }
        }
    }
}
