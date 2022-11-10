using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Hostele.Models;
using Microsoft.AspNetCore.Identity;

namespace Hostele.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Logout([FromBody] Time time)
    {
        var identity = (ClaimsIdentity)User.Identity;
        var user = HttpContext.User;
        if (user?.Identity.IsAuthenticated == true)
        {
            var signedUser =await _userManager.FindByEmailAsync(identity.Name);
            if (signedUser.SessionTimeoout <= time.count)
            {
                await _signInManager.SignOutAsync();
            }

            return Json(new { redirectToUrl = Url.Page("/Account/Login", new { area = "Identity" })});
        }
        return Json("");
    }

    public record Time(int count);

    /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()*/
    /*{
        //return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        
    }*/
}