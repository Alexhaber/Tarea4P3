using System.ComponentModel.DataAnnotations;

namespace App.ViewModels.UserModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "No puede dejar el nombre de usuario vacío")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Introduzca una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
