using System.ComponentModel.DataAnnotations.Schema;
using Hostele.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hostele.ViewModels;

public class WypozyczenieCreateViewModel
{
    public DateTime DataRozpoczecia { get; set; }
    public DateTime DataZakonczenia { get; set; }
    
    public int PokojId { get; set; }
    
    public IEnumerable<SelectListItem>? ListaPokoi { get; set; }
    
  
}