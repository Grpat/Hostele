using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Hostele.Models;

public class AppUser:IdentityUser
{
    public string Name { get; set; }
    public bool IsInitialLogin { get; set; } = false;
    public int MaxLoginAttempts { get; set; } = 5;
    public int SessionTimeoout { get; set; } = 1;
    
    public double? GeneratedPassword{ get; set; }
}