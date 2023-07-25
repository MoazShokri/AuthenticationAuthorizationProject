using Microsoft.AspNetCore.Authorization;

namespace AuthenticationAuthorizationProject.Filter
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {

        }
        //protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        //{
        //    // TODO : Check Exist User Login

        //    if (context.User == null)
        //    {
        //        return;
        //    }
        //    var canAccess = context.User.Claims.Any(c => c.Type == "Permissions" && c.Value == requirement.Permission && c.Issuer == "SecureApi");
        //    if (canAccess)
        //    {
        //        context.Succeed(requirement);
        //        return;
        //    }
        //}
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // TODO: Check Exist User Login

            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                return;
            }

            // Check if the user has the required permission claim
            var canAccess = context.User.Claims.Any(c => c.Type == "Permissions" && c.Value == requirement.Permission);

            if (canAccess)
            {
                context.Succeed(requirement);
            }
        }
    }
}
