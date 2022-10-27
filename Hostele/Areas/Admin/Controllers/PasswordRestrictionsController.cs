using Hostele.Areas.Admin.ViewModels;
using Hostele.Models;
using Hostele.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hostele.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles=SD.Role_Admin)]
public class PasswordRestrictionsController: Controller
{
    private readonly UserManager<AppUser> _userManager;

    public PasswordRestrictionsController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var viewModel = new PasswordRestrictionViewModel
        {
            MinLength = _userManager.Options.Password.RequiredLength,
            RequiredNumber = _userManager.Options.Password.RequireDigit
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Index(PasswordRestrictionViewModel passwordRestrictionViewModel)
    {
        _userManager.Options.Password.RequiredLength = passwordRestrictionViewModel.MinLength;
        _userManager.Options.Password.RequireDigit = passwordRestrictionViewModel.RequiredNumber;
        
        var viewModel = new PasswordRestrictionViewModel
        {
            MinLength = _userManager.Options.Password.RequiredLength,
            RequiredNumber = _userManager.Options.Password.RequireDigit
        };
        
        return View(viewModel);
    }
}