using System.ComponentModel.DataAnnotations;

namespace Hostele.Models;

public class Rodzaj
{
    [Required]
    [Key]
    public int Id { get; set; }
    public string NazwaRodzaju { get; set; }
}