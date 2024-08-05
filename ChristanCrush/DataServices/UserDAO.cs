using ChristanCrush.Models;
using ChristanCrush.Utility;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.DataServices
{
    public class UserDAO
    {
        public bool FindUserByEmailAndPasswordValid(UserModel user, MySqlConnection dbConnection)
        {
            string sqlStatement = "SELECT password FROM users WHERE email = @email";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@email", user.email);

            dbConnection.Open();
            object databaseHashedPassword = cmd.ExecuteScalar();

            if (databaseHashedPassword != null)
            {
                string hashedPassword = databaseHashedPassword.ToString();
                PasswordHasher hasher = new PasswordHasher();

                return hasher.VerifyPassword(user.password, hashedPassword);
            }
            dbConnection.Open();
            return false;

        }

        public string GetUserNameByUserId(int userId, MySqlConnection dbConnection)
        {
            string userInfo = null;

            string sqlStatement = "SELECT firstname, lastname FROM users WHERE id = @ID";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@ID", userId);

            try
            {
                dbConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    userInfo = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();

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
            return userInfo;
        }

        public int FindUserIdByEmail(UserModel user, MySqlConnection dbConnection)
        {
            int userId = 0;

            string sqlStatement = "SELECT Id FROM users WHERE email = @EMAIL";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@EMAIL", user.email);

            try
            {
                dbConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // Use if since we expect a single row
                {
                    userId = reader.GetInt32(0); // Directly get the integer value from the reader
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
            return userId;
        }

        public bool RegisterUserValid(UserModel user, MySqlConnection dbConnection)
        {
            bool success = false;

            if (!IsEmailRegistered(user, dbConnection))
            {

                string sqlStatment = "INSERT INTO users (firstname,lastname,email,password,dateofbirth,gender,createdate) VALUES (@firstname,@lastname,@email,@password,@dateofbirth,@gender,@createdate)";

                PasswordHasher hasher = new PasswordHasher();
                string hashedPassword = hasher.HashPassword(user.password);

                MySqlCommand cmd = new MySqlCommand(sqlStatment, dbConnection);
                cmd.Parameters.AddWithValue("@FIRSTNAME", user.first_name);
                cmd.Parameters.AddWithValue("@LASTNAME", user.last_name);
                cmd.Parameters.AddWithValue("@EMAIL", user.email);
                cmd.Parameters.AddWithValue("@PASSWORD", hashedPassword);
                cmd.Parameters.AddWithValue("@DATEOFBIRTH", user.date_of_birth);
                cmd.Parameters.AddWithValue("@GENDER", user.gender);
                cmd.Parameters.AddWithValue("@CREATEDATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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
                        Debug.WriteLine("Error inserting user into database!");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
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
            else
            {
                Debug.WriteLine("Email is already used");
            }
            return success;
        }

        public bool DeleteUserByEmail(UserModel user, MySqlConnection dbConnection)
        {
            string sqlStatement = "DELETE FROM users WHERE email = @Email";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@Email", user.email);

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

        public bool IsEmailRegistered(UserModel user, MySqlConnection dbConnection)
        {
            string sqlStatement = "SELECT COUNT(*) FROM users WHERE email = @Email";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@Email", user.email);

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
