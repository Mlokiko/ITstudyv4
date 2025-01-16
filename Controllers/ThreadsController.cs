using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITstudyv4.Controllers
{
    public class ThreadsController : Controller
    {
        private readonly AppDbContext _context;
        public ThreadsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ShowAllThreads()
        {
            var threads = _context.Threads.ToList();
            return View(threads);
        }

        public IActionResult EditThread()
        {
            
            return View();
        }

        public IActionResult AddNewThread()
        {
            ViewBag.AllCategories = _context.Categories.Select(r => r.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewThread([Bind("Title,Categories")] Threads thread)
        {
            if (ModelState.IsValid)
            {
                thread.CreatedAt = DateTime.UtcNow;
                thread.Views = 0;
                //thread.CategoryId = _context.Categories.FirstOrDefault(c => c.Name == thread.Category)?.Id ?? 0;
                _context.Add(thread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageThreads));
            }
            return View(thread);
        }

        public IActionResult ManageThreads()
        {
            var threads = _context.Threads.ToList();
            return View(threads);
        }
    }
}
