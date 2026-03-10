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
        }).ToList();

            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            // 1. Baza (SQL) -dən kliklənən məhsulu tapırıq
            var productFromDb = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            // Əgər belə bir məhsul yoxdursa və ya silinibbsə, 404 səhifəsi qaytarırıq
            if (productFromDb == null)
            {
                return NotFound();
            }

            // 2. Bənzər məhsulları (Related Products) tapırıq
            // Şərtimiz: Eyni kateqoriyada olsun, amma özü olmasın (p.Id != id) və cəmi 3 dənə gəlsin
            var relatedProductsDb = await _context.Products
                .Include(p => p.ProductImages)
                .Where(p => p.CategoryId == productFromDb.CategoryId && p.Id != id && !p.IsDeleted)
                .Take(3)
                .ToListAsync();

            // 3. Məlumatları bazadan alıb ViewModel-ə (HTML-ə gedəcək formata) köçürürük
            var productVM = new ProductVM
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Description = productFromDb.Description,
                Price = productFromDb.Price,
                Category = productFromDb.Category?.Name ?? "General",
                // Əsas şəklin adını tapırıq (heç biri yoxdursa default adı veririk)
                ImageUrl = productFromDb.ProductImages?.FirstOrDefault(pi => pi.IsPrimary.GetValueOrDefault())?.ImageURL
                           ?? productFromDb.ProductImages?.FirstOrDefault()?.ImageURL
                           ?? "default-product.jpg",

                // Bənzər məhsulları da öz siyahısına (List) yığırıq
                RelatedProducts = relatedProductsDb.Select(rp => new ProductVM
                {
                    Id = rp.Id,
                    Name = rp.Name,
                    Price = rp.Price,
                    ImageUrl = rp.ProductImages?.FirstOrDefault(pi => pi.IsPrimary.GetValueOrDefault())?.ImageURL
                               ?? rp.ProductImages?.FirstOrDefault()?.ImageURL
                               ?? "default-product.jpg"
                }).ToList()
            };

            return View(productVM);
        }
    }
}
