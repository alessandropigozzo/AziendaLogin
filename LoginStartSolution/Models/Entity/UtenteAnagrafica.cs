using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.Entity
{
    public class UtenteAnagrafica
    {

        [Key]
        public int IdUtenteAnagrafica { get; set; }

        [ForeignKey("Utente")]
        public int IdUtente { get; set; }

        [ForeignKey("Anagrafica")]
        public int IdAnagrafica { get; set; }

        // Navigazione
        public Utente Utente { get; set; }
        public Anagrafica Anagrafica { get; set; }
    }
}
