using ChristanCrush.DataServices;
using ChristanCrush.Models;
using ChristanCrush.Services;
using ChristanCrush.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult CreateProfile(ProfileModel profile)
        {
            ProfileDAO profileDAO = new ProfileDAO();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            profile.UserId = userId;
            Debug.WriteLine(profile.Image1);

            if (profileDAO.InsertProfile(profile))
            {
                Debug.WriteLine("Inserted Profile");
            }
            else
            {
                Debug.WriteLine("Fail To Insert Profile");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create(ProfileModel profile, IFormFile image1, IFormFile image2, IFormFile image3)
        {
            ProfileDAO profileDAO = new ProfileDAO();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            profile.UserId = userId;

            UserDAO userDAO = new UserDAO();

            string email = HttpContext.Session.GetString("email");
            profile.FullName = userDAO.GetUserInfoByEmail(email);

            
            // Process image1
            if (image1 != null && image1.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image1.CopyToAsync(memoryStream);
                    profile.Image1 = memoryStream.ToArray();
                }
            }
            else
            {
            profile.Image1 = null;
            }
            if (image2 != null && image2.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image2.CopyToAsync(memoryStream);
                    profile.Image2 = memoryStream.ToArray();
                }
            }
            else
            {
                profile.Image2 = null;
            }

                
            if (image3 != null && image3.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image3.CopyToAsync(memoryStream);
                        profile.Image3 = memoryStream.ToArray();
                    }
            }
            else
            {
                profile.Image3 = null;
            }

            if (profileDAO.InsertProfile(profile))
            {
                Debug.WriteLine("Inserted Profile");
            }
            else
            {
                Debug.WriteLine("Fail To Insert Profile");
            }

            return View("index", profile);
        }
    }
}
