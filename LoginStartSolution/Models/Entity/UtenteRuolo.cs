using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginStartMenu.Models.Entity
{
    public class UtenteRuolo
    {
        [Key]
        public int IdUtenteRuolo { get; set; }
        [ForeignKey("Utente")] // Chiave esterna per Utente
        public int IdUtente { get; set; }

        [ForeignKey("Ruolo")] // Chiave esterna per Ruolo
        public int IdRuolo { get; set; }
        public Ruolo Ruolo { get; set; }
        public Utente Utente { get; set; }
    }
}
