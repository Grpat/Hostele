using System.Security.Claims;
using Hostele.Data;
using Hostele.Models;
using Hostele.Repository;
using Hostele.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hostele.Areas.Admin.Controllers;
[Area("Admin")]

[Authorize(Roles=SD.Role_Admin)]
public class RoleManagerController : Controller
{
    // GET
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IActivitiesRepository _repository;

    public RoleManagerController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IActivitiesRepository repository)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _repository = repository;
    }
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }
    [HttpPost]
    public async Task<IActionResult> AddRole(string roleName)
    {
        if (roleName != null)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            _repository.AddActivity(email, DateTime.Now, $"Dodano rolę {roleName.Trim()}");
        }
        return RedirectToAction("Index");
    }
}