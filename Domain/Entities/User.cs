using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Common;

namespace Domain.Entities
{
    public class User : BasicAtributtes
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Pfp { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //public ICollection<Post> Posts { get; set; }
    }
}
