using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Milestone.Controllers
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// The function checks if the "username" session variable is set and redirects the user to the login page if it is
        /// not.
        /// </summary>
        /// <param name="AuthorizationFilterContext">The AuthorizationFilterContext is an object that provides information
        /// about the current authorization process and allows you to modify the result of the authorization.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? email = context.HttpContext.Session.GetString("email");

            if (email == null)
            {
                //session "username" variable is not set. Deny access by sending them to the login page
                context.Result = new RedirectResult("/Login");
            }
            else
            { //do nothing. session proceeds.
            }
        }
    }
}
