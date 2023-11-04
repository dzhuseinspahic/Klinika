using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Pacijent
    {
        public int ID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Ime { get; set; } = string.Empty;
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Prezime { get; set; } = string.Empty;
        public Spol Spol { get; set; } = Spol.Nepoznato;
        public string? Adresa { get; set; }
        public string? BrojTelefoona { get; set; }

        public ICollection<Prijem> Prijemi { get; set; }
    }
}
