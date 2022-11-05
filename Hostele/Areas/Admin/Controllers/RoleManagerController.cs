using Hostele.Data;
using Hostele.Models;
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
    private ApplicationDbContext _context;
    public RoleManagerController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ApplicationDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
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
            var user = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(user);

            _context.Aktywnosci.Add(new Aktywnosc
            {
                User = email,
                CzasAktywnosci = DateTime.Now,
                OpisAktywnosci = $"Dodano rolę {roleName.Trim()}"
            });
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}