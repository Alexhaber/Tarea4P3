using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels.UserModels
{
    public class UserHandlerViewModel
    {
        //public string Id { get; set; }

        [Required(ErrorMessage = "No ha introducido un nombre de ussuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "No ha introducido su nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "No ha introducido su apellido")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "No ha introducido su correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No ha introducido su teléfono")]
        public string Cellphone { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un foto de perfil")]
        public string Pfp { get; set; }

        [Required(ErrorMessage = "La contraseña no se puede dejar vacía")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Tiene que confirmar la contraseña")]
        public string ConfirmPassword { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
