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
    public class AdminController : Controller
    {
        private readonly UserManager<ForumUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public AdminController(UserManager<ForumUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> ShowAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            var usersQuery = _userManager.Users.OrderBy(u => u.UserName);
            var totalUsers = await usersQuery.CountAsync();
            var paginatedUsers = await usersQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userRolesVM = new List<UserWithRolesVM>();

            foreach (var user in paginatedUsers)
            {
                var role = await _userManager.GetRolesAsync(user);
                userRolesVM.Add(new UserWithRolesVM
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    ProfilePictureURL = user.ProfilePictureURL,
                    JoinDate = user.JoinDate.ToString("dd/MM/yyyy"),
                    Role = string.Join(", ", role)
                });
            }

            var viewModel = new PaginatedListVM<UserWithRolesVM>
            {
                Items = userRolesVM,
                TotalItems = totalUsers,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var model = new EditUserVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                ProfilePictureURL = user.ProfilePictureURL,
                Bio = user.Bio
            };

            ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserVM model)
        {
            if (!ModelState.IsValid || model.UserId == null)
            {
                ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Bio = model.Bio;
            user.ProfilePictureURL = model.ProfilePictureURL;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
                return View(model);
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = model.Roles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(model.Roles).ToList();

            if (rolesToAdd.Any())
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToRemove.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            TempData["Message"] = "Użytkownik zaktualizowany.";
            return RedirectToAction("ShowAllUsers");
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Message"] = "Użytkownik usunięty.";
                return RedirectToAction("ShowAllUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }
    }
}
