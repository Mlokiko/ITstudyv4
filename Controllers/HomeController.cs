using System.Diagnostics;
using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITstudyv4.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.LatestThreads = _context.Threads
                .Include(t => t.User)
                .OrderByDescending(t => t.CreatedAt)
                .Take(10)
                .ToList();
            ViewBag.PopularThreads = _context.Threads
                .Include(t => t.User)
                .OrderByDescending(t => t.Views) 
                .Take(10)
                .ToList();
            ViewBag.AllCategories = _context.Categories.ToList();

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Regulations()
        { 
            return View();
        }

        public IActionResult dev()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
