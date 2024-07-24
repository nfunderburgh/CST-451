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
        /// The ProcessLogin function in C# processes user login by validating credentials and setting
        /// session variables accordingly.
        /// </summary>
        /// <param name="UserModel">UserModel is a class that represents a user in the system and
        /// typically contains properties such as email, password, and other relevant user information.
        /// In the provided code snippet, the ProcessLogin method takes an instance of the UserModel
        /// class as a parameter to handle user login functionality.</param>
        /// <returns>
        /// The ProcessLogin method is returning a View based on the result of the user authentication
        /// process. If the user is successfully authenticated, it returns the Explore/Index view with
        /// the user model data. If the authentication fails, it returns the LoginFailure view with the
        /// user model data.
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
                return RedirectToAction("Index", "Match");
            }
            else
            {

                HttpContext.Session.Remove("email");

                //MyLogger.GetInstance().Info("Login Failure");
                return View("LoginFailure", user);
            }
        }


       /// <summary>
       /// The `Logout` function clears the session and redirects the user to the login page in a C#
       /// ASP.NET application.
       /// </summary>
       /// <returns>
       /// The `Logout` method is returning a `RedirectToAction` result that redirects the user to the
       /// "Index" action of the "Login" controller.
       /// </returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // MyLogger.GetInstance().Info("Logout Success");
            return RedirectToAction("Index", "Login");
        }
    }
}
