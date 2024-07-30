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
            profile = ProfileDao.GetProfileByUserId(userId);


            if (profile != null)
            {
                profile = ProfileDao.GetRandomProfile(userId);
                if (profile == null)
                {
                    return View("NoProfiles");
                }
                else
                {
                    profile.FullName = UserDao.GetUserNameByUserId(profile.UserId);
                    return View(profile);
                }
            }
            else
            {
                return View("CreateProfile");
            }
        }

        [CustomAuthorization]
        public IActionResult DislikeProfile(int profileId)
        {

            Debug.WriteLine("Profile ID" + profileId);
            return RedirectToAction("Index");
        }

        [CustomAuthorization]
        public IActionResult LikeProfile(int profileId)
        {
            LikeDAO likeDao = new LikeDAO();
            ProfileDAO ProfileDao = new ProfileDAO();

            var profile = ProfileDao.GetProfileByProfileId(profileId);
            int LoggedInUserId = int.Parse(HttpContext.Session.GetString("userId"));

            if (!likeDao.CheckIfLikeExists(LoggedInUserId, profile.UserId))
            {
                var like = new LikeModel
                {
                    LikerId = LoggedInUserId,
                    LikedId = profile.UserId,
                    LikedAt = DateTime.Now
                };

                likeDao.InsertLike(like);

                if(likeDao.CheckIfMutualLikeExists(LoggedInUserId, profile.UserId)){

                    UserDAO UserDao = new UserDAO();
                    profile.FullName = UserDao.GetUserNameByUserId(profile.UserId);
                    insertMatch(LoggedInUserId, profile.UserId);
                    TempData["MatchedMessage"] = "You have matched with " + profile.FullName + "!";
                    
                    // TODO:
                    // Delete Likes in database
                }
            }

            return RedirectToAction("Index");
        }

        private void insertMatch(int loggedInUserId, int userId)
        {
            MatchDAO MatchDao = new MatchDAO();
            var match = new MatchModel
            {
                UserId1 = loggedInUserId,
                UserId2 = userId,
                MatchedAt = DateTime.Now
            };

            MatchDao.InsertMatch(match);
        }

        [CustomAuthorization]
        public IActionResult ViewMatches()
        {
            ProfileDAO ProfileDao = new ProfileDAO();
            int userId = int.Parse(HttpContext.Session.GetString("userId"));


            return View("Matches", ProfileDao.GetProfilesMatchedWithUser(userId));
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
