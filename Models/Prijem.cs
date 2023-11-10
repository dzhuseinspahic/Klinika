using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Prijem
    {
        public int ID { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        [Display(Name = "Datum i vrijeme prijema")]
        [FutureDate(ErrorMessage = "Izaberite datum u budućnosti.")]
        public DateTime DatumVrijemePrijema { get; set; }
        public int PacijentID { get; set; }
        public int LjekarID { get; set; }
        [Display(Name = "Hitni prijem")]
        public bool HitniPrijem { get; set; } = false;

        public Pacijent? Pacijent { get; set; }
        public Ljekar? Ljekar { get; set; }
    }
}
