
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels.UserModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "No puede dejar el correo vacío")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Requiere de un token")]
        [DataType(DataType.Text)]
        public string token { get; set; }

        [Required(ErrorMessage = "La contraseña no se puede dejar vacía")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Tiene que confirmar la contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool HasError { get; set; }

        public string? Error { get; set; }
    }
}
