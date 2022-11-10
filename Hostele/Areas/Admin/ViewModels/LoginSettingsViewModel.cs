using System.ComponentModel.DataAnnotations;

namespace Hostele.Areas.Admin.ViewModels;

public class LoginSettingsViewModel
{
    [Range(0, 15, ErrorMessage = "Timeout can only be between 0 .. 15")]
    public int SessionTimeout { get; set; }
    
    [Range(0, 15, ErrorMessage = "Max login failed attempts can only be between 0 .. 15")]
    public int MaxFailedAttempts { get; set; }
}