using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
