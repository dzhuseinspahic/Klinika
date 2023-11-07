using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public enum Titula
    {
        Specijalista,
        Specijalizant,
        [Display(Name = "Medicinska sestra")]
        MedicinskaSestra
    }
}
