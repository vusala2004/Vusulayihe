using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace VusuLastSummer.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
