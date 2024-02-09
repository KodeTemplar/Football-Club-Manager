using System.ComponentModel.DataAnnotations;

namespace FCManager.Models.ViewModels.Team
{
    public class CreateTeamViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string? Stadium { get; set; }
        public string? HomePageURL { get; set; }
        public string? Image { get; set; }
    }
}
