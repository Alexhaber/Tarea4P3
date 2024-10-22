using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Entities
{
    public class AppUser : IdentityUser
    {
        //public string CellPhone { get; set; }
        public string Pfp { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
    }
}
