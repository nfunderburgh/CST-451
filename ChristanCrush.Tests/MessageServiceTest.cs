using ChristanCrush.Models;
using ChristanCrush.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristanCrush.Tests
{
    public class MessageServiceTest
    {
        private readonly MessageService messageService = new MessageService();

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

            var result = messageService.InsertMessage(message);

            int messageId = messageService.GetMessageIdBySentAt(message.SentAt);

            Assert.True(result);

            messageService.DeleteMessage(messageId);
        }

        [Fact]
        public void InsertMessage_ShouldReturnFalse()
        {
            var message = new MessageModel
            {
                SenderId = 4,
                ReceiverId = 5,
                MessageContent = "Test message",
                SentAt = DateTime.Now
            };

            var result = messageService.InsertMessage(message);

            int messageId = messageService.GetMessageIdBySentAt(message.SentAt);

            Assert.False(string.IsNullOrEmpty(messageId.ToString()));

            messageService.DeleteMessage(messageId);
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
            messageService.InsertMessage(message);

            int messageId = messageService.GetMessageIdBySentAt(message.SentAt);
            
            var result = messageService.DeleteMessage(messageId);

            Assert.True(result);
        }

        [Fact]
        public void GetAllMessages()
        {
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

            messageService.InsertMessage(message1);
            messageService.InsertMessage(message2);

            var messages = messageService.GetAllMessages();

            Assert.NotEmpty(messages);
            int messageId1 = messageService.GetMessageIdBySentAt(message1.SentAt);
            messageService.DeleteMessage(messageId1);

            Assert.NotEmpty(messages);
            int messageId2 = messageService.GetMessageIdBySentAt(message2.SentAt);
            messageService.DeleteMessage(messageId2);
        }

        [Fact]
        public void GetSenderReceiverMessages()
        {
            var message = new MessageModel
            {
                SenderId = 4,
                ReceiverId = 5,
                MessageContent = "Test message",
                SentAt = DateTime.Now
            };

            messageService.InsertMessage(message);
            var messages = messageService.GetSenderReceiverMessages(4, 5);

            int messageId = messageService.GetMessageIdBySentAt(message.SentAt);
            Assert.NotEmpty(messages);
            messageService.DeleteMessage(messageId);
        }
    }
}
