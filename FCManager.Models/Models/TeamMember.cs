using Football_Club_Mgt_App.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCManager.Models.Models
{
    [Table("TeamMembers", Schema = "Team")]
    public class TeamMember : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public Guid TeamMemberId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Nationality { get; set; }
        public string? Position { get; set; }
        public Guid TeamId { get; set; }
        public int GenderId { get; set; }
        public int? PlayerNumber { get; set; }
        public int MemberCategoryId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfSigning { get; set; }


        public Team Team { get; set; }
        public MemberCategory MemberCategory { get; set; }
        public TeamMemberImage TeamMemberImage { get; set; }

    }
}
