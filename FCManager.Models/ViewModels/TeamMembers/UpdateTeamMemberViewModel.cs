namespace FCManager.Models.ViewModels.TeamMembers
{
    public class UpdateTeamMemberViewModel
    {
        public Guid TeamMemberId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Nationality { get; set; }
        public string? Position { get; set; }
        public Guid TeamId { get; set; }
        public string? Image { get; set; }
        public int GenderId { get; set; }
        public int? PlayerNumber { get; set; }
        public int MemberCategoryId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfSigning { get; set; }
    }
}
