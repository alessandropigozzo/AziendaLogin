using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.LoginModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lo username è obbligatorio.")]
        [StringLength(10, ErrorMessage = "Lo username non può superare i 10 caratteri.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
