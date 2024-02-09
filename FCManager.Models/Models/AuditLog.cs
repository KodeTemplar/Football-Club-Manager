using Football_Club_Mgt_App.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCManager.Models.Models
{
    [Table("AuditLog", Schema = "Log")]

    public class AuditLog : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public long AuditId { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
    }
}
