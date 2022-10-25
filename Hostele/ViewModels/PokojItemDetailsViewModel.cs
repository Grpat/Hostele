namespace Hostele.Models.ViewModels;

public class PokojItemDetailsViewModel
{
    public int Id { get; set; }
    
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    
    public int HostelId { get; set; }
   
    public Hostel Hostel { get; set; }
    
    public double CenaZaNocleg { get; set; }
    
    public int IloscLozek { get; set; }
    
    public int RodzajId { get; set; }
    
    public Rodzaj Rodzaj { get; set; }
    
   
}