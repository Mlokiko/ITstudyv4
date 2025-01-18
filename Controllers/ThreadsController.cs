using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITstudyv4.Controllers
{
    public class ThreadsController : Controller
    {
        private readonly AppDbContext _context;

        public ThreadsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShowAllThreads(int categoryId)
        {
            var threads = await _context.Threads
                .Where(t => t.CategoryId == categoryId)
                .Include(t => t.User)
                .ToListAsync();

            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = (await _context.Categories.FindAsync(categoryId))?.Name;

            return View(threads);
        }


        public IActionResult AddNewThread(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewThread(int categoryId, [Bind("Title")] Threads thread)
        {
            // Ensure category exists
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "The specified category does not exist.");
                return View(thread);
            }

            if (ModelState.IsValid)
            {
                thread.CategoryId = categoryId;
                _context.Add(thread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowAllThreads), new { categoryId });
            }

            ViewBag.CategoryId = categoryId;
            return View(thread);
        }



        public async Task<IActionResult> EditThread(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thread = await _context.Threads.FindAsync(id);
            if (thread == null)
            {
                return NotFound();
            }
            return View(thread);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditThread(int id, [Bind("Id,Title")] Threads thread)
        {
            if (id != thread.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Threads.Any(e => e.Id == thread.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ManageThread));
            }
            return View(thread);
        }

        public async Task<IActionResult> ManageThread()
        {
            var threads = await _context.Threads.ToListAsync();
            return View(threads);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageThread(int id, [Bind("id,Title")] Threads thread)
        {
            if (id != thread.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Threads.Any(x => x.Id == thread.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ManageThread));
            }
            return View(thread);
        }
        public async Task<IActionResult> DeleteThread(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thread = await _context.Threads.FirstOrDefaultAsync(x => x.Id == id);
            if (thread == null)
            {
                return NotFound();
            }
            return View(thread);
        }

        [HttpPost, ActionName("DeleteThread")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            if (thread == null)
            {
                return NotFound();
            }
            _context.Threads.Remove(thread);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageThread));
        }

    }
}
