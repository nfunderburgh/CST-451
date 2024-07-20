using ChristanCrush.Models;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.DataServices
{
    public class ProfileDAO
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";

        public bool InsertProfile(ProfileModel profile)
        {
            bool success = false;

            string sqlStatement = @"INSERT INTO profiles (USERID, BIO, IMAGE1, IMAGE2, IMAGE3, OCCUPATION, HOBBIES) VALUES (@USERID, @BIO, @IMAGE1, @IMAGE2, @IMAGE3, @OCCUPATION, @HOBBIES)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("@USERID", profile.UserId);
                    cmd.Parameters.AddWithValue("@BIO", profile.Bio);
                    cmd.Parameters.AddWithValue("@IMAGE1", profile.Image1 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IMAGE2", profile.Image2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IMAGE3", profile.Image3 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@OCCUPATION", profile.Occupation);
                    cmd.Parameters.AddWithValue("@HOBBIES", profile.Hobbies);

                    try
                    {
                        connection.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            success = true;
                        }
                        else
                        {
                            Debug.WriteLine("Error inserting profile into database!");
                            success = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        success = false;
                    }
                }
            }
            return success;
        }
    }
}
