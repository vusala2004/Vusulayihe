using Microsoft.AspNetCore.Mvc;

namespace VusuLastSummer.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
