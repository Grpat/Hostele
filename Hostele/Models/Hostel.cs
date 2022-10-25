using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hostele.Models;

public class Hostel
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    [Required]
    public int Pojemnosc { get; set; }
    
    public ICollection<Pokoj> Pokoje { get; set; }
}