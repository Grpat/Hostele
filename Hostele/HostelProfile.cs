using AutoMapper;
using Hostele.Models;
using Hostele.Models.ViewModels;
using Hostele.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hostele;

public class HostelProfile:Profile
{
    public HostelProfile()
    {
        CreateMap<Hostel, HostelItemViewModel>();
        CreateMap<Hostel, HostelViewModel>();
        CreateMap< HostelItemViewModel,Hostel>();
        
        CreateMap<Pokoj, PokojViewModel>();

        CreateMap<PokojItemViewModel, Pokoj>();
        CreateMap<Pokoj, PokojItemViewModel>();
        
        CreateMap<Pokoj, Rodzaj>();
        
        
        CreateMap<PokojItemDetailsViewModel, Pokoj>();
        CreateMap<Pokoj, PokojItemDetailsViewModel>();
        
       
        CreateMap< HostelItemDetailsViewModel,Hostel>();
        CreateMap< Hostel,HostelItemDetailsViewModel>();
        
        CreateMap< Wypozyczenie,WypozyczenieCreateViewModel>();
        CreateMap<WypozyczenieCreateViewModel,Wypozyczenie>();
        
        

    }
}