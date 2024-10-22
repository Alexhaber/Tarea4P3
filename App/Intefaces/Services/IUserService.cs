using App.Dtos.Profile;
using App.ViewModels.UserModels;

namespace App.Intefaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel viewM, string origin);
        Task<AuthenticationResponse> LoginAsync(UserLoginViewModel viewM);
        Task LogoutAsync();
        Task<RegistrationResponse> RegisterAsync(UserHandlerViewModel viewM, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel viewM);
    }
}