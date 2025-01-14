using ITstudyv4.Data;
using Microsoft.AspNetCore.Mvc;

namespace ITstudyv4.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult ShowAllUsers()
        {
            var users = _context.ForumUser.ToList();
            return View(users);
        }
        public IActionResult EditUser()
        {
            return View();
        }
    }
}
