namespace FCManager.Models.Account
{
    public class AuthenticationResponse
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? RoleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Nationality { get; set; }
        public string? Position { get; set; }
        public int TeamId { get; set; }
        public int GenderId { get; set; }
        public bool IsActive { get; set; }
        public int? PlayerNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfSigning { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string RefreshToken { get; set; }
        public string JWToken { get; set; }
    }
}
