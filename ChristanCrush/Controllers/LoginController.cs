using Microsoft.AspNetCore.Mvc;
using ChristanCrush.Models;
using ChristanCrush.Services;
using ChristanCrush.Utility;
using System.Diagnostics;

namespace ChristanCrush.Controllers
{
    public class LoginController : Controller
    {

        public static int userId = 0;

        public IActionResult Index()
        {
            PasswordHasher hasher = new PasswordHasher();
            string test = hasher.HashPassword("test");
            Debug.WriteLine(test);

            //bool PasswordTest = hasher.VerifyPassword("test", test);
            //Debug.WriteLine(PasswordTest);

            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            UserDAO userDAO = new UserDAO();

            if (userDAO.FindUserByEmailAndPasswordValid(user))
            {
                //remember user logged in
                HttpContext.Session.SetString("email", user.email);

                userId = userDAO.FindUserIdByEmailAndPassword(user);
                //MyLogger.GetInstance().Info("Login Success");
                return View("LoginSuccess", user);
            }
            else
            {

                HttpContext.Session.Remove("email");

                //MyLogger.GetInstance().Info("Login Failure");
                return View("LoginFailure", user);
            }
        }
    }
}
