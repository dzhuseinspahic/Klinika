using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Pacijent
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [StringLength(30, MinimumLength = 2)]
        public string Ime { get; set; } 
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [StringLength(30, MinimumLength = 2)]
        public string Prezime { get; set; } 
        public Spol Spol { get; set; } = Spol.Nepoznato;
        public string? Adresa { get; set; }
        [Display(Name = " Broj telefona")]
        [RegularExpression(@"^[0-9\\s]*$", ErrorMessage = "Broj telefona smije sadržavati smo brojeve i razmake.")]
        public string? BrojTelefona { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [Display(Name = "Broj zdravstvene knjižice")]
        [StringLength (8, MinimumLength = 8, ErrorMessage = "Ovo polje mora sadržavati 8 karaktera.")]
        public string BrojZdravstveneKnjizice { get; set; }

        public ICollection<Prijem>? Prijemi { get; set; }
    }
}
