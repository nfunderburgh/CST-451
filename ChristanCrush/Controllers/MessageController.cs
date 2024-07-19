using ChristanCrush.Models;
using ChristanCrush.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChristanCrush.Controllers
{
    public class MessageController : Controller
    {


        /// <summary>
        /// The Index function in C# is decorated with a CustomAuthorization attribute and returns a
        /// View result.
        /// </summary>
        /// <returns>
        /// A View is being returned from the Index action method.
        /// </returns>
        [CustomAuthorization]
        public IActionResult Index()
        {
            MessageDAO messageDAO = new MessageDAO();
            var messages = messageDAO.GetAllMessages();

            return View(messages);
        }

        public IActionResult SendMessage(MessageModel message)
        {
            MessageDAO messageDAO = new MessageDAO();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            message.ReceiverId = 2; // Need To Get Receiver Id Somehow
            message.SenderId = userId;

            if (messageDAO.InsertMessage(message))
            {
                Debug.WriteLine("Inserted Message");
            }
            else
            {
                Debug.WriteLine("Fail To Insert Message");
            }

            return RedirectToAction("Index");
        }

    }
}
