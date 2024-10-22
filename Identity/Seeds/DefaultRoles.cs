using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using App.Enums;
using Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            await _roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await _roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}
