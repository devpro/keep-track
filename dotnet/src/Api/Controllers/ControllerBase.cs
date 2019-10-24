using System;
using System.Linq;

namespace KeepTrack.Api.Controllers
{
    /// <summary>
    /// Base controller for the web application.
    /// </summary>
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        /// <summary>
        /// Get authenticated user id.
        /// </summary>
        /// <returns></returns>
        protected string GetUserId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }

            return userId;
        }
    }
}
