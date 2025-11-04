
using LoginStartMenu.Models.Entity;

namespace LoginStartMenu.Models.Collection
{
    public class AccessoUtente
    {
        public AccessoUtente() { }
        public Utente utente { get; set; }
        public Ruolo ruolo { get; set; }
        public Immagine immagine { get; set; }

    }
}
