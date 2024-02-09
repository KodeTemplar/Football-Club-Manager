using FCManager.Models.Account;
using FCManager.Models.ViewModels.Account;
using FCManager.Models.Wrappers;

namespace IdentityManager.Services
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> LoginAsync(LoginViewModel request, string ipAddress);
    }
}
