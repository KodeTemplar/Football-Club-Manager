namespace Football_Club_Mgt_App.Dtos
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
    }
}
