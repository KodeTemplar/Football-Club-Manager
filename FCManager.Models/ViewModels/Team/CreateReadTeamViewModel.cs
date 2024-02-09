namespace FCManager.Models.ViewModels.Team
{
    public class CreateReadTeamViewModel
    {
        public IEnumerable<FCManager.Models.Models.Team> Teams { get; set; }
        public CreateTeamViewModel CreateTeam { get; set; }
    }
}
