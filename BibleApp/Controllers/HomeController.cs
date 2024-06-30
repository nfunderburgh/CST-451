using BibleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Milestone.Controllers;
using System.Diagnostics;

namespace BibleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /* The code `public HomeController(ILogger<HomeController> logger)` is a constructor for the HomeController class.
        It takes an ILogger<HomeController> logger parameter and assigns it to the _logger field of the class. This
        allows the HomeController to have access to a logger instance, which can be used to log information, warnings,
        and errors during the execution of the controller's actions. */
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The above function is an action method in a C# controller that returns a view.
        /// </summary>
        /// <returns>
        /// The method is returning a View result.
        /// </returns>
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The Privacy function returns a View and applies a LogControllerEntryExitFilter service filter.
        /// </summary>
        /// <returns>
        /// The method is returning a View result.
        /// </returns>
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// The above function is an error handler in C# that returns a view with an ErrorViewModel object.
        /// </summary>
        /// <returns>
        /// The method is returning a View with an ErrorViewModel object as the model.
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
