using Microsoft.AspNetCore.Mvc;

namespace ITstudyv4.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowAllUsers()
        {
            return View();
        }
        public IActionResult EditUser()
        {
            return View();
        }
    }
}
