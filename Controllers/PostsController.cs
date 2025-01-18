using ITstudyv4.Data;
using ITstudyv4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> ShowAllPosts(int threadId)
        {
            var posts = await _context.Posts
                .Where(p => p.ThreadId == threadId)
                .Include(p => p.User)
                .ToListAsync();

            ViewBag.ThreadId = threadId;
            ViewBag.ThreadTitle = (await _context.Threads.FindAsync(threadId))?.Title;
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
                post.CreatedDate = DateTime.UtcNow;
                post.Edited = false;
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShowAllPosts), new { threadId = post.ThreadId });
            }

            ViewBag.ThreadId = post.ThreadId;
            return View(post);
        }

    }
}
