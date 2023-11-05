using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Nalaz
    {
        public int ID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Opis { get; set; } 
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DatumVrijemeKreiranja { get; set; }
    }
}
