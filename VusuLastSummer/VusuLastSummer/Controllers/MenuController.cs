using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.Menu;
using VusuLastSummer.ViewModels.Product;

namespace VusuLastSummer.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Menu()
        {
            // Baza olmadığı üçün C#-da saxta menyu siyahısı yaradırıq
            var products = new List<MenuVM>
            {
                new MenuVM { Id = 1, Name = "Classic Espresso", Category = "Espresso", Description = "Rich and bold espresso.", Price = 3.50m, ImageUrl = "" },
                new MenuVM { Id = 2, Name = "Caramel Macchiato", Category = "Espresso", Description = "Espresso with vanilla and caramel.", Price = 4.50m, ImageUrl = "" },
                new MenuVM { Id = 3, Name = "Cold Brew", Category = "Cold Brew", Description = "Slow-steeped cold brew over ice.", Price = 4.00m, ImageUrl = "" },
                new MenuVM { Id = 4, Name = "Green Tea", Category = "Tea", Description = "Organic calming green tea.", Price = 3.00m, ImageUrl = "" },
                new MenuVM { Id = 5, Name = "Butter Croissant", Category = "Pastries", Description = "Flaky and buttery fresh pastry.", Price = 2.50m, ImageUrl = "" },
                new MenuVM { Id = 6, Name = "Pumpkin Spice Latte", Category = "Seasonal", Description = "Your favorite fall seasonal drink.", Price = 5.00m, ImageUrl = "" }
            };
            return View();
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
