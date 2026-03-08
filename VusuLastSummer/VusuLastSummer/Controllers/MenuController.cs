using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.Menu;

namespace VusuLastSummer.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
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
    }
}
