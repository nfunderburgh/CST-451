using ChristanCrush.Models;
using ChristanCrush.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristanCrush.Tests
{
    public class MessageDAOTest
    {
        private readonly MessageDAO MessageDao = new MessageDAO();

        [Fact]
        public void InsertMessage_ShouldReturnTrue()
        {
            var message = new MessageModel
            {
                SenderId = 4,
                ReceiverId = 5,
                MessageContent = "Test message",
                SentAt = DateTime.Now
            };

            var result = MessageDao.InsertMessage(message);

            int messageId = MessageDao.GetMessageIdBySentAt(message.SentAt);

            Assert.True(result);

            MessageDao.DeleteMessage(messageId);
        }

        [Fact]
        public void DeleteMessage_ShouldReturnTrue()
        {
            var message = new MessageModel
            {
                SenderId = 4,
                ReceiverId = 5,
                MessageContent = "Test message",
                SentAt = DateTime.Now
            };
            MessageDao.InsertMessage(message);

            int messageId = MessageDao.GetMessageIdBySentAt(message.SentAt);
            
            var result = MessageDao.DeleteMessage(messageId);

            Assert.True(result);
        }

        [Fact]
        public void GetAllMessages()
        {
            // Arrange
            var message1 = new MessageModel
            {
                SenderId = 4,
                ReceiverId = 5,
                MessageContent = "Test message 1",
                SentAt = DateTime.Now
            };

            var message2 = new MessageModel
            {
                SenderId = 5,
                ReceiverId = 4,
                MessageContent = "Test message 2",
                SentAt = DateTime.Now
            };

            MessageDao.InsertMessage(message1);
            MessageDao.InsertMessage(message2);

            var messages = MessageDao.GetAllMessages();

            Assert.NotEmpty(messages);
            int messageId1 = MessageDao.GetMessageIdBySentAt(message1.SentAt);
            MessageDao.DeleteMessage(messageId1);

            Assert.NotEmpty(messages);
            int messageId2 = MessageDao.GetMessageIdBySentAt(message2.SentAt);
            MessageDao.DeleteMessage(messageId2);
        }

        [Fact]
        public void GetSenderReceiverMessages()
        {
            // Arrange
            var message = new MessageModel
            {
                SenderId = 4,
                ReceiverId = 5,
                MessageContent = "Test message",
                SentAt = DateTime.Now
            };

            MessageDao.InsertMessage(message);
            var messages = MessageDao.GetSenderReceiverMessages(4, 5);

            int messageId = MessageDao.GetMessageIdBySentAt(message.SentAt);
            Assert.NotEmpty(messages);
            MessageDao.DeleteMessage(messageId);
        }
    }
}
