
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels.UserModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "No puede dejar el correo vacío")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        public bool HasError { get; set; }

        public string? Error { get; set; }
    }
}
