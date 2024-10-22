using App.Intefaces.Services;
using App.MappingProfile;
using App.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App
{
    public static class InjectionService
    {
        public static void AddAppReference(this IServiceCollection services)
        {
            #region Configuraciones de servicios

            #region Automapper

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #endregion

            #region Vainas varias

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            #endregion

            #endregion
        }
    }
}
