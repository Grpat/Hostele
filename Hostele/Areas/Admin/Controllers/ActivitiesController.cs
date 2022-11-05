using AutoMapper;
using Hostele.Areas.Admin.ViewModels;
using Hostele.Data;
using Hostele.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hostele.Areas.Admin.Controllers;

public class ActivitiesController: Controller
{
    private readonly IActivitiesRepository _repository;
    private readonly Mapper _mapper;

    public ActivitiesController(IActivitiesRepository repository, Mapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> Index()
    {
        var aktywnosci = _repository.GetAll();

        var aktywnosciViewModel = _mapper.Map<List<ActivitiesViewModel>>(aktywnosci);
        
        return View(aktywnosciViewModel);
    }
}