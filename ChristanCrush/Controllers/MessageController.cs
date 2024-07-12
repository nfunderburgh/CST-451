using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
