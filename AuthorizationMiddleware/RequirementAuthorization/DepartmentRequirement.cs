using Microsoft.AspNetCore.Authorization;

namespace AuthorizationMiddleware.RequirementAuthorization
{
    public class DepartmentRequirement:IAuthorizationRequirement
    {
        public string Department;
        public DepartmentRequirement(string department)
        {
            Department= department;
        }
    }
}
