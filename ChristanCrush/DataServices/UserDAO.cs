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
        /// The function `FindUserByEmailAndPasswordValid` checks if a user's email and password are
        /// valid by querying the database for the hashed password and verifying it using a
        /// PasswordHasher.
        /// </summary>
        /// <param name="UserModel">UserModel is a class that likely contains properties for user
        /// information such as email and password.</param>
        /// <returns>
        /// The method `FindUserByEmailAndPasswordValid` returns a boolean value. It returns `true` if
        /// the user's email and password are valid (i.e., the password provided by the user matches the
        /// hashed password stored in the database for the corresponding email address), and it returns
        /// `false` if either the email is not found in the database or the password does not match the
        /// hashed password.
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
        /// The function FindUserIdByEmailAndPassword searches for a user ID in the database based on
        /// the provided email and hashed password.
        /// </summary>
        /// <param name="UserModel">UserModel is a class that contains the properties email and
        /// password.</param>
        /// <returns>
        /// The method `FindUserIdByEmailAndPassword` is returning an integer value representing the
        /// user ID found in the database based on the provided email and password in the `UserModel`
        /// object.
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
        /// The function `RegisterUserValid` checks if a user's email is already registered and inserts
        /// the user into a database if not.
        /// </summary>
        /// <param name="UserModel">It looks like you are trying to register a user in a database using
        /// the provided UserModel object. The UserModel likely contains properties such as first_name,
        /// last_name, email, password, date_of_birth, and gender.</param>
        /// <returns>
        /// The method `RegisterUserValid` returns a boolean value indicating whether the user
        /// registration was successful (`true`) or not (`false`).
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


        /// <summary>
        /// The function `DeleteUserByEmail` deletes a user from a database based on their email
        /// address.
        /// </summary>
        /// <param name="UserModel">UserModel is a class or structure that represents a user in the
        /// system. It likely contains properties such as email, name, ID, and other relevant
        /// information about a user. In this context, UserModel is used to pass user information,
        /// specifically the email address, to the DeleteUserByEmail method for deleting</param>
        /// <returns>
        /// The DeleteUserByEmail method returns a boolean value. It returns true if the deletion
        /// operation was successful and at least one row was affected in the database. Otherwise, it
        /// returns false if an exception occurred during the deletion process or if no rows were
        /// affected.
        /// </returns>
        public bool DeleteUserByEmail(UserModel user)
        {
            string sqlStatement = "DELETE FROM users WHERE email = @Email";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@Email", user.email);

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
        
        /// <summary>
        /// The IsEmailRegistered function checks if a given email is already registered in the
        /// database.
        /// </summary>
        /// <param name="UserModel">UserModel is a class or structure that contains information about a
        /// user. In this case, it likely has a property called "email" which stores the email address
        /// of the user.</param>
        /// <returns>
        /// The IsEmailRegistered method returns a boolean value indicating whether the email provided
        /// in the UserModel object is already registered in the database. It queries the database to
        /// check if there are any records with the same email address and returns true if the count is
        /// greater than 0, indicating that the email is already registered. If an exception occurs
        /// during the database operation, it catches the exception, prints the error message to
        /// </returns>
        public bool IsEmailRegistered(UserModel user)
        {
            string sqlStatement = "SELECT COUNT(*) FROM users WHERE email = @Email";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@Email", user.email);

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
    }
}
