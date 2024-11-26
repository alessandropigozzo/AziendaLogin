using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.Entity
{
    public class UtenteRuolo
    {
            [Key]
            public int IdUtenteRuolo { get; set; }

            [ForeignKey("Utente")]
            public int IdUtente { get; set; }

            [ForeignKey("Ruolo")]
            public int IdRuolo { get; set; }

            // Navigazione
            public Utente Utente { get; set; }
            public Ruolo Ruolo { get; set; }
    }
}
