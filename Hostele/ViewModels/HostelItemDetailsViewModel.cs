namespace Hostele.Models.ViewModels;

public class HostelItemDetailsViewModel
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    public int Pojemnosc { get; set; }
    
    public IEnumerable<Pokoj> Pokoje { get; set; }
}