using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Ljekar
    {
        public int ID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Ime { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Prezime { get; set; }
        public Titula Titula { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Sifra { get; set; }

        public ICollection<Prijem> Prijemi { get; set; }
    }
}
