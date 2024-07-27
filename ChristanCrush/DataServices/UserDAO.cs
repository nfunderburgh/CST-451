using ChristanCrush.Models;
using ChristanCrush.Utility;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.DataServices
{
    public class UserDAO
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";


        /// <summary>
        /// The function `FindUserByEmailAndPasswordValid` checks if a user's email and password are
        /// valid by querying the database for the hashed password and verifying it against the input
        /// password.
        /// </summary>
        /// <param name="UserModel">UserModel is a class or structure that contains information about a
        /// user. In this context, it likely has properties for the user's email and password.</param>
        /// <returns>
        /// The method `FindUserByEmailAndPasswordValid` returns a boolean value. It returns `true` if
        /// the user's email and password are valid (i.e., the password provided by the user matches the
        /// hashed password stored in the database for the corresponding email), and it returns `false`
        /// if either the email is not found in the database or the password does not match the hashed
        /// password.
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
        /// The function `GetUserInfoByEmail` retrieves the first name and last name of a user based on
        /// their email from a MySQL database.
        /// </summary>
        /// <param name="email">The code you provided is a C# method that retrieves user information
        /// (first name and last name) based on the email address provided. The method connects to a
        /// MySQL database, executes a SELECT query to fetch the user's first name and last name from
        /// the "users" table where the email matches the</param>
        /// <returns>
        /// The `GetUserInfoByEmail` method returns a string containing the concatenated `FirstName` and
        /// `LastName` of a user retrieved from the database based on the provided email address.
        /// </returns>
        public string GetUserNameByUserId(int userId)
        {
            string userInfo = null;

            string sqlStatement = "SELECT firstname, lastname FROM users WHERE id = @ID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@ID", userId);

                try
                {
                    connection.Open();
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
            }
            return userInfo;
        }

       
        /// <summary>
        /// The function `FindUserIdByEmail` retrieves the user ID from a database based on the provided
        /// email address in a C# application.
        /// </summary>
        /// <param name="UserModel">UserModel is a class or model that represents a user in the system.
        /// It likely contains properties such as email, name, id, etc. In this context, UserModel is
        /// used to pass user information, specifically the email, to the FindUserIdByEmail method for
        /// finding the corresponding user ID in the database</param>
        /// <returns>
        /// The method `FindUserIdByEmail` returns an integer value representing the user ID found in
        /// the database based on the provided email address in the `UserModel` object. If a user with
        /// the specified email exists in the database, the method will return the corresponding user
        /// ID. If no user is found, the method will return 0.
        /// </returns>
        public int FindUserIdByEmail(UserModel user)
        {
            int userId = 0;

            string sqlStatement = "SELECT Id FROM users WHERE email = @EMAIL";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@EMAIL", user.email);

                try
                {
                    connection.Open();
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
            }
            return userId;
        }


       
        /// <summary>
        /// The function `RegisterUserValid` registers a new user in a database if the email is not
        /// already registered.
        /// </summary>
        /// <param name="UserModel">The `RegisterUserValid` method you provided is responsible for
        /// registering a new user in a database if the email is not already registered. Here's a
        /// breakdown of the method:</param>
        /// <returns>
        /// The RegisterUserValid method returns a boolean value indicating whether the user
        /// registration was successful or not. If the user registration is successful, it returns true.
        /// If there was an error during registration or if the email is already registered, it returns
        /// false.
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
                        Debug.WriteLine(ex.Message);
                        success = false;
                    };
                }
            }
            else
            {
                Debug.WriteLine("Email is already used");
            }
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
