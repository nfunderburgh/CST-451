using BibleApp.Models;
using BibleApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Milestone.Controllers;
using System.Diagnostics;

namespace BibleApp.Controllers
{
    public class BibleController : Controller
    {
        BibleDAO repository = new BibleDAO();

        /* The `public BibleController()` is a constructor for the `BibleController` class. It initializes a new instance
        of the `BibleDAO` class and assigns it to the `repository` variable. This allows the controller to have access
        to the methods and data in the `BibleDAO` class. */
        public BibleController()
        {
            repository = new BibleDAO();
        }


        /// <summary>
        /// The above function is decorated with custom authorization and a log controller entry/exit filter, and it returns
        /// a view.
        /// </summary>
        /// <returns>
        /// The method is returning a View result.
        /// </returns>
        [CustomAuthorization]
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// The SearchForm function is decorated with custom authorization and a log controller entry/exit filter, and it
        /// returns a view.
        /// </summary>
        /// <returns>
        /// The method is returning a View.
        /// </returns>
        [CustomAuthorization]
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult SearchForm()
        {
            return View();
        }


        /// <summary>
        /// The function `SearchResults` takes in a search term and a selected testament, performs a search for verses based
        /// on the selected testament, and returns the results to the Index view.
        /// </summary>
        /// <param name="searchTerm">The `searchTerm` parameter is a string that represents the term or keyword that the
        /// user wants to search for in the Bible verses.</param>
        /// <param name="selectTestament">The `selectTestament` parameter is a string that represents the selected testament
        /// in a search operation. It can have one of the following values:</param>
        /// <returns>
        /// The method is returning a View named "Index" with the verseList as the model.
        /// </returns>
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult SearchResults(string searchTerm, string selectTestament)
        {
            List<BibleModel> verseList = null;

            int userid = LoginController.userId;
            ViewBag.CurrentUserId = userid;

            HttpContext.Session.SetString("SearchTerm", searchTerm);
            HttpContext.Session.SetString("SelectTestament", selectTestament);


            switch (selectTestament)
            {
                case "Both":
                    verseList = repository.SearchVersesBoth(searchTerm);
                    break;
                case "Old Testament":
                    verseList = repository.SearchVersesOld(searchTerm);
                    break;
                case "New Testament":
                    verseList = repository.SearchVersesNew(searchTerm);
                    break;
                default:
                    verseList = repository.SearchVersesBoth(searchTerm);
                    break;
            }
            return View("Index", verseList);
        }

        /// <summary>
        /// The `FavoriteVerse` function adds a verse to the user's favorite list and returns a view with a list of verses
        /// based on the search term and selected testament.
        /// </summary>
        /// <param name="id">The `id` parameter in the `FavoriteVerse` method represents the unique identifier of a verse.
        /// It is used to add the verse to the user's favorite list.</param>
        /// <returns>
        /// The method is returning a View named "Index" with the verseList as the model.
        /// </returns>
        [CustomAuthorization]
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public ActionResult FavoriteVerse(int id)
        {
            List<BibleModel> verseList = null;

            repository.AddFavoriteVerse(LoginController.userId, id);

            int userid = LoginController.userId;
            ViewBag.CurrentUserId = userid;

            var searchTerm = HttpContext.Session.GetString("SearchTerm");
            var selectTestament = HttpContext.Session.GetString("SelectTestament");

            switch (selectTestament)
            {
                case "Both":
                    verseList = repository.SearchVersesBoth(searchTerm);
                    break;
                case "Old Testament":
                    verseList = repository.SearchVersesOld(searchTerm);
                    break;
                case "New Testament":
                    verseList = repository.SearchVersesNew(searchTerm);
                    break;
                default:
                    verseList = repository.SearchVersesBoth(searchTerm);
                    break;
            }
            return View("Index", verseList);
        }

        /// <summary>
        /// The UnfavoriteVerse function removes a verse from the user's favorites list and returns the updated list of
        /// verses based on the search term and selected testament.
        /// </summary>
        /// <param name="id">The "id" parameter in the UnfavoriteVerse method is an integer that represents the ID of the
        /// verse that needs to be unfavorited.</param>
        /// <returns>
        /// The method is returning a View named "Index" with the verseList as the model.
        /// </returns>
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public ActionResult UnfavoriteVerse(int id)
        {
            List<BibleModel> verseList = null;

            repository.UnfavoriteVerse(LoginController.userId, id);

            int userid = LoginController.userId;
            ViewBag.CurrentUserId = userid;

            var searchTerm = HttpContext.Session.GetString("SearchTerm");
            var selectTestament = HttpContext.Session.GetString("SelectTestament");

            switch (selectTestament)
            {
                case "Both":
                    verseList = repository.SearchVersesBoth(searchTerm);
                    break;
                case "Old Testament":
                    verseList = repository.SearchVersesOld(searchTerm);
                    break;
                case "New Testament":
                    verseList = repository.SearchVersesNew(searchTerm);
                    break;
                default:
                    verseList = repository.SearchVersesBoth(searchTerm);
                    break;
            }
            return View("Index", verseList);
        }

        /// <summary>
        /// The function "FavoritesForm" retrieves a list of favorite Bible verses for a specific user and displays them on
        /// the "Index" view.
        /// </summary>
        /// <returns>
        /// The method is returning a View named "Index" with a list of BibleModel objects named "verseList".
        /// </returns>
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult FavoritesForm()
        {
            List<BibleModel> verseList = null;

            int userid = LoginController.userId;
            ViewBag.CurrentUserId = userid;

            verseList = repository.DisplayFavoriteVerses(LoginController.userId);

            return View("Index", verseList);
        }

        /// <summary>
        /// The function checks if a verse is favorited by a user.
        /// </summary>
        /// <param name="verseid">The verseid parameter is an integer that represents the ID of a specific verse.</param>
        /// <param name="userid">The `userid` parameter represents the unique identifier of the user for whom we want to
        /// check if a verse is favorited.</param>
        /// <returns>
        /// The method is returning a Boolean value, which indicates whether a verse is favorited or not.
        /// </returns>
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public Boolean CheckIfVerseIsFavorited(int verseid, int userid)
        {
            Boolean Favorited = repository.CheckIfFavorited(verseid, userid);

            return Favorited;
        }

        /// <summary>
        /// The above function returns the verse of the day by retrieving a random verse from the repository and passing it
        /// to the "Index" view.
        /// </summary>
        /// <returns>
        /// The method is returning a View with the name "Index" and passing a list of BibleModel objects called "verseList"
        /// as the model for the view.
        /// </returns>
        [CustomAuthorization]
        [ServiceFilter(typeof(LogControllerEntryExitFilter))]
        public IActionResult VerseOfTheDay()
        {
            List<BibleModel> verseList = null;

            int userid = LoginController.userId;
            ViewBag.CurrentUserId = userid;

            verseList = repository.RandomVerse();

            return View("Index", verseList);
        }
    }
}
