using App.Dtos.Profile;
using App.Enums;
using App.Intefaces.Services;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Text;

namespace Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public ProfileService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        #region Autentificaciones

        #region Login
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay un usuario registrado al nombre {request.UserName}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales invalidas para {request.UserName}";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Cuenta no confirmada para el usuario {request.UserName}";
                return response;
            }


            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = roleList.ToList();
            response.IsVerified = user.EmailConfirmed;


            return response;
        }

        #region logout
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

        #endregion

        #region Manejo de Registro
        public async Task<RegistrationResponse> RegistrateBasicAsync(RegistrationRequest request, string origin)
        {
            RegistrationResponse response = new()
            {
                HasError = false
            };

            var repeatedUser = await _userManager.FindByNameAsync(request.Username);
            if (repeatedUser != null)
            {
                response.HasError = true;
                response.Error = $"{request.Username} ya está en uso";
                return response;
            }

            var repeatedEmail = await _userManager.FindByEmailAsync(request.Email);
            if (repeatedEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo {request.Email} ya está ligado a una cuenta";
                return response;
            }

            var user = new AppUser
            {
                UserName = request.Username,
                FirstName = request.FirstName,
                Lastname = request.Lastname,
                Email = request.Email,
                PhoneNumber = request.CellPhone,
                Pfp = request.Pfp
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                var verificationUrl = await SendVerificationEmailUrl(user, origin);
                await _emailService.SendAsync(new App.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Verifique su cuenta con este link: {verificationUrl}",
                    Subject = "Confirmacion de resgitro"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"Error Window Jumpscare, rraaaaah";
                return response;
            }

            return response;
        }

        //Creacion de la url de verificacion
        private async Task<string> SendVerificationEmailUrl(AppUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "Login/ConfirmEmail";
            var url = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(verificationUrl, "token", code);

            return verificationUrl;
        }
        #endregion

        #region Confirmacion de usuario

        public async Task<string> ConfirmProfileAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No hay cuentas a nombre de este usuario";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return $"Cuenta confirmada con el correo '{user.Email}', Ya tiene acceso a la aplicación";
            }
            else
            {
                return $"Error intentando confirmar la cuenta al correo {user.Email}";
            }
        }

        #endregion

        #region Manejo de la recuperacion de contraseñas


        private string GenerateRandomPassword(int length)
        {
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string numberChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+<>?";

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            // Garantizar al menos una mayúscula, un número y un carácter especial
            password.Append(upperChars[random.Next(upperChars.Length)]);
            password.Append(numberChars[random.Next(numberChars.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            // Completar con caracteres aleatorios
            string allChars = upperChars + lowerChars + numberChars + specialChars;
            for (int i = password.Length; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Mezclar los caracteres para evitar que siempre estén en el mismo orden
            return new string(password.ToString().OrderBy(x => random.Next()).ToArray());
        }

        //Metodo para manejar  la peticion d reseteo
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No se encontro el usuario {request.UserName}";
                return response;
            }

            //var verificationUrl = await SendForgotPasswordlUrl(user, origin);

            //await _emailService.SendAsync(new App.Dtos.Email.EmailRequest()
            //{
            //    To = user.Email,
            //    Body = $"Restablezca su contraseña con esta url: {verificationUrl}",
            //    Subject = "Restablecimiento de contraseña"
            //});

            var newPassword = GenerateRandomPassword(12);

            // Resetear la contraseña del usuario

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "No se pudo resetear la contraseña correctamente";
                return response;
            }

            await _emailService.SendAsync(new App.Dtos.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Su nueva contraseña temporal es: {newPassword}",
                Subject = "Restablecimiento de contraseña"
            });

            return response;
        }

        //Metodo para resetear
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No se encontro una cuenta ligada a {request.UserName}";
                return response;
            }

            request.token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.token));

            var result = await _userManager.ResetPasswordAsync(user, request.token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"La contraseña no pudo resetearse corretamente";

                return response;
            }

            return response;
        }

        //Crea el link para recuperar
        private async Task<string> SendForgotPasswordlUrl(AppUser user, string origin)
        {
            
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Login/ResetPassword";
            var url = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "token", code);

            return verificationUrl;
        }
        #endregion

        #endregion
    }

}
