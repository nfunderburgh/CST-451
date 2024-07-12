using ChristanCrush.Models;
using ChristanCrush.Utility;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.Services
{
    public class UserDAO
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";

        /// <summary>
        /// The function FindUserByNameAndPasswordValid checks if a user with a given username and password exists in the
        /// database.
        /// </summary>
        /// <param name="UserModel">A model class that represents a user. It contains properties for the user's username and
        /// password.</param>
        /// <returns>
        /// The method is returning a boolean value indicating whether the user with the given username and password exists
        /// in the database.
        /// </returns>
        public bool FindUserByEmailAndPasswordValid(UserModel user)
        {
            string sqlStatement = "SELECT password FROM users WHERE email = @email";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@email", user.email);

                connection.Open();
                object databaseHashedPassword = cmd.ExecuteScalar();

                if (databaseHashedPassword != null)
                {
                    string hashedPassword = databaseHashedPassword.ToString();
                    PasswordHasher hasher = new PasswordHasher();

                    return hasher.VerifyPassword(user.password, hashedPassword);
                }

                return false;
            }
        }

        /// <summary>
        /// The function FindUserIdByNameAndPassword takes a UserModel object as input and returns the user ID of the user
        /// with the matching username and password in the database.
        /// </summary>
        /// <param name="UserModel">A model class that represents a user. It contains properties for the user's username and
        /// password.</param>
        /// <returns>
        /// The method is returning an integer value, which represents the user ID.
        /// </returns>
        public int FindUserIdByEmailAndPassword(UserModel user)
        {
            int userId = 0;

            string sqlStatment = "SELECT 1 FROM users WHERE email = @email AND password = @password";

            PasswordHasher hasher = new PasswordHasher();
            string hashedPassword = hasher.HashPassword(user.password);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);
                cmd.Parameters.AddWithValue("@EMAIL", user.email);
                cmd.Parameters.AddWithValue("@PASSWORD", hashedPassword);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userId = ((int)(long)reader[0]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return userId;
        }

        /// <summary>
        /// The function `RegisterUserValid` inserts a user into a database table and returns a boolean value indicating
        /// whether the operation was successful or not.
        /// </summary>
        /// <param name="UserModel">A model class that represents a user with properties such as FirstName, LastName, Sex,
        /// Age, State, Email, UserName, and Password.</param>
        /// <returns>
        /// The method is returning a boolean value indicating whether the user registration was successful or not.
        /// </returns>
        public bool RegisterUserValid(UserModel user)
        {
            bool success = false;

            if (!IsEmailRegistered(user))
            {

                string sqlStatment = "INSERT INTO users (firstname,lastname,email,password,dateofbirth,gender,createdate) VALUES (@firstname,@lastname,@email,@password,@dateofbirth,@gender,@createdate)";

                PasswordHasher hasher = new PasswordHasher();
                string hashedPassword = hasher.HashPassword(user.password);

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);
                    cmd.Parameters.AddWithValue("@FIRSTNAME", user.first_name);
                    cmd.Parameters.AddWithValue("@LASTNAME", user.last_name);
                    cmd.Parameters.AddWithValue("@EMAIL", user.email);
                    cmd.Parameters.AddWithValue("@PASSWORD", hashedPassword);
                    cmd.Parameters.AddWithValue("@DATEOFBIRTH", user.date_of_birth);
                    cmd.Parameters.AddWithValue("@GENDER", user.gender);
                    cmd.Parameters.AddWithValue("@CREATEDATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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
                            Debug.WriteLine("Error inserting user into database!");
                            success = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        success = false;
                    };
                }
            }
            Debug.WriteLine("Email is already used");
            return success;
        }

        public bool IsEmailRegistered(UserModel user)
        {
            string sqlStatement = "SELECT COUNT(*) FROM users WHERE email = @Email";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@Email", user.email);

                connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; 
            }
        }
    }
}
