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
        private MessageService messageService = new MessageService();

        [CustomAuthorization]
        public IActionResult Index(int matchUserId)
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            MatchedUserId = matchUserId;
            var messages = messageService.GetSenderReceiverMessages(matchUserId, userId);

            return View(messages);
        }

        [CustomAuthorization]
        public IActionResult SendMessage(MessageModel message)
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            message.ReceiverId = MatchedUserId;
            message.SenderId = userId;

            if (messageService.InsertMessage(message))
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
