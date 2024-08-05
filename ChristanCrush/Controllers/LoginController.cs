using Microsoft.AspNetCore.Mvc;
using ChristanCrush.Models;
using ChristanCrush.DataServices;
using ChristanCrush.Utility;
using System.Diagnostics;
using ChristanCrush.Services;

namespace ChristanCrush.Controllers
{
    public class LoginController : Controller
    {
        private UserService userService = new UserService();
        private static int userId = 0;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {

            if (userService.FindUserByEmailAndPasswordValid(user))
            {
                userId = userService.FindUserIdByEmail(user);
                HttpContext.Session.SetString("userId", userId.ToString());

                int userid = int.Parse(HttpContext.Session.GetString("userId"));
                Debug.Write("userid" + userid);

                return RedirectToAction("Index", "Match");
            }
            else
            {
                HttpContext.Session.Remove("userId");

                return View("LoginFailure", user);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}
