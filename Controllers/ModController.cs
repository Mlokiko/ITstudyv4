using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITstudyv4.Controllers
{
    public class ModController : Controller
    {
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult ModPanel()
        {
            return View();
        }
    }
}
