using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginStartMenu.Models.Entity
{
    public class Utente
    {
        
            [Key]
            public int IdUtente { get; set; }

            public string Email { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

            public string Nome { get; set; }

            public string Cognome { get; set; }

            public string CodiceFiscale { get; set; }

            public string RuoliAssegnati { get; set; } 
       
    }

}
