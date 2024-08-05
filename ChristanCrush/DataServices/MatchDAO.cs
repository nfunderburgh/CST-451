using ChristanCrush.Models;
using MySqlConnector;

namespace ChristanCrush.DataServices
{
    public class MatchDAO
    {
        public bool InsertMatch(MatchModel match, MySqlConnection dbConnection)
        {
            string sqlStatement = "INSERT INTO Matches (UserId1, UserId2, MatchedAt) VALUES (@UserId1, @UserId2, @MatchedAt)";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@UserId1", match.UserId1);
            cmd.Parameters.AddWithValue("@UserId2", match.UserId2);
            cmd.Parameters.AddWithValue("@MatchedAt", match.MatchedAt.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                dbConnection.Open();
                int result = cmd.ExecuteNonQuery();
                dbConnection.Close();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        public MatchModel GetMatch(int userId1, int userId2, MySqlConnection dbConnection)
        {
            MatchModel match = null;
            string sqlStatement = "SELECT * FROM Matches WHERE (UserId1 = @USERID1 AND UserId2 = @USERID2) OR (UserId1 = @USERID2 AND UserId2 = @USERID1) LIMIT 1";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@USERID1", userId1);
            cmd.Parameters.AddWithValue("@USERID2", userId2);

            try
            {
                dbConnection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        match = new MatchModel
                        {
                            MatchId = Convert.ToInt32(reader["MatchId"]),
                            UserId1 = Convert.ToInt32(reader["UserId1"]),
                            UserId2 = Convert.ToInt32(reader["UserId2"]),
                            MatchedAt = Convert.ToDateTime(reader["MatchedAt"])

                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return match;
        }

        public bool DeleteMatchById(int matchId, MySqlConnection dbConnection)
        {
            string sqlStatement = @"DELETE FROM Matches WHERE MatchId = @MATCHID";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@MATCHID", matchId);

            try
            {
                dbConnection.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }
    }
}
