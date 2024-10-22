using App.Intefaces.Repositories;
using App.Intefaces.Services;
using App.MappingProfile;
using App.Service;
using Database.Contexts;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class InjectionService
    {
        public static void AddDatabseReference(this IServiceCollection services, IConfiguration _configuration)
        {
            #region Configuraciones & servicios

            #region conexion a la base de datos

            if (_configuration.GetValue<bool>("UsInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                var connectStr = _configuration.GetConnectionString("Connection");
                services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(connectStr,
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion



        }
    }
}
