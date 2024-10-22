using App.Enums;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            AppUser _defaultUser = new();
            _defaultUser.UserName = "superadminuser";
            _defaultUser.Email = "superadmin@dummy#12.com";
            _defaultUser.FirstName = "Gary";
            _defaultUser.Lastname = "Stu";
            _defaultUser.Pfp = "Superadmin.png";
            _defaultUser.EmailConfirmed = true;
            _defaultUser.PhoneNumberConfirmed = true;

            if(_userManager.Users.All(u => u.Id != _defaultUser.Id))
            {
                var user = await _userManager.FindByEmailAsync(_defaultUser.Email);
                if(user == null)
                {
                    await _userManager.CreateAsync(_defaultUser, "@Passw0rd");
                    await _userManager.AddToRoleAsync(_defaultUser, Roles.Basic.ToString());
                    await _userManager.AddToRoleAsync(_defaultUser, Roles.Admin.ToString());
                    await _userManager.AddToRoleAsync(_defaultUser, Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}
