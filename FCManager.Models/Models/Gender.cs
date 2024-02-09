using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCManager.Models.Models
{
    [Table("Genders", Schema = "Setting")]

    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        public string? GenderName { get; set; }
    }
}
