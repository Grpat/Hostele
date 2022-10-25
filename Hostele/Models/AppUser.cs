using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Hostele.Models;

public class AppUser:IdentityUser
{
    public string Name { get; set; }
}