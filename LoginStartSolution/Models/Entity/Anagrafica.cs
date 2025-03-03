using System.ComponentModel.DataAnnotations;

namespace LoginStartMenu.Models.Entity
{
    public class Anagrafica
    {

        [Key]
        public int IdAnagrafica { get; set; }
        public string Nazionalita { get; set; }
        public int Eta { get; set; }
        public string Via { get; set; }
        public string Indirizzo { get; set; }
        public int Cap { get; set; }


        public ICollection<UtenteAnagrafica> UtentiAnagrafiche { get; set; }



    }
}
