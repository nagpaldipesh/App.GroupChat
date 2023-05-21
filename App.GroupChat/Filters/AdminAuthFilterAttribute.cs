using App.GroupChat.Api.Exceptions;
using App.GroupData.Shared;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace App.GroupChat.Api.Filters {
    public class AdminAuthFilterAttribute: ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {
            
            var role = Convert.ToInt32(context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role)).Value);
            if (role != null && (role.Equals((int)UserRoles.Admin) || role.Equals((int)UserRoles.SuperAdmin))) {
                base.OnActionExecuting(context);
                return;
            }
            throw new ForbiddenException("You do not have access to the resources");
        }
    }
}
