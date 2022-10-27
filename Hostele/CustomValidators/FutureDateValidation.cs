using System.ComponentModel.DataAnnotations;

namespace Hostele.CustomValidators;

public class FutureDateValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime dateTime = Convert.ToDateTime(value);
        return dateTime > DateTime.Now;
    }
    
}