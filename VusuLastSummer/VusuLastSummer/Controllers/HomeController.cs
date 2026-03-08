using Microsoft.AspNetCore.Mvc;
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

            // Baza olmadýðý üçün C# daxilind? ?ll? 3 d?n? saxta m?hsul yaradýrýq
            var dummyProducts = new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Classic Espresso",
                    Description = "Rich, full-bodied espresso with a beautiful crema.",
                    Price = 3.50m,
                    //ImageUrl = "", // Boþ qoyuruq ki, default þ?kil iþl?sin
                    //IsFeatured = true
                },
                new Product {
                    Id = 2,
                    Name = "Vanilla Latte",
                    Description = "Smooth espresso with steamed milk and vanilla syrup.",
                    Price = 4.50m,
                    //ImageUrl = "",
                    //IsFeatured = true
                },
                new Product {
                    Id = 3,
                    Name = "Cold Brew",
                    Description = "Slow-steeped cold brew coffee over ice.",
                    Price = 4.00m,
                    //ImageUrl = "",
                    //IsFeatured = true
                }
            };

            var model = new HomeVM
            {
                FeaturedProducts = dummyProducts
            };
            //var model = new HomeVM
            //{
            //    // Bazadan yalnýz ön? çýxan 3 m?hsulu ç?kirik
            //    FeaturedProducts = await _context.Products
            //        .Where(p => p.IsFeatured)
            //        .Take(3)
            //        .ToListAsync()
            //};
            return View();
        }

       
    }
}
