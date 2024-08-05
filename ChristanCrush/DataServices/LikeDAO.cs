using ChristanCrush.Models;
using MySqlConnector;

namespace ChristanCrush.DataServices
{
    public class LikeDAO
    {

        public bool InsertLike(LikeModel like, MySqlConnection dbConnection)
        {
            string sqlStatement = "INSERT INTO Likes (likerid, likedid, likedat) VALUES (@LIKERID, @LIKEDID, @LIKEDAT)";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);

            cmd.Parameters.AddWithValue("@LIKERID", like.LikerId);
            cmd.Parameters.AddWithValue("@LIKEDID", like.LikedId);
            cmd.Parameters.AddWithValue("@LIKEDAT", like.LikedAt);

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

        public int InsertLikeInt(LikeModel like, MySqlConnection dbConnection)
        {
            string sqlStatement = "INSERT INTO Likes (LikerId, LikedId, LikedAt) VALUES (@LIKERID, @LIKEDID, @LIKEDAT)";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);

            cmd.Parameters.AddWithValue("@LIKERID", like.LikerId);
            cmd.Parameters.AddWithValue("@LIKEDID", like.LikedId);
            cmd.Parameters.AddWithValue("@LIKEDAT", like.LikedAt);

            try
            {
                dbConnection.Open();
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    return (int)cmd.LastInsertedId;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

        }

        public bool CheckIfMutualLikeExists(int userId1, int userId2, MySqlConnection dbConnection)
        {
            string sqlStatement = "SELECT COUNT(*) FROM Likes WHERE(LikerId = @UserId1 AND LikedId = @UserId2) OR(LikerId = @UserId2 AND LikedId = @UserId1)";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);

            cmd.Parameters.AddWithValue("@UserId1", userId1);
            cmd.Parameters.AddWithValue("@UserId2", userId2);

            try
            {
                dbConnection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 1;
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

        public bool DeleteLike(int likeId, MySqlConnection dbConnection)
        {
            string sqlStatement = "DELETE FROM likes WHERE LikeId = @LikeId";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);

            cmd.Parameters.AddWithValue("@LikeId", likeId);

            try
            {
                dbConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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

        public bool CheckIfLikeExists(int likerId, int likedId, MySqlConnection dbConnection)
        {
            string sqlStatement = @"SELECT COUNT(*) FROM Likes WHERE LikerId = @LikerId AND LikedId = @LikedId";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);

            cmd.Parameters.AddWithValue("@LikerId", likerId);
            cmd.Parameters.AddWithValue("@LikedId", likedId);

            try
            {
                dbConnection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
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
