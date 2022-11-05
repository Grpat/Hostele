// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Hostele.Data;
using Microsoft.AspNetCore.Authorization;
using Hostele.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Hostele.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private ApplicationDbContext _context;

        public LogoutModel(SignInManager<AppUser> signInManager, ILogger<LogoutModel> logger, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var email = await _signInManager.UserManager.GetEmailAsync(user);
            
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            _context.Aktywnosci.Add(new Aktywnosc
            {
                User = email,
                CzasAktywnosci = DateTime.Now,
                OpisAktywnosci = "Wylogowywanie"
            });
            await _context.SaveChangesAsync();
            
            if (returnUrl != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
