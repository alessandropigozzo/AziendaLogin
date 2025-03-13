using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.Entity
{
    public class Immagine
    {
        [Key]
        public int IdImmagine { get; set; }
        public byte[]? Img1 { get; set; } // Salva l'immagine in formato binario
        public byte[]? Img2 { get; set; }
        public byte[]? Img3 { get; set; }

        public Utente Utente { get; set; }
    }
}
