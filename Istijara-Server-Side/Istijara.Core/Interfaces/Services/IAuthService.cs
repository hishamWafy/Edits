using Istijara.Core.DTOs;
using Istijara.Core.DTOs.Identity;

namespace Istijara.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(RequestTokenModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<bool> ForgotPasswordAsync(ForgotPasswordModel model);
        Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordModel model);

    }
}
