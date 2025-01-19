using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ITstudyv4.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowAllPosts(int threadId)
        {
            var thread = await _context.Threads
                .Include(t => t.User)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == threadId);

            if (thread == null)
            {
                return NotFound();
            }

            thread.Views++;
            await _context.SaveChangesAsync();

            var posts = await _context.Posts
                .Where(p => p.ThreadId == threadId)
                .Include(p => p.User)
                .ToListAsync();

            ViewBag.ThreadId = threadId;
            ViewBag.ThreadTitle = (await _context.Threads.FindAsync(threadId))?.Title;
            ViewBag.UserName = thread.User?.UserName ?? "Nieznany użytkownik";
            ViewBag.CreatedDate = thread.CreatedAt.ToString("dd MMMM yyyy");
            ViewBag.CategoryName = thread.Category?.Name ?? "Brak kategorii";
            return View(posts);
        }

        public IActionResult AddNewPost(int threadId)
        {
            ViewBag.ThreadId = threadId;
            return View(new Posts { ThreadId = threadId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewPost(Posts post)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError(string.Empty, "User must be logged in to create a post.");
                    ViewBag.ThreadId = post.ThreadId;
                    return View(post);
                }

                post.UserId = userId;
                post.CreatedDate = DateTime.UtcNow;
                post.Edited = false;

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowAllPosts), new { threadId = post.ThreadId });
            }

            ViewBag.ThreadId = post.ThreadId;
            return View(post);
        }

        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIsAdminOrModerator = User.IsInRole("Admin") || User.IsInRole("Moderator");
            if (post.UserId != userId && !userIsAdminOrModerator)
            {
                return Forbid();
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("Id,Content")] Posts post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            var originalPost = await _context.Posts.FindAsync(id);
            if (originalPost == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIsAdminOrModerator = User.IsInRole("Admin") || User.IsInRole("Moderator");
            if (post.UserId != userId && !userIsAdminOrModerator)
            {
                return Forbid();
            }

            originalPost.Content = post.Content;

            if (ModelState.IsValid)
            {
                try
                {
                    post.Edited = true;
                    _context.Update(originalPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Threads.Any(e => e.Id == post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ShowAllPosts), new { threadId = originalPost.ThreadId });
            }
            return View(post);
        }

        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIsAdminOrModerator = User.IsInRole("Admin") || User.IsInRole("Moderator");
            if (post.UserId != userId && !userIsAdminOrModerator)
            {
                return Forbid();
            }

            return View(post);
        }

        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIsAdminOrModerator = User.IsInRole("Admin") || User.IsInRole("Moderator");
            if (post.UserId != userId && !userIsAdminOrModerator)
            {
                return Forbid();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowAllPosts), new { threadId = post.ThreadId });
        }
    }
}
