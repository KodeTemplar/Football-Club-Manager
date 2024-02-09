using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCManager.Models.Models
{
    [Table("TeamLogos", Schema = "Team")]
    public class TeamLogo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public Guid LogoId { get; set; }
        public Guid TeamId { get; set; }
        public string? Logo { get; set; }

        public Team Team { get; set; }
    }
}
