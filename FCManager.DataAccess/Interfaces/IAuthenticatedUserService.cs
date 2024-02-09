namespace FCManager.DataAccess.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        string RoleId { get; }
        string RoleName { get; }
    }
}
