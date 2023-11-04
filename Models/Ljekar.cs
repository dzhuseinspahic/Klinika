using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Ljekar
    {
        public int ID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Ime { get; set; } = string.Empty;
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Prezime { get; set; } = string.Empty;
        public Titula? Titula { get; set; }
        public string Sifra { get; set; } = string.Empty;

        public ICollection<Prijem> Prijemi { get; set; }
    }
}
