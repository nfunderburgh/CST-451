using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Milestone.Models;
using Milestone.Utility;

namespace Milestone.Controllers
{
    public class LogControllerEntryExitFilter : Attribute, IActionFilter
    {
        /// <summary>
        /// The function logs the entry of a specific action in the application.
        /// </summary>
        /// <param name="ActionExecutingContext">The ActionExecutingContext is an object that contains information about the
        /// current action being executed, including the action descriptor, controller, and route data. It provides access
        /// to the HttpContext, which contains information about the current HTTP request and response.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            MyLogger.GetInstance().Info($"Entering {context.ActionDescriptor.DisplayName}");
        }

        /// <summary>
        /// The function logs the exit of an action with the action's display name.
        /// </summary>
        /// <param name="ActionExecutedContext">The ActionExecutedContext is an object that provides information about the
        /// action that was executed, including the result of the action and any exceptions that occurred during execution.
        /// It also provides access to the HttpContext, which contains information about the current HTTP request and
        /// response.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            MyLogger.GetInstance().Info($"Exiting {context.ActionDescriptor.DisplayName}");
        }
    }
}
