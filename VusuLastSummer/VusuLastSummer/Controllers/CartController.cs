using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.Cart;

namespace VusuLastSummer.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            // Baza tam qoşulana qədər səbəti 2 saxta məhsulla doldururuq
            var model = new CartIndexVM();

            model.CartItems.Add(new CartVM
            {
                Id = 1,
                ProductId = 1,
                ProductName = "Classic Espresso",
                Price = 3.50m,
                Quantity = 2,
                Size = "Medium",
                ImageUrl = "" // Default şəkil üçün boş qoyuruq
            });

            model.CartItems.Add(new CartVM
            {
                Id = 2,
                ProductId = 3,
                ProductName = "Cold Brew",
                Price = 4.00m,
                Quantity = 1,
                Size = "Large",
                ImageUrl = ""
            });
            return View();
        }
    }
}
