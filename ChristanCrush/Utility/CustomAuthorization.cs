using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChristanCrush.Utility
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        
       /// <summary>
       /// The function checks if a user is authorized by verifying the presence of an email in the
       /// session and redirects to the login page if not.
       /// </summary>
       /// <param name="AuthorizationFilterContext">AuthorizationFilterContext represents the context in
       /// which authorization filters are executed. It provides information about the current request
       /// and allows you to interact with the authorization process. In the provided code snippet, the
       /// OnAuthorization method is checking if a user's email is stored in the session. If the email
       /// is not found,</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? userIdString = context.HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                context.Result = new RedirectResult("/Login");
            }
            else
            {

            }
        }
    }
}
