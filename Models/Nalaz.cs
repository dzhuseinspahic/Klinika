using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Nalaz
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Ovo polje mora sadržavati više od 2 karaktera.")]
        public string Opis { get; set; } 
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        [Display(Name = "Datum i vrijeme kreiranja")]
        public DateTime DatumVrijemeKreiranja { get; set; }
    }
}
