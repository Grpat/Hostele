using System.ComponentModel.DataAnnotations;

namespace Hostele.Areas.Admin.ViewModels;

public class GeneratePasswordViewModel
{
    [Required]
    public int a { get; set; }
    [Required]
    public int x { get; set; }
    
}