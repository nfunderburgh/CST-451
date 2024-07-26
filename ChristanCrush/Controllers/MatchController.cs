using ChristanCrush.DataServices;
using ChristanCrush.Models;
using ChristanCrush.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;

namespace ChristanCrush.Controllers
{
    public class MatchController : Controller
    {
        
        /// <summary>
        /// The Index function in C# is decorated with a CustomAuthorization attribute and returns a
        /// View result.
        /// </summary>
        /// <returns>
        /// A View is being returned from the Index action method.
        /// </returns>
        [CustomAuthorization]
        public IActionResult Index()
        {
            ProfileModel profile = new ProfileModel();
            ProfileDAO ProfileDao = new ProfileDAO();
            UserDAO UserDao = new UserDAO();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            profile = ProfileDao.GetRandomProfile(userId);

            profile.FullName = UserDao.GetUserInfoByEmail(profile.UserId);

            Debug.WriteLine(profile.Image2Data);
           
            return View(profile);
        }

        public IActionResult DislikeProfile(int profileId)
        {
            // Do Nothing and show next user
            return RedirectToAction("Index");
        }

        public static Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
