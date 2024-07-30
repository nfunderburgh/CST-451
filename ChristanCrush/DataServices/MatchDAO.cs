using ChristanCrush.Models;
using MySqlConnector;

namespace ChristanCrush.DataServices
{
    public class MatchDAO
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";

        public bool InsertMatch(MatchModel match)
        {
            string sqlStatement = "INSERT INTO Matches (UserId1, UserId2, MatchedAt) VALUES (@UserId1, @UserId2, @MatchedAt)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@UserId1", match.UserId1);
                cmd.Parameters.AddWithValue("@UserId2", match.UserId2);
                cmd.Parameters.AddWithValue("@MatchedAt", match.MatchedAt.ToString("yyyy-MM-dd HH:mm:ss"));

                try
                {
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public List<MatchModel> GetMatches(int userId)
        {
            List<MatchModel> matches = new List<MatchModel>();
            string sqlStatement = "SELECT * FROM Matches WHERE UserId1 = @UserId OR UserId2 = @UserId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MatchModel match = new MatchModel
                            {
                                MatchId = Convert.ToInt32(reader["MatchId"]),
                                UserId1 = Convert.ToInt32(reader["UserId1"]),
                                UserId2 = Convert.ToInt32(reader["UserId2"])
                            };
                            matches.Add(match);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return matches;
        }
    }
}
