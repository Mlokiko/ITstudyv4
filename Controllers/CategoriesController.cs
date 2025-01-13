using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ITstudyv4.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowAllCategories()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult AddNewCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewCategory([Bind("Name,Description")] Categories category)
        {
            if (ModelState.IsValid)
            {
                Categories cat = new();
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowAllCategories));
            }
            return View(category);
        }

        public IActionResult EditCategory()
        {
            return View();
        }
    }
}
