using ChristanCrush.Models;
using ChristanCrush.DataServices;
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
        /// The ProcessRegister function in C# processes user registration by validating the user and
        /// redirecting to appropriate views based on the registration outcome.
        /// </summary>
        /// <param name="UserModel">A model class representing a user with properties such as username,
        /// password, email, etc. It is used to pass user data between the view and the controller in an
        /// ASP.NET MVC application.</param>
        /// <returns>
        /// The ProcessRegister method returns a View based on the validation and registration status of
        /// the user. If the ModelState is valid and the user registration is successful, it returns the
        /// "RegisterSuccess" view with the user model. If the registration fails, it returns the
        /// "RegisterFailure" view with the user model. If the ModelState is not valid, it returns the
        /// "index" view with the user model.
        /// </returns>
        public IActionResult ProcessRegister(UserModel user)
        {
            UserDAO securityDAO = new UserDAO();
            if (ModelState.IsValid)
            {
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
            return View("index", user);
        }
    }
}
