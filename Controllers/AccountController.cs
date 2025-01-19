using ITstudyv4.Data;
using ITstudyv4.Models;
using ITstudyv4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ITstudyv4.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ForumUser> signInManager;
        private readonly UserManager<ForumUser> userManager;
        private readonly AppDbContext context;
        public AccountController(SignInManager<ForumUser> signInManager, UserManager<ForumUser> userManager, AppDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.UserName!, model.Password!, model.RememberMe, false);
            if (result.Succeeded)
            {
                TempData["Message"] = "Pomyślnie zalogowano.";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub hasło");
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ForumUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                JoinDate = DateTime.UtcNow.Date,
            };

            var result = await userManager.CreateAsync(user, model.Password!);
            await userManager.AddToRoleAsync(user, "Użytkownik");
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                TempData["Message"] = "Pomyślnie stworzono konto.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["Message"] = "Pomyślnie wylogowano.";
            return RedirectToAction("Index", "Home"); 
        }

        [Authorize]
        public async Task<IActionResult> ManageAccount()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var role = await userManager.GetRolesAsync(user);
            var model = new UserWithRolesVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = string.Join(", ", role),
                ProfilePictureURL = user.ProfilePictureURL,
                Bio = user.Bio,
                JoinDate = user.JoinDate.ToString("dd/MM/yyyy"),
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ChangeAboutMe(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserWithRolesVM
            {
                UserId = user.Id,
                Bio = user.Bio
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAboutMe(UserWithRolesVM model)
        {
            if (!ModelState.IsValid || model.UserId == null)
            {
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            user.Bio = model.Bio;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            TempData["Message"] = "Zaktualizowano 'O sobie'.";
            return RedirectToAction("ManageAccount");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid || model.CurrentPassword == null || model.NewPassword == null)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                TempData["Message"] = "Hasło zmieniono pomyślnie.";
                return RedirectToAction("ManageAccount");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ChangeEmail(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserWithRolesVM
            {
                UserId = user.Id,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(UserWithRolesVM model)
        {
            if (!ModelState.IsValid || model.UserId == null)
            {
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = model.Email;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            TempData["Message"] = "Zaktualizowano Email.";
            return RedirectToAction("ManageAccount");
        }

        [Authorize]
        public async Task<IActionResult> ChangeProfilePicture(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserWithRolesVM
            {
                UserId = user.Id,
                ProfilePictureURL = user.ProfilePictureURL
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeProfilePicture(UserWithRolesVM model)
        {
            if (!ModelState.IsValid || model.UserId == null)    // Trochę bez sensu sprawdzanie UserId, ale dodałem bo na dole w FindByAsync próło się o możliwość pustego stringa
            {
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            user.ProfilePictureURL = model.ProfilePictureURL;
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            TempData["Message"] = "Zaktualizowano Zdjęcie profilowe.";
            return RedirectToAction("ManageAccount");
        }

        [Authorize]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var role = await userManager.GetRolesAsync(user);
            var model = new UserWithRolesVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = string.Join(", ", role),
                ProfilePictureURL = user.ProfilePictureURL,
                Bio = user.Bio,
                JoinDate = user.JoinDate.ToString("dd/MM/yyyy"),
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAccountConfirmed(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var threads = context.Threads.Where(o => o.UserId == id);
            context.Threads.RemoveRange(threads);

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await signInManager.SignOutAsync();
                TempData["Message"] = "Twoje konto zostało usunięte.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }
    }
}