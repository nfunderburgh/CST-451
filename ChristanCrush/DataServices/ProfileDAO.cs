using ChristanCrush.Models;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.DataServices
{
    public class ProfileDAO
    {
        public bool InsertProfile(ProfileModel profile, MySqlConnection dbConnection)
        {
            bool success = false;

            string sqlStatement = @"INSERT INTO profiles (USERID, BIO, IMAGE1, IMAGE2, IMAGE3, OCCUPATION, HOBBIES) VALUES (@USERID, @BIO, @IMAGE1, @IMAGE2, @IMAGE3, @OCCUPATION, @HOBBIES)";

            using (MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection))
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
                    dbConnection.Open();
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
                finally
                {
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
            }

            return success;
        }

        public ProfileModel GetProfileByUserId(int userId, MySqlConnection dbConnection)
        {
            ProfileModel profile = null;

            string sqlStatement = @"SELECT PROFILEID, USERID, BIO, IMAGE1, IMAGE2, IMAGE3, OCCUPATION, HOBBIES 
                                    FROM profiles WHERE USERID = @USERID";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);

            cmd.Parameters.AddWithValue("@USERID", userId);

            try
            {
                dbConnection.Open();
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
                        Debug.WriteLine("No profile found for the given UserId.");
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

            return profile;
        }

        public ProfileModel GetRandomProfile(int currentUserId, MySqlConnection dbConnection)
        {
            ProfileModel profile = null;

            string sqlStatement = @"SELECT PROFILEID, USERID, BIO, IMAGE1, IMAGE2, IMAGE3, OCCUPATION, HOBBIES 
                            FROM profiles 
                            WHERE USERID != @CURRENTUSERID
                            ORDER BY RAND()
                            LIMIT 1";

            using (MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection))
            {
                cmd.Parameters.AddWithValue("@CURRENTUSERID", currentUserId);

                try
                {
                    dbConnection.Open();
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
                finally
                {
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
            }

            return profile;
        }

        public ProfileModel GetProfileByProfileId(int profileId, MySqlConnection dbConnection)
        {
            ProfileModel profile = null;

            string sqlStatement = @"SELECT PROFILEID, USERID, BIO, IMAGE1, IMAGE2, IMAGE3, OCCUPATION, HOBBIES 
                            FROM profiles WHERE PROFILEID = @PROFILEID";

            using (MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection))
            {
                cmd.Parameters.AddWithValue("@PROFILEID", profileId);

                try
                {
                    dbConnection.Open();
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
                finally
                {
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
            }

            return profile;
        }

        public List<ProfileModel> GetProfilesMatchedWithUser(int userId, MySqlConnection dbConnection)
        {
            List<ProfileModel> profiles = new List<ProfileModel>();

            string sqlStatement = @"SELECT u.FIRSTNAME, u.LASTNAME, p.IMAGE1, u.ID FROM profiles p
                                JOIN Users u ON p.USERID = u.Id JOIN Matches m ON (p.USERID = m.UserId2 AND m.UserId1 = @UserId)OR (p.USERID = m.UserId1 AND m.UserId2 = @UserId);";

            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);

            cmd.Parameters.AddWithValue("@UserId", userId);

            try
            {
                dbConnection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProfileModel profile = new ProfileModel
                        {
                            FullName = reader.GetString("FIRSTNAME") + " " + reader.GetString("LASTNAME"),
                            Image1Data = (byte[])reader["IMAGE1"],
                            UserId = reader.GetInt32("ID")
                        };
                        profiles.Add(profile);
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

            return profiles;
        }

        public bool DeleteProfile(int profileId, MySqlConnection dbConnection)
        {
            bool success = false;

            string sqlStatement = "DELETE FROM profiles WHERE PROFILEID = @PROFILEID";

            using (MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection))
            {
                cmd.Parameters.AddWithValue("@PROFILEID", profileId);

                try
                {
                    dbConnection.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        success = true;
                    }
                    else
                    {
                        Debug.WriteLine("Error deleting profile from database!");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
                finally
                {
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
            }
            return success;
        }
    }
}
