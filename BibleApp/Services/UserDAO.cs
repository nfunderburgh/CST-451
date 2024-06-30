using Milestone.Models;
using MySqlConnector;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Milestone.Services
{
    public class UserDAO : IUserDataService
    {

        String connectionString = "Server=bibleapp.mysql.database.azure.com;User ID=thunderquack;Database=dbo;Password=WeWon2020$";
    
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
            bool success = false;

            string sqlStatment = "SELECT * FROM dbo.users WHERE email = @email and password = @password";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);
                cmd.Parameters.AddWithValue("@EMAIL", user.Email);
                cmd.Parameters.AddWithValue("@PASSWORD", user.Password);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
                return success;
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

            string sqlStatment = "SELECT 1 FROM dbo.users WHERE email = @email AND password = @password";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);
                cmd.Parameters.AddWithValue("@EMAIL", user.Email);
                cmd.Parameters.AddWithValue("@PASSWORD", user.Password);

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

            string sqlStatment = "INSERT INTO dbo.users (firstname,lastname,phone,address,state,email,password) VALUES (@firstname,@lastname,@phone,@address,@state,@email,@password)";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);
                cmd.Parameters.AddWithValue("@FIRSTNAME", user.FirstName);
                cmd.Parameters.AddWithValue("@LASTNAME", user.LastName);
                cmd.Parameters.AddWithValue("@PHONE", user.Phone);
                cmd.Parameters.AddWithValue("@ADDRESS", user.Address);
                cmd.Parameters.AddWithValue("@STATE", user.State);
                cmd.Parameters.AddWithValue("@EMAIL", user.Email);
                cmd.Parameters.AddWithValue("@PASSWORD", user.Password);

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

                return success;
            }
        }
    }
}
