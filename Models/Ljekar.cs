using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Ljekar
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [StringLength(30, MinimumLength = 2)]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [StringLength(30, MinimumLength = 2)]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        public Titula Titula { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Šifra")]
        public string Sifra { get; set; }

        public ICollection<Prijem>? Prijemi { get; set; }
    }
}
