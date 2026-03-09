using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.DAL;
using VusuLastSummer.ViewModels.Menu;
using VusuLastSummer.ViewModels.Product;

namespace VusuLastSummer.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Menu()
        {
            var productsFromDb = await _context.Products
        .Include(p => p.Category)
        .Include(p => p.ProductImages)
        .ToListAsync();

            var model = productsFromDb.Select(p => new MenuVM
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category?.Name ?? "General",
                Description = p.Description,
                Price = p.Price,

                // Sənin modelində IsPrimary bool? (nullable) olduğu üçün .GetValueOrDefault() istifadə edirik
                ImageUrl = p.ProductImages?.FirstOrDefault(pi => pi.IsPrimary.GetValueOrDefault())?.ImageURL
                           ?? p.ProductImages?.FirstOrDefault()?.ImageURL
                           ?? "/img/default-product.jpg"
            }).ToList();

            return View(model);
        }
        public IActionResult Details(int id)
        {
            // Gələcəkdə bu məlumatları Verilənlər Bazasından (SQL) ID-yə görə çəkəcəyik.
            // Hələlik səhifənin necə işlədiyini görmək üçün Dummy Data yaradırıq:

            var product = new ProductVM
            {
                Id = id,
                Name = "Classic Espresso",
                Description = "A rich, full-bodied espresso with a sweet caramel finish. Perfectly brewed for your morning start.",
                Price = 3.50m,
                ImageUrl = "/images/espresso.jpg", // Öz şəkil yolunu yazarsan
                Category = "Espresso",
                RelatedProducts = new List<ProductVM>
                {
                    new ProductVM { Id = 2, Name = "Americano", Price = 3.00m, ImageUrl = "/images/americano.jpg" },
                    new ProductVM { Id = 3, Name = "Macchiato", Price = 4.00m, ImageUrl = "/images/macchiato.jpg" }
                }
            };

            return View(product);
        }
    }
}
