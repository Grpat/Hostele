using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hostele.Models;

public class Wypozyczenie
{
    [Key]
    [Required]
    public int Id { get; set; }
    public DateTime DataUtworzenia { get; set; }=DateTime.UtcNow;
    public DateTime DataRozpoczecia { get; set; }
    public DateTime DataZakonczenia { get; set; }
    public StatusWypozyczenia Status { get; set; }
    
    public string AppUserId { get; set; }
    [ForeignKey("AppUserId")]
    [ValidateNever]
    public AppUser AppUser { get; set; }
    
    public int PokojId { get; set; }
    [ForeignKey("PokojId")]
    public Pokoj? Pokoj { get; set; }
}