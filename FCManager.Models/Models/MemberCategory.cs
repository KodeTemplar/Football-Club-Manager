using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCManager.Models.Models
{
    [Table("TeamMemberCategories", Schema = "Team")]

    public class MemberCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string TeamMemberCategory { get; set; }
    }
}
