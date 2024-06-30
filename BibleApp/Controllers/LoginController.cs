using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;
using Milestone.Utility;
using NLog;


namespace Milestone.Controllers
{
    public class LoginController : Controller
    {
        public static int userId = 0;

        /// <summary>
        /// The function returns a view for the Index page.
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
        /// The ProcessLogin function checks if the user's email and password are valid, and if so, logs the user in and
        /// returns a view indicating success, otherwise it returns a view indicating failure.
        /// </summary>
        /// <param name="UserModel">UserModel is a model class that represents a user. It contains properties such as Email
        /// and Password, which are used to authenticate the user during the login process.</param>
        /// <returns>
        /// The method is returning an IActionResult. The specific view being returned depends on the outcome of the login
        /// process. If the login is successful, the method returns the "LoginSuccess" view with the user model as the
        /// parameter. If the login fails, the method returns the "LoginFailure" view with the user model as the parameter.
        /// </returns>
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult ProcessLogin(UserModel user)
        {
            UserDAO userDAO = new UserDAO();

            if (userDAO.FindUserByEmailAndPasswordValid(user))
            {
                //remember user logged in
                HttpContext.Session.SetString("email", user.Email);

                userId = userDAO.FindUserIdByEmailAndPassword(user);
                MyLogger.GetInstance().Info("Login Success");
                return View("LoginSuccess", user);
            }
            else
            {
                
                HttpContext.Session.Remove("email");

                MyLogger.GetInstance().Info("Login Failure");
                return View("LoginFailure", user);
            }
        }
    }
}
