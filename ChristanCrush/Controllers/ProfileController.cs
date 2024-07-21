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
        /// The Index function is decorated with a CustomAuthorization attribute and returns a View
        /// result.
        /// </summary>
        /// <returns>
        /// A View is being returned.
        /// </returns>
        [CustomAuthorization]
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// The function `Create` in C# asynchronously creates a profile with images and inserts it into
        /// the database.
        /// </summary>
        /// <param name="ProfileModel">ProfileModel is a model class that likely contains properties
        /// related to a user's profile, such as UserId, FullName, and Image1, Image2, and Image3 as
        /// byte arrays for storing images.</param>
        /// <param name="IFormFile">IFormFile is an interface in ASP.NET Core that represents a file
        /// sent with the HttpRequest. It allows you to access the file contents, file name, content
        /// type, and other properties of the uploaded file. In the code snippet you provided, the
        /// method `Create` takes three IFormFile parameters</param>
        /// <param name="IFormFile">IFormFile is an interface provided by ASP.NET Core for handling
        /// files uploaded in HTTP requests. It represents a file sent with the HttpRequest. In the code
        /// snippet you provided, the method `Create` takes three IFormFile parameters named `image1`,
        /// `image2`, and `image3`,</param>
        /// <param name="IFormFile">IFormFile is an interface in ASP.NET Core that represents a file
        /// sent with the HttpRequest. It allows you to access the file contents, file name, content
        /// type, and other properties of the uploaded file. In the provided code snippet, the method
        /// `Create` takes three IFormFile parameters (`</param>
        /// <returns>
        /// The method is returning a `View` with the name "index" and passing the `profile` object as
        /// the model to the view.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProfileModel profile, IFormFile image1, IFormFile image2, IFormFile image3)
        {
            ProfileDAO profileDAO = new ProfileDAO();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            profile.UserId = userId;

            UserDAO userDAO = new UserDAO();

            string email = HttpContext.Session.GetString("email");
            profile.FullName = userDAO.GetUserInfoByEmail(email);

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

            return View("profilesuccess", profile);
        }
    }
}
