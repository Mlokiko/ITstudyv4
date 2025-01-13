using Microsoft.AspNetCore.Mvc;

namespace ITstudyv4.Controllers
{
    public class ThreadsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditThread()
        {
            return View();
        }
        public IActionResult AddNewThread()
        {
            return View();
        }
    }
}
