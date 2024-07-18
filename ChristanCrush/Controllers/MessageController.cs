using ChristanCrush.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
