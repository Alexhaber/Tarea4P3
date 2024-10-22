using App.Intefaces.Repositories;
using App.Intefaces.Services;
using App.ViewModels.UserModels;
using AutoMapper;
using Domain.Entities;
using App.Dtos.Email;
using App.Dtos.Profile;

namespace App.Service
{
    public class UserService : /*GenericService<User, UserHandlerViewModel>,*/ IUserService
    {
        private readonly IMapper _mapper;

        private readonly IProfileService _profileService;
        //private readonly IEmailService _emailService;

        public UserService(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        #region Login y logout
        public async Task<AuthenticationResponse> LoginAsync(UserLoginViewModel viewM)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(viewM);
            AuthenticationResponse userResponse = await _profileService.AuthenticateAsync(loginRequest);
            return userResponse;
        }

        //Logout
        public async Task LogoutAsync()
        {
            await _profileService.LogoutAsync();
        }
        #endregion

        #region Manejo de registro
        public async Task<RegistrationResponse> RegisterAsync(UserHandlerViewModel viewM, string origin)
        {
            RegistrationRequest registerRequest = _mapper.Map<RegistrationRequest>(viewM);
            return await _profileService.RegistrateBasicAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _profileService.ConfirmProfileAsync(userId, token);
        }
        #endregion

        #region Manejo de recuperacion de contraseñas
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel viewM, string origin)
        {
            ForgotPasswordRequest passwordRequest = _mapper.Map<ForgotPasswordRequest>(viewM);
            return await _profileService.ForgotPasswordAsync(passwordRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel viewM)
        {
            ResetPasswordRequest resetPassRequest = _mapper.Map<ResetPasswordRequest>(viewM);
            return await _profileService.ResetPasswordAsync(resetPassRequest);
        }
        #endregion


    }
}
    

