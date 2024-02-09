using Football_Club_Mgt_App.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCManager.Models.Models
{
    [Table("Teams", Schema = "Team")]
    public class Team : BaseEntity
    {
        public Team()
        {
            TeamMembers = new HashSet<TeamMember>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Stadium { get; set; }
        public string HomePageURL { get; set; }
        public TeamLogo TeamLogo { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
    }
}
