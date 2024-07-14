using ChristanCrush.Models;
using ChristanCrush.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChristanCrush.Controllers
{
    public class RegisterController : Controller
    {
        /// <summary>
        /// The above function is an action method in a C# controller that returns a view.
        /// </summary>
        /// <returns>
        /// The method is returning a View result.
        /// </returns>
        //[ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult Index()
        {
            return View();
        }

        

        /// <summary>
        /// The ProcessRegister function in C# checks if a user registration is valid and returns a
        /// success or failure view accordingly.
        /// </summary>
        /// <param name="UserModel">A model class that represents a user with properties such as
        /// username, password, email, etc.</param>
        /// <returns>
        /// The ProcessRegister method is returning a View based on the result of the RegisterUserValid
        /// method from the UserDAO class. If the RegisterUserValid method returns true, the method
        /// returns a View called "RegisterSuccess" with the user model data. If the RegisterUserValid
        /// method returns false, the method returns a View called "RegisterFailure" with the user model
        /// data.
        /// </returns>
        //[ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult ProcessRegister(UserModel user)
        {
            UserDAO securityDAO = new UserDAO();

            if (securityDAO.RegisterUserValid(user))
            {
                //MyLogger.GetInstance().Info("Registration Success");
                return View("RegisterSuccess", user);
            }
            else
            {
                //MyLogger.GetInstance().Info("Registration Failed");
                return View("RegisterFailure", user);
            }
        }
    }
}
