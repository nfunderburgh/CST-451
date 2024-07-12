using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class MatchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
