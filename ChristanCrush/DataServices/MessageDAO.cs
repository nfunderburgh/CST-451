using ChristanCrush.Models;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.Services
{
    public class MessageDAO
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";

        /// <summary>
        /// The function `InsertMessage` inserts a message into a database table using parameterized SQL
        /// statements in C#.
        /// </summary>
        /// <param name="MessageModel">The `InsertMessage` method you provided is responsible for
        /// inserting a message into a database table named `messages`. It takes a `MessageModel` object
        /// as a parameter, which likely contains information about the message such as sender ID,
        /// receiver ID, message content, and sent timestamp.</param>
        /// <returns>
        /// The method `InsertMessage` returns a boolean value indicating whether the message insertion
        /// was successful (`true`) or not (`false`).
        /// </returns>
        public bool InsertMessage(MessageModel message)
        {
            bool success = false;

            string sqlStatement = "INSERT INTO messages (senderid, receiverid, messagecontent, sentat) VALUES (@SENDERID, @RECEIVERID, @MESSAGECONTENT, @SENTAT)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@SENDERID", message.SenderId);
                cmd.Parameters.AddWithValue("@RECEIVERID", message.ReceiverId);
                cmd.Parameters.AddWithValue("@MESSAGECONTENT", message.MessageContent);
                cmd.Parameters.AddWithValue("@SENTAT", message.SentAt.ToString("yyyy-MM-dd HH:mm:ss"));

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
                        Debug.WriteLine("Error inserting message into database!");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// The DeleteMessage function deletes a message from a database using the provided message ID.
        /// </summary>
        /// <param name="messageId">The `messageId` parameter is an integer value representing the
        /// unique identifier of the message that you want to delete from the database table
        /// `messages`.</param>
        /// <returns>
        /// The DeleteMessage method returns a boolean value. It returns true if the deletion operation
        /// was successful and at least one row was affected in the database. Otherwise, it returns
        /// false if an exception occurred during the deletion process or if no rows were affected.
        /// </returns>
        public bool DeleteMessage(int messageId)
        {
            string sqlStatement = "DELETE FROM messages WHERE messageid = @MESSAGEID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@MESSAGEID", messageId);

                try
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// The function `GetAllMessages` retrieves a list of messages from a database table and maps
        /// the data to a list of `MessageModel` objects.
        /// </summary>
        /// <returns>
        /// This method `GetAllMessages()` returns a list of `MessageModel` objects containing message
        /// information retrieved from the database.
        /// </returns>
        public List<MessageModel> GetAllMessages()
        {
            List<MessageModel> messages = new List<MessageModel>();
            string sqlStatement = "SELECT m.messageid, m.senderid, m.receiverid, m.messagecontent, m.sentat, u.firstname AS sendername " +
                                  "FROM Messages m " +
                                  "JOIN Users u ON m.senderid = u.Id " +
                                  "ORDER BY m.sentat ASC";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MessageModel message = new MessageModel();
                            message.MessageId = Convert.ToInt32(reader["messageid"]);
                            message.SenderId = Convert.ToInt32(reader["senderid"]);
                            message.ReceiverId = Convert.ToInt32(reader["receiverid"]);
                            message.MessageContent = reader["messagecontent"].ToString();
                            message.SentAt = Convert.ToDateTime(reader["sentat"]);
                            message.SenderName = reader["sendername"].ToString();

                            messages.Add(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return messages;
        }

        /// <summary>
        /// The function retrieves messages between two users based on sender and receiver IDs from a
        /// database and returns a list of MessageModel objects.
        /// </summary>
        /// <param name="senderId">The `senderId` parameter in the `GetSenderReceiverMessages` method
        /// represents the ID of the user who is sending the message. This method retrieves messages
        /// between two users based on the sender and receiver IDs provided as parameters.</param>
        /// <param name="receiverId">The `receiverId` parameter in the `GetSenderReceiverMessages`
        /// method represents the ID of the user who is receiving the messages. This method retrieves
        /// messages between two users based on their sender and receiver IDs.</param>
        /// <returns>
        /// This method returns a list of `MessageModel` objects that represent messages between two
        /// users identified by `senderId` and `receiverId`. The messages are retrieved from the
        /// database based on the provided sender and receiver IDs, sorted by the timestamp they were
        /// sent at.
        /// </returns>
        public List<MessageModel> GetSenderReceiverMessages(int senderId, int receiverId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            string sqlStatement = "SELECT m.messageid, m.senderid, m.receiverid, m.messagecontent, m.sentat, u.firstname AS sendername " +
                                  "FROM Messages m " +
                                  "JOIN Users u ON m.senderid = u.Id " +
                                  "WHERE (m.senderid = @senderId AND m.receiverid = @receiverId) OR (m.senderid = @receiverId AND m.receiverid = @senderId) " +
                                  "ORDER BY m.sentat ASC";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@senderId", senderId);
                cmd.Parameters.AddWithValue("@receiverId", receiverId);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MessageModel message = new MessageModel();
                            message.MessageId = Convert.ToInt32(reader["messageid"]);
                            message.SenderId = Convert.ToInt32(reader["senderid"]);
                            message.ReceiverId = Convert.ToInt32(reader["receiverid"]);
                            message.MessageContent = reader["messagecontent"].ToString();
                            message.SentAt = Convert.ToDateTime(reader["sentat"]);
                            message.SenderName = reader["sendername"].ToString();

                            messages.Add(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return messages;
        }

        public int GetMessageIdBySentAt(DateTime sentAt)
        {
            int messageId = 0;
            string sqlStatement = "SELECT m.messageid " +
                                  "FROM Messages m " +
                                  "WHERE m.sentat = @SentAt " +
                                  "ORDER BY m.sentat ASC " +
                                  "LIMIT 1";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@SentAt", sentAt.ToString("yyyy-MM-dd HH:mm:ss"));

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        messageId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return messageId;
        }
    }
}
