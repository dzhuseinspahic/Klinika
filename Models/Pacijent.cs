using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Pacijent
    {
        public int ID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Ime { get; set; } 
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Prezime { get; set; } 
        public Spol Spol { get; set; } = Spol.Nepoznato;
        public string? Adresa { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? BrojTelefona { get; set; }

        public ICollection<Prijem> Prijemi { get; set; }
    }
}
