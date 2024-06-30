using BibleApp.Models;
using MySqlConnector;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BibleApp.Services
{
    public class BibleDAO
    {

        String connectionString = "Server=bibleapp.mysql.database.azure.com;User ID=thunderquack;Database=dbo;Password=WeWon2020$";

        /// <summary>
        /// The function searches for verses in a database table that contain a given search term and returns a list of
        /// BibleModel objects representing the found verses.
        /// </summary>
        /// <param name="searchTerm">The searchTerm parameter is a string that represents the term you want to search for in
        /// the database.</param>
        /// <returns>
        /// The method is returning a List of BibleModel objects.
        /// </returns>
        public List<BibleModel> SearchVersesBoth(string searchTerm)
        {

            List<BibleModel> foundVerses = new List<BibleModel>();

            String sqlStatment = "SELECT * FROM dbo.t_bbe WHERE t LIKE @t";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatment, connection);

                command.Parameters.AddWithValue("@t", '%' + searchTerm + '%');

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundVerses.Add(new BibleModel((int)reader[0],(int)reader[1], (int)reader[2], (int)reader[3], (string)reader[4]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundVerses;
        }

        /// <summary>
        /// The function `SearchVersesNew` searches for verses in a database table based on a given search term and returns
        /// a list of `BibleModel` objects representing the found verses.
        /// </summary>
        /// <param name="searchTerm">The `searchTerm` parameter is a string that represents the term or keyword that you
        /// want to search for in the database. This method searches for verses in the database table `dbo.t_bbe` where the
        /// column `t` contains the specified search term.</param>
        /// <returns>
        /// The method is returning a List of BibleModel objects.
        /// </returns>
        public List<BibleModel> SearchVersesNew(string searchTerm)
        {
            List<BibleModel> foundVerses = new List<BibleModel>();

            String sqlStatment = "SELECT * FROM dbo.t_bbe WHERE id > 39999999 AND t LIKE @t";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatment, connection);

                command.Parameters.AddWithValue("@t", '%' + searchTerm + '%');

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundVerses.Add(new BibleModel((int)reader[0], (int)reader[1], (int)reader[2], (int)reader[3], (string)reader[4]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundVerses;
        }

        /// <summary>
        /// The function checks if a verse has been favorited by a specific user.
        /// </summary>
        /// <param name="verseid">The verseid parameter is an integer that represents the ID of a specific verse in a
        /// database.</param>
        /// <param name="userid">The `userid` parameter is the unique identifier of the user for whom we want to check if a
        /// favorite exists.</param>
        /// <returns>
        /// The method is returning a boolean value indicating whether a favorite exists for a given verseid and userid.
        /// </returns>
        internal bool CheckIfFavorited(int verseid, int userid)
        {
            bool favoriteExists = false;

            string sqlStatement = "SELECT 1 FROM favorites WHERE verseid = @verseid AND userid = @userid";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("@VERSEID", verseid);
                    cmd.Parameters.AddWithValue("@USERID", userid);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            favoriteExists = true;
                        }
                    }
                }
            }

            return favoriteExists;
        }

        /// <summary>
        /// The function searches for verses in a database table based on a given search term and returns a list of
        /// BibleModel objects representing the found verses.
        /// </summary>
        /// <param name="searchTerm">The searchTerm parameter is a string that represents the term you want to search for in
        /// the database.</param>
        /// <returns>
        /// The method is returning a List of BibleModel objects.
        /// </returns>
        public List<BibleModel> SearchVersesOld(string searchTerm)
        {
            List<BibleModel> foundVerses = new List<BibleModel>();

            String sqlStatment = "SELECT * FROM dbo.t_bbe WHERE id < 39999999 AND t LIKE @t";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatment, connection);

                command.Parameters.AddWithValue("@t", '%' + searchTerm + '%');

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundVerses.Add(new BibleModel((int)reader[0], (int)reader[1], (int)reader[2], (int)reader[3], (string)reader[4]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundVerses;
        }

        /// <summary>
        /// The function `AddFavoriteVerse` inserts a user's favorite verse into a database table.
        /// </summary>
        /// <param name="UserId">The UserId parameter is an integer that represents the ID of the user who wants to add a
        /// favorite verse.</param>
        /// <param name="VerseId">The VerseId parameter is an integer that represents the ID of the verse that the user
        /// wants to add to their favorites.</param>
        /// <returns>
        /// The method is returning a boolean value indicating whether the favorite verse was successfully added or not.
        /// </returns>
        public bool AddFavoriteVerse(int UserId, int VerseId)
        {
            bool success = false;

            string sqlStatment = "INSERT INTO dbo.favorites (userid,verseid) VALUES (@userid,@verseid)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);
                cmd.Parameters.AddWithValue("@USERID", UserId);
                cmd.Parameters.AddWithValue("@VERSEID", VerseId);

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

        /// <summary>
        /// The UnfavoriteVerse function deletes a verse from a user's favorites list in a database.
        /// </summary>
        /// <param name="UserId">The UserId parameter is an integer that represents the ID of the user who wants to
        /// unfavorite a verse.</param>
        /// <param name="VerseId">The VerseId parameter is an integer that represents the ID of the verse that the user
        /// wants to unfavorite.</param>
        /// <returns>
        /// The method is returning a boolean value indicating whether the verse was successfully unfavorited or not.
        /// </returns>
        public bool UnfavoriteVerse(int UserId, int VerseId)
        {
            bool success = false;

            string sqlStatment = "DELETE FROM dbo.favorites WHERE userid = @userid AND verseid = @verseid";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);
                cmd.Parameters.AddWithValue("@USERID", UserId);
                cmd.Parameters.AddWithValue("@VERSEID", VerseId);

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

        /// <summary>
        /// The function "DisplayFavoriteVerses" retrieves a list of favorite Bible verses for a given user from a database.
        /// </summary>
        /// <param name="UserId">The UserId parameter is an integer that represents the user's ID. It is used to filter the
        /// favorite verses based on the user's ID.</param>
        /// <returns>
        /// The method is returning a List of BibleModel objects.
        /// </returns>
        public List<BibleModel> DisplayFavoriteVerses(int UserId)
        {
            List<BibleModel> foundVerses = new List<BibleModel>();

            String sqlStatment = "SELECT f.userId, t.id, t.b, t.c, t.v, t.t " +
                                 "FROM favorites f " +
                                 "JOIN t_bbe t ON f.verseId = t.id " +
                                 "WHERE f.userId = @UserId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);

                cmd.Parameters.AddWithValue("@USERID", UserId);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        foundVerses.Add(new BibleModel((int)reader[1], (int)reader[2], (int)reader[3], (int)reader[4], (string)reader[5]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundVerses;
        }

        /// <summary>
        /// The RandomVerse function retrieves a random verse from a database table and returns it as a list of BibleModel
        /// objects.
        /// </summary>
        /// <returns>
        /// The method `RandomVerse` returns a `List<BibleModel>` containing a randomly selected verse from the database.
        /// </returns>
        public List<BibleModel> RandomVerse()
        {
            List<BibleModel> foundVerses = new List<BibleModel>();

            String sqlStatment = "SELECT * FROM dbo.t_bbe ORDER BY RAND() LIMIT 1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatment, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        foundVerses.Add(new BibleModel((int)reader[0], (int)reader[1], (int)reader[2], (int)reader[3], (string)reader[4]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundVerses;
        }
    }
}
