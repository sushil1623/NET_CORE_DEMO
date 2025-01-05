using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthorizationMiddleware.RequirementAuthorization
{
    public class DepartmentAuthorizationHandler : AuthorizationHandler<DepartmentRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DepartmentRequirement requirement)
        {
            var roleClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Role);
            var departmentClaim = context.User.FindFirst(c => c.Type == "Department");
            if (roleClaim is null || departmentClaim is null) {
               return Task.CompletedTask;
            }

            if(roleClaim.Value=="Admin" && departmentClaim.Value == requirement.Department)
            {
               context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
