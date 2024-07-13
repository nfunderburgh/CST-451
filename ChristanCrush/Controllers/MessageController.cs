using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class MessageController : Controller
    {
        [CustomAuthorization]
        public IActionResult Index()
        {
            return View();
        }
    }
}
