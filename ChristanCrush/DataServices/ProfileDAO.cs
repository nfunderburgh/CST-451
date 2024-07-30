using ChristanCrush.Models;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.DataServices
{
    public class ProfileDAO
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";

        /// <summary>
        /// The function `InsertProfile` inserts a profile into a database table using parameterized SQL
        /// statements in C#.
        /// </summary>
        /// <param name="ProfileModel">The code snippet you provided is a method that inserts a profile
        /// into a database using ADO.NET with MySQL. The `InsertProfile` method takes a `ProfileModel`
        /// object as a parameter and inserts its properties into the `profiles` table in the
        /// database.</param>
        /// <returns>
        /// The method `InsertProfile` returns a boolean value indicating whether the insertion of the
        /// profile into the database was successful or not. If the insertion is successful, it returns
        /// `true`, otherwise it returns `false`.
        /// </returns>
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
                    cmd.Parameters.AddWithValue("@IMAGE1", profile.Image1Data);
                    cmd.Parameters.AddWithValue("@IMAGE2", profile.Image2Data);
                    cmd.Parameters.AddWithValue("@IMAGE3", profile.Image3Data);
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

        public ProfileModel GetProfileByUserId(int userId)
        {
            ProfileModel profile = null;

            string sqlStatement = @"SELECT PROFILEID, USERID, BIO, IMAGE1, IMAGE2, IMAGE3, OCCUPATION, HOBBIES 
                                    FROM profiles 
                                    WHERE USERID = @USERID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                
                cmd.Parameters.AddWithValue("@USERID", userId);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            profile = new ProfileModel
                            {
                                ProfileId = reader.GetInt32("PROFILEID"),
                                UserId = reader.GetInt32("USERID"),
                                Bio = reader.GetString("BIO"),
                                Image1Data =  (byte[])reader["IMAGE1"],
                                Image2Data = reader.IsDBNull(reader.GetOrdinal("IMAGE2")) ? new byte[0] : (byte[])reader["IMAGE2"],
                                Image3Data = reader.IsDBNull(reader.GetOrdinal("IMAGE3")) ? new byte[0] : (byte[])reader["IMAGE3"],
                                Occupation = reader.GetString("OCCUPATION"),
                                Hobbies = reader.GetString("HOBBIES")
                            };
                        }
                        else
                        {
                            Debug.WriteLine("No profile found for the given UserId.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

            return profile;
        }

        public ProfileModel GetRandomProfile(int currentUserId)
        {
            ProfileModel profile = null;

            string sqlStatement = @"SELECT PROFILEID, USERID, BIO, IMAGE1, IMAGE2, IMAGE3, OCCUPATION, HOBBIES 
                            FROM profiles 
                            WHERE USERID != @CURRENTUSERID
                            ORDER BY RAND()
                            LIMIT 1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("@CURRENTUSERID", currentUserId);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                profile = new ProfileModel
                                {
                                    ProfileId = reader.GetInt32("PROFILEID"),
                                    UserId = reader.GetInt32("USERID"),
                                    Bio = reader.GetString("BIO"),
                                    Image1Data = reader.IsDBNull(reader.GetOrdinal("IMAGE1")) ? new byte[0] : (byte[])reader["IMAGE1"],
                                    Image2Data = reader.IsDBNull(reader.GetOrdinal("IMAGE2")) ? new byte[0] : (byte[])reader["IMAGE2"],
                                    Image3Data = reader.IsDBNull(reader.GetOrdinal("IMAGE3")) ? new byte[0] : (byte[])reader["IMAGE3"],
                                    Occupation = reader.GetString("OCCUPATION"),
                                    Hobbies = reader.GetString("HOBBIES")
                                };
                            }
                            else
                            {
                                Debug.WriteLine("No random profile found.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return profile;
        }

        public ProfileModel GetProfileByProfileId(int profileId)
        {
            ProfileModel profile = null;

            string sqlStatement = @"SELECT PROFILEID, USERID, BIO, IMAGE1, IMAGE2, IMAGE3, OCCUPATION, HOBBIES 
                            FROM profiles WHERE PROFILEID = @PROFILEID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("@PROFILEID", profileId);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                profile = new ProfileModel
                                {
                                    ProfileId = reader.GetInt32("PROFILEID"),
                                    UserId = reader.GetInt32("USERID"),
                                    Bio = reader.GetString("BIO"),
                                    Image1Data = (byte[])reader["IMAGE1"],
                                    Image2Data = reader.IsDBNull(reader.GetOrdinal("IMAGE2")) ? new byte[0] : (byte[])reader["IMAGE2"],
                                    Image3Data = reader.IsDBNull(reader.GetOrdinal("IMAGE3")) ? new byte[0] : (byte[])reader["IMAGE3"],
                                    Occupation = reader.GetString("OCCUPATION"),
                                    Hobbies = reader.GetString("HOBBIES")
                                };
                            }
                            else
                            {
                                Debug.WriteLine("No profile found for the given ProfileId.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return profile;
        }
    }
}
