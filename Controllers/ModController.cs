using Microsoft.AspNetCore.Mvc;

namespace ITstudyv4.Controllers
{
    public class ModController : Controller
    {
        public IActionResult ModPanel()
        {
            return View();
        }
    }
}
