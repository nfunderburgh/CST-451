using ChristanCrush.Models;
using ChristanCrush.Services;
using ChristanCrush.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChristanCrush.Controllers
{
    public class MessageController : Controller
    {
        public static int MatchedUserId = 0;

        /// <summary>
        /// The Index function retrieves all messages from the database and returns them to the view.
        /// </summary>
        /// <returns>
        /// The `Index` method is returning a view with the `messages` data retrieved from the
        /// `messageDAO.GetAllMessages()` method.
        /// </returns>
        [CustomAuthorization]
        public IActionResult Index(int matchUserId)
        {
            MessageDAO messageDAO = new MessageDAO();
            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            MatchedUserId = matchUserId;
            var messages = messageDAO.GetSenderReceiverMessages(matchUserId, userId);

            return View(messages);
        }

        /// <summary>
        /// The SendMessage function inserts a message into the database with the sender's and
        /// receiver's IDs.
        /// </summary>
        /// <param name="MessageModel">MessageModel is a model class that represents a message with
        /// properties like MessageId, SenderId, ReceiverId, Content, Timestamp, etc. It is used to pass
        /// message data between the view and the controller in an ASP.NET application.</param>
        /// <returns>
        /// The method `SendMessage` is returning a `RedirectToAction` result with the action name
        /// "Index".
        /// </returns>
        public IActionResult SendMessage(MessageModel message)
        {
            MessageDAO messageDAO = new MessageDAO();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            message.ReceiverId = MatchedUserId;
            message.SenderId = userId;

            if (messageDAO.InsertMessage(message))
            {
                Debug.WriteLine("Inserted Message");
            }
            else
            {
                Debug.WriteLine("Fail To Insert Message");
            }

            return RedirectToAction("Index", "Message", new { matchUserId = message.ReceiverId });
        }

    }
}
