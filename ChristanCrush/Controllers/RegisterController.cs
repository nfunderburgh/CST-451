using ChristanCrush.Models;
using Microsoft.AspNetCore.Mvc;
using ChristanCrush.Services;

namespace ChristanCrush.Controllers
{
    public class RegisterController : Controller
    {
        private UserService userService = new UserService();

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ProcessRegister(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (userService.RegisterUserValid(user))
                {
                    return View("RegisterSuccess", user);
                }
                else
                {
                    return View("RegisterFailure", user);
                }
            }
            return View("index", user);
        }
    }
}
