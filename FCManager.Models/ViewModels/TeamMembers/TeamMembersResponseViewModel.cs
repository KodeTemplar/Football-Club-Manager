namespace FCManager.Models.ViewModels.TeamMembers
{
    public class TeamMembersResponseViewModel
    {
        public FCManager.Models.Models.Team TeamModel { get; set; }
        public IEnumerable<TeamMembersViewModel> TeamMembers { get; set; }
        public CreateTeamMemberViewModel CreateTeamMember { get; set; }
    }
}
