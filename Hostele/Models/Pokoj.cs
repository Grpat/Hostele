using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hostele.Models;

public class Pokoj
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    [Display(Name="Hostel")]
    public int HostelId { get; set; }
    [ForeignKey("HostelId")]
    
    
    public Hostel Hostel { get; set; }
    
    public int RodzajId { get; set; }
    [ForeignKey("RodzajId")]

    public Rodzaj Rodzaj { get; set; }
    
    [Required]
    [Display(Name="Cena za nocleg")]
    public double CenaZaNocleg { get; set; }
    [Required]
    [Display(Name="Ilosc lozek")]
    public int IloscLozek { get; set; }
    
    
}