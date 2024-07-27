using ChristanCrush.Models;
using MySqlConnector;

namespace ChristanCrush.DataServices
{
    public class LikeDAO
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";

        public bool InsertLike(LikeModel like)
        {
            string sqlStatement = "INSERT INTO Likes (likerid, likedid, likedat) VALUES (@LIKERID, @LIKEDID, @LIKEDAT)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                
                cmd.Parameters.AddWithValue("@LIKERID", like.LikerId);
                cmd.Parameters.AddWithValue("@LIKEDID", like.LikedId);
                cmd.Parameters.AddWithValue("@LIKEDAT", like.LikedAt);

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
        public bool CheckIfMutualLikeExists(int userId1, int userId2)
        {
            string sqlStatement = "SELECT COUNT(*) FROM Likes WHERE LikerId = @UserId2 AND LikedId = @UserId1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);

                cmd.Parameters.AddWithValue("@UserId1", userId1);
                cmd.Parameters.AddWithValue("@UserId2", userId2);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool DeleteLike(int likeId)
        {
            string sqlStatement = "DELETE FROM likes WHERE LikeId = @LikeId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                
                cmd.Parameters.AddWithValue("@LikeId", likeId);

                try
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
