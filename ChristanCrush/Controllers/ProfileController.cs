using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class ProfileController : Controller
    {
        [CustomAuthorization]
        public IActionResult Index()
        {
            return View();
        }
    }
}
