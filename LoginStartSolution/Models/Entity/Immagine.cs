using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.Entity
{
    public class Immagine
    {
            [Key]
            public int IdImmagine { get; set; }
            public string? Img1 { get; set; } 
            public string? Img2 { get; set; }
            public string? Img3 { get; set; }

            public ICollection<UtenteImmagine> UtentiImmagini { get; set; }


        // Metodo per convertire un array di byte in Base64
        public static string ConvertiInBase64(byte[] imageBytes)
            {
                return Convert.ToBase64String(imageBytes);
            }

            // Metodo per convertire una stringa Base64 in un array di byte
            public static byte[] ConvertiDaBase64(string base64String)
            {
                return Convert.FromBase64String(base64String);
            }

    }
}
