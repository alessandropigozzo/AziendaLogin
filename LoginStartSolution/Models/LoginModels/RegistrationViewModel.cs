using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.LoginModels
{
    public class RegistrationViewModel
    {
        [Key]
        public int IdPersona { get; set; }
        
        [Required(ErrorMessage = "L'email è obbligatoria.")]
        [EmailAddress(ErrorMessage = "Formato email non valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lo username è obbligatorio.")]
        [StringLength(10, ErrorMessage = "Lo username non può superare i 10 caratteri.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il codice fiscale deve essere di 16 caratteri.")]
        [RegularExpression("^[A-Z0-9]+$", ErrorMessage = "Il codice fiscale deve contenere solo lettere maiuscole e numeri.")]
        public string CodiceFiscale { get; set; }

    }
}
