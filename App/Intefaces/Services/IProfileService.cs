using App.Dtos.Profile;

namespace App.Intefaces.Services
{
    public interface IProfileService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmProfileAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task LogoutAsync();
        Task<RegistrationResponse> RegistrateBasicAsync(RegistrationRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request/*, string origin*/);
    }
}