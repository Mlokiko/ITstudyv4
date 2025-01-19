using ITstudyv4.Data;
using ITstudyv4.Models;
using ITstudyv4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> ShowAllCategories(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Categories.OrderBy(i => i.Id);
            var totalCategories = await query.CountAsync();
            var categories = await query
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new PaginatedListVM<Categories>
            {
                Items = categories,
                TotalItems = totalCategories,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult AddNewCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewCategory([Bind("Name,Description")] Categories category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Dodano nową kategorię.";
                return RedirectToAction(nameof(ManageCategory));
            }

            return View(category);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, [Bind("Id,Name,Description")] Categories category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Categories.Any(e => e.Id == category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Pomyślnie zaktualizowano kategorię.";
                return RedirectToAction(nameof(ManageCategory));
            }
            return View(category);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> ManageCategory()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageCategory(int id, [Bind("id,Name,Description")] Categories category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Categories.Any(x => x.Id == category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ManageCategory));
            }
            return View(category);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("DeleteCategory")]
        [Authorize(Roles = "Admin, Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Pomyślnie usunięto kategorię.";
            return RedirectToAction(nameof(ManageCategory));
        }
    }
}
