using FCManager.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FCManager.DataAccess.Repository
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue("email");
            FirstName = httpContextAccessor.HttpContext?.User?.FindFirstValue("firstname");
            LastName = httpContextAccessor.HttpContext?.User?.FindFirstValue("lastname");
            RoleId = httpContextAccessor.HttpContext?.User?.FindFirstValue("roleid");
            RoleName = httpContextAccessor.HttpContext?.User?.FindFirstValue("rolename");
        }

        public string UserId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string RoleId { get; }
        public string RoleName { get; }
    }
}
