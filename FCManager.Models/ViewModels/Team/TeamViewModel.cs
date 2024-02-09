namespace FCManager.Models.ViewModels.Team
{
    public class TeamViewModel
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Stadium { get; set; }
        public string? HomePageURL { get; set; }
        public string? Image { get; set; }
    }
}
