using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hostele.Models.ViewModels;

public class PokojItemViewModel
{
    
    public int Id { get; set; }
    
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    
    public int HostelId { get; set; }
    
    
    public double CenaZaNocleg { get; set; }
    
    public int IloscLozek { get; set; }
    
    public int RodzajId { get; set; }
    
    
    public IEnumerable<SelectListItem>? RodzajePokoi { get; set; }
    public IEnumerable<SelectListItem>? ListofHostels { get; set; }
    
}