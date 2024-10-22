using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dtos.Profile
{
    public class RegistrationRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string Pfp { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
