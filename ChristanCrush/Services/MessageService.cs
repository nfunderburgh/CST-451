using ChristanCrush.DataServices;
using ChristanCrush.Models;

namespace ChristanCrush.Services
{
    public class MessageService
    {
        private DBConnection database = new DBConnection();
        private MessageDAO MessageDao = new MessageDAO();

        public bool InsertMessage(MessageModel message)
        {
            return MessageDao.InsertMessage(message, database.DbConnection());
        }

        public bool DeleteMessage(int messageId)
        {
            return MessageDao.DeleteMessage(messageId, database.DbConnection());
        }
      
        public List<MessageModel> GetAllMessages()
        {
            return MessageDao.GetAllMessages(database.DbConnection());
        }
        
        public List<MessageModel> GetSenderReceiverMessages(int senderId, int receiverId)
        {
            return MessageDao.GetSenderReceiverMessages(senderId, receiverId, database.DbConnection());
        }

        public int GetMessageIdBySentAt(DateTime sentAt)
        {
            return MessageDao.GetMessageIdBySentAt(sentAt, database.DbConnection());
        }
    }
}
