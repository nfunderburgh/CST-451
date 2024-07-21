using Microsoft.AspNetCore.Mvc;
using ChristanCrush.Models;
using ChristanCrush.DataServices;
using ChristanCrush.Utility;
using System.Diagnostics;

namespace ChristanCrush.Controllers
{
    public class LoginController : Controller
    {

        public static int userId = 0;


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
        /// The ProcessLogin function in C# checks if the user's email and password are valid, sets the
        /// email in the session if valid, and redirects to different views based on the outcome.
        /// </summary>
        /// <param name="UserModel">The `UserModel` class likely contains properties that represent a
        /// user, such as `email` and possibly `password`. It is used to pass user information between
        /// different parts of the application.</param>
        /// <returns>
        /// Depending on the outcome of the login process, either a view for the Explore page with the
        /// user model data is returned if the login is successful, or a view for the LoginFailure page
        /// with the user model data is returned if the login fails.
        /// </returns>
        public IActionResult ProcessLogin(UserModel user)
        {

            UserDAO userDAO = new UserDAO();

            if (userDAO.FindUserByEmailAndPasswordValid(user))
            {
                HttpContext.Session.SetString("email", user.email);

                userId = userDAO.FindUserIdByEmail(user);
                HttpContext.Session.SetString("userId", userId.ToString());
                //MyLogger.GetInstance().Info("Login Success");
                return View("../Explore/Index", user);
            }
            else
            {

                HttpContext.Session.Remove("email");

                //MyLogger.GetInstance().Info("Login Failure");
                return View("LoginFailure", user);
            }
        }

        /// <summary>
        /// The Logout function clears the session and redirects the user to the Login page.
        /// </summary>
        /// <returns>
        /// The `Logout` method is returning a `RedirectToAction` result that redirects the user to the
        /// "Index" action method of the "Login" controller.
        /// </returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // MyLogger.GetInstance().Info("Logout Success");
            return RedirectToAction("Index", "Login");
        }
    }
}
