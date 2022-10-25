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
    public RoleManagerController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
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
        }
        return RedirectToAction("Index");
    }
}