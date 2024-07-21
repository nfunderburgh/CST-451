using ChristanCrush.Utility;
using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class MatchController : Controller
    {
        /// <summary>
        /// The Index function is decorated with a custom authorization attribute and returns a view.
        /// </summary>
        /// <returns>
        /// A View is being returned from the Index action method.
        /// </returns>
        [CustomAuthorization]
        public IActionResult Index()
        {
            return View();
        }
    }
}
