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
            // Bütün məhsulları çəkirik, amma dizayn pozulmasın deyə yalnız 3 dənəsini (Take(3)) alırıq.
            // Əgər IsDeleted problemi varsa, hələlik o şərti yığışdıraq ki, ekranda görə bilək:
            var products = await _context.Products
                .Include(p => p.ProductImages)
                // .Where(p => !p.IsDeleted) <-- Əgər SQL-də hamısı gizlənibsə, bunu hələlik kommetdə saxla
                .Take(3)
                .ToListAsync();

            HomeVM homeVM = new HomeVM
            {
                FeaturedProducts = products
            };

            return View(homeVM);
        }


    }
}
