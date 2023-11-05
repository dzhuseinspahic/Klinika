using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class Prijem
    {
        public int ID { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DatumVrijemePrijema { get; set; }
        public int PacijentID { get; set; }
        public int LjekarID { get; set; }
        public int? NalazID { get; set; }
        public bool HitniPrijem { get; set; } = false;

        public Pacijent Pacijent { get; set; }
        public Ljekar Ljekar { get; set; }
        public Nalaz Nalaz { get; set; }
    }
}
