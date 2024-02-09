using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCManager.Models.Models
{
    [Table("TeamMemberImages", Schema = "Team")]
    public class TeamMemberImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public Guid ImageId { get; set; }
        public Guid TeamMemberId { get; set; }
        public string? Image { get; set; }

        public TeamMember TeamMember { get; set; }
    }
}