using App.Intefaces.Services;
using App.MappingProfile;
using App.Service;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Settings;
using Shared.Services;


namespace Shared
{
    public static class InjectionService
    {
        public static void AddSharedLayer(this IServiceCollection services, IConfiguration _configuration)
        {
            #region Configuraciones de servicios

            services.Configure<MailSettings>(_configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            #endregion
        }
    }
}

