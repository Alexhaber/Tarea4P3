using App.Intefaces.Services;
using App.MappingProfile;
using App.Service;
using Identity.Context;
using Identity.Entities;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
    public static class InjectionService
    {
        public static void AddIdentityReference(this IServiceCollection services, IConfiguration _configuration)
        {
            #region Databse

            if (_configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                //var connectStr = _configuration.GetConnectionString("Connection");

                
                services.AddDbContext<IdentityContext>(opt => 
                {
                    opt.EnableSensitiveDataLogging();
                    opt.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"),
                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }

            #endregion

            #region Identity
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Login/"/*ForbiddenAcess*/;
                options.AccessDeniedPath = "/Login/ForbiddenAcess";
            });

            services.AddAuthentication();
            #endregion

            #region Service
            services.AddTransient<IProfileService, ProfileService>();
            #endregion
        }
    }
}
