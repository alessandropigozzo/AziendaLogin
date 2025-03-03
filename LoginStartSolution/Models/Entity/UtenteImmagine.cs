using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginStartMenu.Models.Entity
{
    public class UtenteImmagine
    {
        [Key]
        public int IdUtenteImmagine { get; set; }

        [ForeignKey("Utente")]
        public int IdUtente { get; set; }

        [ForeignKey("Immagine")]
        public int IdImmagine { get; set; }

        // Navigazione
        public Utente Utente { get; set; }
        public Immagine Immagine { get; set; }
    }
}
