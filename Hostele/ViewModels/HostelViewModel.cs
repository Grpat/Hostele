using System.Collections;

namespace Hostele.Models.ViewModels;

public class HostelViewModel 
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    public int Pojemnosc { get; set; }
    
    public IEnumerable<HostelViewModel> Hostele { get; set; }
}