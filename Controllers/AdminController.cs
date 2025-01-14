using ITstudyv4.Data;
using ITstudyv4.Models;
using ITstudyv4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITstudyv4.Controllers
{
    public class AdminController : Controller
    {
        // te RoleManager nie jest aktualnie potrzebne, ale przy modyfikacji sie przyda
        private readonly UserManager<ForumUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<ForumUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> ShowAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userRolesVM = new List<UserWithRolesVM>();

            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userRolesVM.Add(new UserWithRolesVM
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    JoinDate = user.JoinDate.ToString("dd/MM/yyyy"),
                    Role = string.Join(", ", role)
                });
            }
            return View(userRolesVM);
        }
        public IActionResult EditUser()
        {
            return View();
        }
    }
}
