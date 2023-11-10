using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date.Date >= DateTime.Now.Date 
                    || (date.Date == DateTime.Now.Date && date.TimeOfDay >= DateTime.Now.TimeOfDay);
            }

            return false;
        }
    }
}
