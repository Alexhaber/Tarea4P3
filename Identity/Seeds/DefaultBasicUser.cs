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
    public class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            AppUser _defaultUser = new();
            _defaultUser.UserName = "basicuser";
            _defaultUser.Email = "dummy@dummy.com";
            _defaultUser.FirstName = "Jane";
            _defaultUser.Lastname = "Doe";
            _defaultUser.Pfp = "basicuser.png";
            _defaultUser.EmailConfirmed = true;
            _defaultUser.PhoneNumberConfirmed = true;

            if(_userManager.Users.All(u => u.Id != _defaultUser.Id))
            {
                var user = await _userManager.FindByEmailAsync(_defaultUser.Email);
                if(user == null)
                {
                    await _userManager.CreateAsync(_defaultUser, "@Passw0rd");
                    await _userManager.AddToRoleAsync(_defaultUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
