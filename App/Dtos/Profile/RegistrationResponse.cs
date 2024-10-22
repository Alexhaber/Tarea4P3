using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dtos.Profile
{
    public class RegistrationResponse
    {
        public bool HasError {get; set; }
        public string? Error { get; set; }
    }
}
