using ChristanCrush.DataServices;
using ChristanCrush.Models;
using ChristanCrush.Services;
using ChristanCrush.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;

namespace ChristanCrush.Controllers
{
    public class MatchController : Controller
    {
        UserService userService = new UserService();
        ProfileService profileService = new ProfileService();
        LikeService likeService = new LikeService();
        MatchService matchService = new MatchService();

        [CustomAuthorization]
        public IActionResult Index()
        {
            ProfileModel profile = new ProfileModel();

            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            profile = profileService.GetProfileByUserId(userId);


            if (profile != null)
            {
                profile = profileService.GetRandomProfile(userId);
                if (profile == null)
                {
                    return View("NoProfiles");
                }
                else
                {
                    profile.FullName = userService.GetUserNameByUserId(profile.UserId);
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

            var profile = profileService.GetProfileByProfileId(profileId);
            int LoggedInUserId = int.Parse(HttpContext.Session.GetString("userId"));

            if (!likeService.CheckIfLikeExists(LoggedInUserId, profile.UserId))
            {
                var like = new LikeModel
                {
                    LikerId = LoggedInUserId,
                    LikedId = profile.UserId,
                    LikedAt = DateTime.Now
                };

                likeService.InsertLike(like);

                if(likeService.CheckIfMutualLikeExists(LoggedInUserId, profile.UserId)){

                    profile.FullName = userService.GetUserNameByUserId(profile.UserId);
                    insertMatch(LoggedInUserId, profile.UserId);
                    TempData["MatchedMessage"] = "You have matched with " + profile.FullName + "!";
                    
                    
                }
            }

            return RedirectToAction("Index");
        }

        [CustomAuthorization]
        private void insertMatch(int loggedInUserId, int userId)
        {
            MatchDAO MatchDao = new MatchDAO();
            var match = new MatchModel
            {
                UserId1 = loggedInUserId,
                UserId2 = userId,
                MatchedAt = DateTime.Now
            };

            matchService.InsertMatch(match);
        }

        [CustomAuthorization]
        public IActionResult ViewMatches()
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            return View("Matches", profileService.GetProfilesMatchedWithUser(userId));
        }


        [CustomAuthorization]
        public IActionResult deleteMatch(int matchUserId)
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            MatchDAO MatchDao = new MatchDAO();
            var match = matchService.GetMatch(userId, matchUserId);

            matchService.DeleteMatchById(match.MatchId);

            return View("Matches", profileService.GetProfilesMatchedWithUser(userId));
        }

        [CustomAuthorization]
        public static Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
