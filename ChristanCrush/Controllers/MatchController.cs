using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class MatchController : Controller
    {
        [CustomAuthorization]
        public IActionResult Index()
        {
            return View();
        }
    }
}
