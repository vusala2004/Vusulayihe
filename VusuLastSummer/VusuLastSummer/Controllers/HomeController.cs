using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VusuLastSummer.DAL;
using VusuLastSummer.Models;
using VusuLastSummer.ViewModels.Home;


namespace VusuLastSummer.Controllers
{
    public class HomeController : Controller
    {


        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {


            var model = new HomeVM
            {
                // Bazadan yalnýz ön? çýxan 3 m?hsulu ç?kirik
                FeaturedProducts = await _context.Products
                    .Where(p => p.IsFeatured)
                    .Take(3)
                    .ToListAsync()
            };
            return View();
        }

       
    }
}
