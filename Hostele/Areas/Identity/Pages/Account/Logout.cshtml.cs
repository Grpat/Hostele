// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Hostele.Data;
using Microsoft.AspNetCore.Authorization;
using Hostele.Models;
using Hostele.Repository;
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
        private readonly IActivitiesRepository _repository;

        public LogoutModel(SignInManager<AppUser> signInManager, ILogger<LogoutModel> logger, IActivitiesRepository repository)
        {
            _signInManager = signInManager;
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            
            _repository.AddActivity(email, DateTime.Now, "Wylogowywanie");
            
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
