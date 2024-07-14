using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class ProfileController : Controller
    {
        /// <summary>
        /// The Index function in C# is decorated with a CustomAuthorization attribute and returns a
        /// View result.
        /// </summary>
        /// <returns>
        /// In the provided code snippet, the `Index` method is returning a `ViewResult`. This means
        /// that when the method is called, it will render a view and return the rendered view to the
        /// client.
        /// </returns>
        [CustomAuthorization]
        public IActionResult Index()
        {
            return View();
        }
    }
}
