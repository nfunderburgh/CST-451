using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
