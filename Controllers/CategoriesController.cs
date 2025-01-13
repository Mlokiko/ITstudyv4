using ITstudyv4.Data;
using ITstudyv4.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult EditCategory()
        {
            return View();
        }
    }
}
