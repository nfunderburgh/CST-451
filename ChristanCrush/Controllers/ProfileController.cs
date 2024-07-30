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

            ProfileDAO profileDao = new ProfileDAO();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            ProfileModel profile = new ProfileModel();
            profile = profileDao.GetProfileByUserId(userId);
            
            return View(profile);
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
        public async Task<IActionResult> CreateProfile(ProfileModel profile)
        {
            ProfileDAO profileDAO = new ProfileDAO();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            profile.UserId = userId;

            UserDAO userDAO = new UserDAO();
            profile.FullName = userDAO.GetUserNameByUserId(userId);

            // Process images
            profile.Image1Data = await ProcessFile(profile.Image1);
            profile.Image2Data = await ProcessFile(profile.Image2);
            profile.Image3Data = await ProcessFile(profile.Image3);

            if (profile.Image1Data == null)
            {
                return View("index", profile);
            }
            else
            {
                if (profileDAO.InsertProfile(profile))
                {
                    Debug.WriteLine("Inserted Profile");
                    return View("index", profile);
                }
                else
                {
                    Debug.WriteLine("Fail To Insert Profile");
                    return View("index", profile);
                }
            }
            
        }

        private async Task<byte[]> ProcessFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null;
        }
    }
}
