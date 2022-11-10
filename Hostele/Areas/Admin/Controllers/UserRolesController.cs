using System.Linq;
using System.Security.Claims;
using Hostele.Areas.Admin.ViewModels;
using Hostele.Data;
using Hostele.Models;
using Hostele.Repository;
using Hostele.Utility;
using Hostele.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hostele.Areas.Admin.Controllers
{
    
    /*[Authorize(Roles=SD.Role_Admin)]*/
    [Area("Admin")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IActivitiesRepository _repository;

        public UserRolesController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IActivitiesRepository repository)
        {
            _roleManager = roleManager;
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var admins = await _userManager.GetUsersInRoleAsync(SD.Role_Admin);
            
            foreach (var admin in admins)
            {
                users.RemoveAll(c => c.Id == admin.Id);
            }
           
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (AppUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.Name = user.Name;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View();
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userEmail = await _userManager.GetEmailAsync(user);
            if (user == null)
            {
                return View();
            }
            
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            
            _repository.AddActivity(email, DateTime.Now, $"Usunięto role użytkownikowi {userEmail}");

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            
            _repository.AddActivity(email, DateTime.Now, $"Dodano role do użytkownika {userEmail}");
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View();
            }
            ViewBag.UserName = user.UserName;
            return View();
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userEmail = await _userManager.GetEmailAsync(user);
            if (user == null)
            {
                return View();
            }
            
            var result = await _userManager.DeleteAsync(user);
            
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user");
                return View();
            }
            
            var currentUser = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(currentUser);
            
            _repository.AddActivity(email, DateTime.Now, $"Usunięto użytkownika {userEmail}");
            
            return RedirectToAction("Index");
        }
        
        
        public async Task<IActionResult> Lock(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View();
            }
            ViewBag.UserName = user.UserName;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Lock(LockoutDateViewModel model, string userId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var userEmail = await _userManager.GetEmailAsync(user);
                if (user == null)
                {
                    return View();
                }

                await _userManager.SetLockoutEnabledAsync(user, true);
                var result = await _userManager.SetLockoutEndDateAsync(user, model.LockoutDate);


                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot set Lockout for user");
                    return View(model);
                }
                
                var currentUser = await _userManager.GetUserAsync(User);
                var email = await _userManager.GetEmailAsync(currentUser);
                
                _repository.AddActivity(email, DateTime.Now, $"Zablokowano usera {userEmail}");

                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        
         public async Task<IActionResult> Unlock(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View();
            }
            ViewBag.UserName = user.UserName;
            return View();
        }
        [HttpPost]
        [HttpPost, ActionName("Unlock")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockConfirm(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userEmail = await _userManager.GetEmailAsync(user);
            if (user == null)
            {
                return View();
            }
            
            var result = await _userManager.SetLockoutEnabledAsync(user,false);
            
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot unlock account");
                return View();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(currentUser);
            
            _repository.AddActivity(email, DateTime.Now, $"Odblokowano usera {userEmail}");
            
            return RedirectToAction("Index");
        }
        
        private async Task<List<string>> GetUserRoles(AppUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        
        
        /*public async Task<IActionResult> UserCredentials(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            return View();
        }
        [HttpPost]
        [HttpPost, ActionName("UserCredentials")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCredentialsPost(UserCredentialsViewModel userCredentialsViewModel,string userId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            var email = await _userManager.GetEmailAsync(user);
            

           // var changePasswordResult = await _userManager.ChangePasswordAsync(user, userCredentialsViewModel.OldPassword, userCredentialsViewModel.NewPassword);
            /if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            
            _repository.AddActivity(email, DateTime.Now, $"Zmieniono hasło");

            return RedirectToAction("Index");
        }*/
        
        
        public async Task<IActionResult> LoginSettings(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            var viewModel = new LoginSettingsViewModel()
            {
                MaxFailedAttempts = 5,
                SessionTimeout = 20
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> LoginSettings(LoginSettingsViewModel loginSettingsViewModel, string userId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }
                
                user.MaxLoginAttempts = loginSettingsViewModel.MaxFailedAttempts;
                user.SessionTimeoout = loginSettingsViewModel.SessionTimeout;
                await _userManager.UpdateAsync(user);
                return View(loginSettingsViewModel);
            }
            return View();
        }
    }
}