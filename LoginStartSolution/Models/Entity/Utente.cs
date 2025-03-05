using System.ComponentModel.DataAnnotations;

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

        // Relazioni
        public int IdAnagrafica { get; set; }
        public Anagrafica Anagrafica { get; set; }  // Relazione uno a uno con Anagrafica

        public int IdImmagine { get; set; }
        public Immagine Immagine { get; set; }  // Relazione uno a uno con Immagine

        public ICollection<UtenteRuolo> UtentiRuoli { get; set; }  // Relazione molti a molti con Ruolo tramite UtenteRuolo
    }
}
