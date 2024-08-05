using MySqlConnector;

namespace ChristanCrush.DataServices
{
    public class DBConnection
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";

        public MySqlConnection DbConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            if (connection != null)
            {
                return connection;
            }
            else
            {
                return null;
            }
        }
    }
}
