using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.Entity
{
    public class Ruolo
    {
        [Key]
        public int IdRuolo { get; set; }
        public string NomeRuolo { get; set; }

        // Relazione molti a molti con Utente tramite UtenteRuolo
        public ICollection<UtenteRuolo> UtentiRuoli { get; set; }
    }
}
