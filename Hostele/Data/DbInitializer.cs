using Hostele.Models;
using Hostele.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hostele.Data;

public class DbInitializer:IDbInitializer
{
    private readonly UserManager<AppUser> _userManager;
    
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public DbInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
    }

    public void Initialize()
    {
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception ex)
        {

        }

        if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Staff)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();
            _userManager.CreateAsync(new AppUser
            {
                Email = "admin@gmail.com",
                Name = "admino",
                UserName = "admin@gmail.com"

            }, "Admin!23").GetAwaiter().GetResult();
            AppUser user = _db.AppUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, SD.Role_Staff).GetAwaiter().GetResult();
        }
    }
}