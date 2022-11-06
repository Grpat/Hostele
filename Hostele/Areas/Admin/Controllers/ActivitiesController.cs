using AutoMapper;
using Hostele.Areas.Admin.ViewModels;
using Hostele.Repository;
using Hostele.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostele.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles=SD.Role_Admin)]
public class ActivitiesController: Controller
{
    private readonly IActivitiesRepository _repository;
    private readonly IMapper _mapper;

    public ActivitiesController(IActivitiesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> Index()
    {
        var aktywnosci = await _repository.GetAll();

        var aktywnosciViewModel = _mapper.Map<List<ActivitiesViewModel>>(aktywnosci);
        
        return View(aktywnosciViewModel);
    }
}