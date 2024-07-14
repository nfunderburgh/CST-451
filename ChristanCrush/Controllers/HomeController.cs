using ChristanCrush.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChristanCrush.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /* The `public HomeController(ILogger<HomeController> logger)` constructor in the
        `HomeController` class is initializing a private field `_logger` with the logger instance
        passed as a parameter. This allows the `HomeController` class to have access to a logger
        instance for logging purposes throughout its methods and actions. */
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The Index function in C# returns a View result.
        /// </summary>
        /// <returns>
        /// A View is being returned.
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The Login function in C# returns a View for the login page.
        /// </summary>
        /// <returns>
        /// A View result is being returned.
        /// </returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// The Privacy function in C# returns a View for displaying privacy information.
        /// </summary>
        /// <returns>
        /// A View result is being returned.
        /// </returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// The Error function in C# returns a view with an ErrorViewModel containing a RequestId.
        /// </summary>
        /// <returns>
        /// The `Error` method is returning a `View` with an `ErrorViewModel` object as a parameter. The
        /// `ErrorViewModel` object is initialized with a `RequestId` value, which is set to
        /// `Activity.Current?.Id ?? HttpContext.TraceIdentifier`.
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}