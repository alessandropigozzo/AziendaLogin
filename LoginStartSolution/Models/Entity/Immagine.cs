using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.Entity
{
    public class Immagine
    {
        [Key]
        public int IdImmagine { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }

        public Utente Utente { get; set; }
    }
}
