using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.DAL;
using VusuLastSummer.ViewModels.Cart;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VusuLastSummer.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        // JSON keçmədiyiniz üçün səbəti müvəqqəti olaraq static listdə saxlayırıq.
        // Bu, proqram işlədiyi müddətcə səbəti yadda saxlayacaq.
        private static List<CartVM> _cartItems = new List<CartVM>();

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // 1. SƏBƏT SƏHİFƏSİ
        public IActionResult Cart()
        {
            // Cəmi məbləğləri burada əllə mənimsətməyə ehtiyac yoxdur (Xəta CS0200 bura idi)
            CartIndexVM model = new CartIndexVM
            {
                CartItems = _cartItems,
                DeliveryFee = _cartItems.Any() ? 3.00m : 0m
            };

            return View(model);
        }

        // 2. SƏBƏTƏ ƏLAVƏ ETMƏK
        [HttpPost]
        public async Task<IActionResult> Add(int ProductId, string Size, int Quantity)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == ProductId && !p.IsDeleted);

            if (product == null) return NotFound();

            var existingItem = _cartItems.FirstOrDefault(c => c.ProductId == ProductId && c.Size == Size);

            if (existingItem != null)
            {
                existingItem.Quantity += Quantity;
            }
            else
            {
                decimal sizePrice = 0;
                if (Size == "Medium") sizePrice = 0.50m;
                if (Size == "Large") sizePrice = 1.00m;

                // XƏTA CS0266 BURADA HƏLL OLUNDU: == true əlavə etdik
                string imgUrl = product.ProductImages?.FirstOrDefault(pi => pi.IsPrimary == true)?.ImageURL
                                ?? product.ProductImages?.FirstOrDefault()?.ImageURL
                                ?? "default-product.jpg";

                _cartItems.Add(new CartVM
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price + sizePrice,
                    Quantity = Quantity,
                    Size = Size,
                    ImageUrl = imgUrl
                });
            }

            return RedirectToAction("Cart");
        }

        // 3. SAYI ARTIRMAQ
        public IActionResult Increase(int productId, string size)
        {
            var item = _cartItems.FirstOrDefault(x => x.ProductId == productId && x.Size == size);
            if (item != null)
            {
                item.Quantity++;
            }
            return RedirectToAction("Cart");
        }

        // 4. SAYI AZALTMAQ
        public IActionResult Decrease(int productId, string size)
        {
            var item = _cartItems.FirstOrDefault(x => x.ProductId == productId && x.Size == size);
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
            }
            return RedirectToAction("Cart");
        }

        // 5. SƏBƏTDƏN SİLMƏK
        public IActionResult Remove(int productId, string size)
        {
            var item = _cartItems.FirstOrDefault(x => x.ProductId == productId && x.Size == size);
            if (item != null)
            {
                _cartItems.Remove(item);
            }
            return RedirectToAction("Cart");
        }
    }
}