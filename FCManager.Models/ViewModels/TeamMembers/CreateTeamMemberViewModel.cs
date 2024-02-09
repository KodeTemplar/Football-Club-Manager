using System.ComponentModel;

namespace FCManager.Models.ViewModels.TeamMembers
{
    public class CreateTeamMemberViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Nationality { get; set; }
        public string? Position { get; set; }
        public string? Image { get; set; }
        public Guid TeamId { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        public int? GenderId { get; set; }
        public int? PlayerNumber { get; set; }
        public int MemberCategoryId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfSigning { get; set; }
    }
}
