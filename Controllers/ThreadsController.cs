using ITstudyv4.Data;
using ITstudyv4.Models;
using ITstudyv4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITstudyv4.Controllers
{
    public class ThreadsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ForumUser> userManager;

        public ThreadsController(AppDbContext context, UserManager<ForumUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowThreadsInCategory(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Threads
                .Where(t => t.CategoryId == categoryId);

            var totalThreads = await query.CountAsync();

            var threads = await query
                
                .Include(t => t.User)
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new PaginatedListVM<Threads>
            {
                Items = threads,
                TotalItems = totalThreads,
                CurrentPage = pageNumber,
                PageSize = pageSize,
            };

            ViewBag.CategoryName = (await _context.Categories.FindAsync(categoryId))?.Name;
            ViewBag.CategoryId = categoryId;
            return View(viewModel);
        }

        [Authorize]
        public IActionResult AddNewThread(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowAllThreads(int pageNumber = 1, int pageSize = 10)
        {
            //paginacja
            var query = _context.Threads.OrderBy(i => i.Id);
            var totalThreads = await query.CountAsync();

            var threads = await query
                .Include(t => t.User)
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new PaginatedListVM<Threads>
            {
                Items = threads,
                TotalItems = totalThreads,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewThread(int categoryId, [Bind("Title")] Threads thread)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "Ta kategoria nie istnieje.");
                return View(thread);
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError(string.Empty, "Musisz być zalogowany, żeby stworzyć temat.");
                    ViewBag.CategoryId = thread.CategoryId;
                    return View(thread);
                }
                thread.UserId = userId;
                thread.CategoryId = categoryId;
                _context.Add(thread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowThreadsInCategory), new { categoryId });
            }

            ViewBag.CategoryId = categoryId;
            return View(thread);
        }

        [Authorize]
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIsAdminOrModerator = User.IsInRole("Admin") || User.IsInRole("Moderator");
            if (thread.UserId != userId && !userIsAdminOrModerator)
            {
                return Forbid();
            }

            return View(thread);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditThread(int id, [Bind("Id,Title")] Threads thread)
        {
            if (id != thread.Id)
            {
                return NotFound();
            }

            var originalThread = await _context.Threads.FindAsync(id);
            if (originalThread == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIsAdminOrModerator = User.IsInRole("Admin") || User.IsInRole("Moderator");
            if (originalThread.UserId != userId && !userIsAdminOrModerator)
            {
                return Forbid();
            }

            originalThread.Title = thread.Title;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(originalThread);
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
                return RedirectToAction(nameof(ShowThreadsInCategory), new { categoryId = originalThread.CategoryId });
            }
            return View(thread);
        }

        [Authorize]
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIsAdminOrModerator = User.IsInRole("Admin") || User.IsInRole("Moderator");
            if (thread.UserId != userId && !userIsAdminOrModerator)
            {
                return Forbid();
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIsAdminOrModerator = User.IsInRole("Admin") || User.IsInRole("Moderator");
            if (thread.UserId != userId && !userIsAdminOrModerator)
            {
                return Forbid();
            }

            _context.Threads.Remove(thread);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowThreadsInCategory), new { categoryId = thread.CategoryId });
        }
    }
}
