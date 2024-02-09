using Microsoft.AspNetCore.Identity;

namespace FCManager.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? SuperAdminName { get; set; }
        public bool IsActive { get; set; }
        public Guid? TeamMemberId { get; set; }

        public TeamMember? TeamMember { get; set; }
    }
}