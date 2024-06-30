using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using Milestone.Services;
using Milestone.Utility;

namespace Milestone.Controllers
{
    public class RegisterController : Controller
    {

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
        /// The function "ProcessRegister" registers a user and returns a view based on the success or failure of the
        /// registration.
        /// </summary>
        /// <param name="UserModel">UserModel is a class that represents the data structure for a user. It contains
        /// properties such as username, password, email, and other relevant information for user registration.</param>
        /// <returns>
        /// The method is returning an IActionResult. The specific view being returned depends on the result of the
        /// securityDAO.RegisterUserValid(user) method. If it returns true, the "RegisterSuccess" view is returned with the
        /// user model as the parameter. If it returns false, the "RegisterFailure" view is returned with the user model as
        /// the parameter.
        /// </returns>
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult ProcessRegister(UserModel user)
        {
            UserDAO securityDAO = new UserDAO();

            if (securityDAO.RegisterUserValid(user))
            {
                MyLogger.GetInstance().Info("Registration Success");
                return View("RegisterSuccess", user);
            }
            else
            {
                MyLogger.GetInstance().Info("Registration Failed");
                return View("RegisterFailure", user);
            }
        }
    }
}
