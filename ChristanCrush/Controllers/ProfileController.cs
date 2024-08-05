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
        private UserService userService = new UserService();
        private ProfileService profileService = new ProfileService();

        [CustomAuthorization]
        public IActionResult Index()
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            ProfileModel profile = new ProfileModel();
            profile = profileService.GetProfileByUserId(userId);
            
            return View(profile);
        }

        public async Task<IActionResult> CreateProfile(ProfileModel profile)
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            profile.UserId = userId;
            profile.FullName = userService.GetUserNameByUserId(userId);

            profile.Image1Data = await ProcessFile(profile.Image1);
            profile.Image2Data = await ProcessFile(profile.Image2);
            profile.Image3Data = await ProcessFile(profile.Image3);

            if (profile.Image1Data == null)
            {
                return View("index", profile);
            }
            else
            {
                if (profileService.InsertProfile(profile))
                {
                    Debug.WriteLine("Inserted Profile");
                    return View("ProfileSuccess", profile);
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
