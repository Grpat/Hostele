using System.ComponentModel.DataAnnotations;
using Hostele.CustomValidators;

namespace Hostele.ViewModels;

public class LockoutDateViewModel
{
    [FutureDateValidation(ErrorMessage = "Must be future date")] 
    public DateTime LockoutDate { get; set; }
}