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

            string sqlStatement = "INSERT INTO messages (senderid, receiverid, messagecontext, sentat) VALUES (@SENDERID, @RECEIVERID, @MESSAGECONTEXT, @SENTAT)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@SENDERID", message.SenderId);
                cmd.Parameters.AddWithValue("@RECEIVERID", message.ReceiverId);
                cmd.Parameters.AddWithValue("@MESSAGECONTEXT", message.MessageContent);
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
                    Console.WriteLine(ex.Message);
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
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public List<MessageModel> GetMessages(int senderId, int receiverId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            string sqlStatement = "SELECT messageid, senderid, receiverid, messagecontent, sentat " +
                                  "FROM messages " +
                                  "WHERE (senderid = @SENDERID AND receiverid = @RECEIVERID) " +
                                  "   OR (senderid = @RECEIVERID AND receiverid = @SENDERID) " +
                                  "ORDER BY sent_at ASC";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@SENDERID", senderId);
                cmd.Parameters.AddWithValue("@RECEIVERID", receiverId);

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

                            messages.Add(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return messages;
        }

        public List<MessageModel> GetAllMessages()
        {
            List<MessageModel> messages = new List<MessageModel>();
            string sqlStatement = "SELECT m.messageid, m.senderid, m.receiverid, m.messagecontext, m.sentat, u.firstname AS sendername " +
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
                            message.MessageContent = reader["messagecontext"].ToString();
                            message.SentAt = Convert.ToDateTime(reader["sentat"]);
                            message.SenderName = reader["sendername"].ToString();

                            messages.Add(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return messages;
        }
    }
}
