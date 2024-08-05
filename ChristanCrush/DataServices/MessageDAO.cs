using ChristanCrush.Models;
using MySqlConnector;
using System.Diagnostics;

namespace ChristanCrush.Services
{
    public class MessageDAO
    {
        public bool InsertMessage(MessageModel message, MySqlConnection dbConnection)
        {
            bool success = false;

            string sqlStatement = "INSERT INTO messages (senderid, receiverid, messagecontent, sentat) VALUES (@SENDERID, @RECEIVERID, @MESSAGECONTENT, @SENTAT)";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@SENDERID", message.SenderId);
            cmd.Parameters.AddWithValue("@RECEIVERID", message.ReceiverId);
            cmd.Parameters.AddWithValue("@MESSAGECONTENT", message.MessageContent);
            cmd.Parameters.AddWithValue("@SENTAT", message.SentAt.ToString("yyyy-MM-dd HH:mm:ss"));

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
                    Debug.WriteLine("Error inserting message into database!");
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

            return success;
        }

        public bool DeleteMessage(int messageId, MySqlConnection dbConnection)
        {
            string sqlStatement = "DELETE FROM messages WHERE messageid = @MESSAGEID";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@MESSAGEID", messageId);

            try
            {
                dbConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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

        public List<MessageModel> GetAllMessages(MySqlConnection dbConnection)
        {
            List<MessageModel> messages = new List<MessageModel>();
            string sqlStatement = "SELECT m.messageid, m.senderid, m.receiverid, m.messagecontent, m.sentat, u.firstname AS sendername " +
                                  "FROM Messages m " +
                                  "JOIN Users u ON m.senderid = u.Id " +
                                  "ORDER BY m.sentat ASC";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);

            try
            {
                dbConnection.Open();
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
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return messages;
        }

        public List<MessageModel> GetSenderReceiverMessages(int senderId, int receiverId, MySqlConnection dbConnection)
        {
            List<MessageModel> messages = new List<MessageModel>();
            string sqlStatement = "SELECT m.messageid, m.senderid, m.receiverid, m.messagecontent, m.sentat, u.firstname AS sendername " +
                                  "FROM Messages m " +
                                  "JOIN Users u ON m.senderid = u.Id " +
                                  "WHERE (m.senderid = @senderId AND m.receiverid = @receiverId) OR (m.senderid = @receiverId AND m.receiverid = @senderId) " +
                                  "ORDER BY m.sentat ASC";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@senderId", senderId);
            cmd.Parameters.AddWithValue("@receiverId", receiverId);

            try
            {
                dbConnection.Open();
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
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return messages;
        }

        public int GetMessageIdBySentAt(DateTime sentAt, MySqlConnection dbConnection)
        {
            int messageId = 0;
            string sqlStatement = "SELECT m.messageid " +
                                  "FROM Messages m " +
                                  "WHERE m.sentat = @SentAt " +
                                  "ORDER BY m.sentat ASC " +
                                  "LIMIT 1";


            MySqlCommand cmd = new MySqlCommand(sqlStatement, dbConnection);
            cmd.Parameters.AddWithValue("@SentAt", sentAt.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                dbConnection.Open();
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
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return messageId;
        }
    }
}
