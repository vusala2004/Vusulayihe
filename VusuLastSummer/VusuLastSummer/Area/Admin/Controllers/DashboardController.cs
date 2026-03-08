using Microsoft.AspNetCore.Mvc;

namespace VusuLastSummer.Area.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "Admin")] // Hələlik test edəndə mane olmasın deyə kommentə ala bilərsən
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
