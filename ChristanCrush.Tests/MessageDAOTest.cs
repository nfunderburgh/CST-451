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
        public void InsertMessageShouldReturnTrue()
        {
            var message = new MessageModel
            {
                SenderId = 1,
                ReceiverId = 2,
                MessageContent = "Test message",
                SentAt = DateTime.Now
            };

            var result = MessageDao.InsertMessage(message);

            int messageId = MessageDao.GetMessageIdBySentAt(message.SentAt);

            Assert.True(result);

            MessageDao.DeleteMessage(messageId);
        }

        [Fact]
        public void DeleteMessage()
        {
            var message = new MessageModel
            {
                SenderId = 1,
                ReceiverId = 2,
                MessageContent = "Test message",
                SentAt = DateTime.Now
            };
            MessageDao.InsertMessage(message);

            int messageId = MessageDao.GetMessageIdBySentAt(message.SentAt);
            
            var result = MessageDao.DeleteMessage(messageId);

            Assert.True(result);
        }

        [Fact]
        public void GetAllMessages_ShouldReturnMessages()
        {
            // Arrange
            var message1 = new MessageModel
            {
                MessageId = 100,
                SenderId = 1,
                ReceiverId = 2,
                MessageContent = "Test message 1",
                SentAt = DateTime.Now
            };

            var message2 = new MessageModel
            {
                MessageId = 101,
                SenderId = 2,
                ReceiverId = 1,
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
                SenderId = 1,
                ReceiverId = 2,
                MessageContent = "Test message",
                SentAt = DateTime.Now
            };

            MessageDao.InsertMessage(message);
            var messages = MessageDao.GetSenderReceiverMessages(1, 2);

            Assert.NotEmpty(messages);
            int messageId = MessageDao.GetMessageIdBySentAt(message.SentAt);
            Assert.NotEmpty(messages);
            MessageDao.DeleteMessage(messageId);
        }
    }
}
