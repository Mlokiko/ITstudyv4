﻿using ITstudyv4.Data;
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
        public async Task<IActionResult> AddNewThread([Bind("Name,Categories")] Thread thread)
        {
            if (ModelState.IsValid)
            {
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
