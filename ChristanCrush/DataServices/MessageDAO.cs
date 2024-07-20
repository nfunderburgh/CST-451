using ChristanCrush.Models;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.Services
{
    public class MessageDAO
    {
        String connectionString = "Server=localhost;User ID=root;Password=root;Database=CST_451;";

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
    }
}
