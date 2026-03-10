using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.DAL;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.Controllers
{
    // Yalnız sistemə giriş edən (Login olan) istifadəçilər səbətə nəsə ata bilər
    [Authorize]
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1. Sistemə daxil olmuş istifadəçini tapırıq
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Index", "Home"); // Login deyilsə ana səhifəyə at

            // 2. Bu istifadəçinin səbətini, içindəki məhsulları (BasketItems) və o məhsulların detallarını (Product) gətiririk
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product) // Məhsulun adını, qiymətini, şəklini görmək üçün lazımdır
                .FirstOrDefaultAsync(b => b.AppUserId == user.Id);

            // Əgər istifadəçinin heç səbəti yoxdursa, boş bir səbət modeli göndəririk ki, error verməsin
            if (basket == null)
            {
                basket = new Basket { BasketItems = new List<BasketItem>() };
            }

            return View(basket);
        }

        // Məhsulu səbətə əlavə etmək metodu
        [HttpPost]
        public async Task<IActionResult> AddToBasket(int productId)
        {
            // 1. Sistəmə daxil olmuş istifadəçini tapırıq
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // 2. Bu istifadəçinin bazada səbəti varmı deyə yoxlayırıq
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.AppUserId == user.Id);

            // Əgər istifadəçinin səbəti yoxdursa, ona təzə səbət yaradırıq
            if (basket == null)
            {
                basket = new Basket { AppUserId = user.Id };
                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync(); // Səbətin İD-si yaranması üçün yadda saxlayırıq
            }

            // 3. İstifadəçi bu məhsulu artıq səbətə atıb, yoxsa birinci dəfədir?
            var basketItem = basket.BasketItems.FirstOrDefault(bi => bi.ProductId == productId);

            if (basketItem != null)
            {
                // Əgər artıq səbətdə varsa, sadəcə sayını (Quantity) 1 vahid artırırıq
                basketItem.Quantity++;
            }
            else
            {
                // Əgər səbətdə yoxdursa, yeni məhsul kimi əlavə edirik
                basketItem = new BasketItem
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1,
                    Size = "Medium" // "Size" məntiqini qurana qədər hər kəsə standart "Medium" kofe veririk :)
                };
                await _context.BasketItems.AddAsync(basketItem);
            }

            // Dəyişiklikləri bazaya yazırıq
            await _context.SaveChangesAsync();

            // Səbətə atandan sonra istifadəçini gəldiyi səhifəyə qaytarırıq (və ya Home səhifəsinə)
            return RedirectToAction("Index", "Home");
        }
    }
}
