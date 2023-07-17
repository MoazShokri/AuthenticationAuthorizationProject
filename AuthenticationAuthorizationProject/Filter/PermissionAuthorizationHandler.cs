using Microsoft.AspNetCore.Authorization;

namespace AuthenticationAuthorizationProject.Filter
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        public PermissionAuthorizationHandler()
        {

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // TODO : Check Exist User Login

            if (context.User == null)
            {
                return;
            }
            var canAccess = context.User.Claims.Any(c => c.Type == "Permissions" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");
            if (canAccess)
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
