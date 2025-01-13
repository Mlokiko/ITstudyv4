using ITstudyv4.Models;
using ITstudyv4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITstudyv4.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ForumUser> signInManager;
        private readonly UserManager<ForumUser> userManager;

        public AccountController(SignInManager<ForumUser> signInManager, UserManager<ForumUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                //login
               var result = await signInManager.PasswordSignInAsync(model.UserName!, model.Password!,model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub hasło");
                return View(model);
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                ForumUser user = new()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    JoinDate = DateTime.UtcNow.Date, // Do wyświetlania możemy użyć .Today, a pełną datę dawać tylko w przypadku zapisywania do bazy
                    RankId = 1                       // Oryginalnie miało być now, ale sie coś spruło z typami z postgresa
                };

                var result = await userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
        public IActionResult ManageAccount()
        {
            return View();
        }
    }
}
