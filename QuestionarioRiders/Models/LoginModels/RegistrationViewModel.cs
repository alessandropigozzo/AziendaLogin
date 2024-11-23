using System.ComponentModel.DataAnnotations;

namespace QuestionarioRiders.Models.LoginModels
{
    public class RegistrationViewModel
    {
        [Key]
        public int IdPersona { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your username.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Please enter your name.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Please enter your surname.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Please enter your tax-number.")]
        public string CodiceFiscale { get; set; }

    }
}
