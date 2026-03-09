using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.Areas.Admin.ViewModels.Blogs;
using VusuLastSummer.DAL;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.Controllers
{
    [Area("Admin")] // Mütləq bunu yazırıq ki, proqram bunun Admin panelə aid olduğunu bilsin
    public class BlogCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public BlogCategoryController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Kateqoriyaların Siyahısı (Index)
        public async Task<IActionResult> Index()
        {
            var categories = await _context.BlogCategories.OrderByDescending(c => c.Id).ToListAsync();
            return View(categories);
        }

        // 2. Yeni Kateqoriya Yaratmaq səhifəsini açmaq (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 3. Yeni Kateqoriyanı bazaya yadda saxlamaq (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // Eyni adda kateqoriya olub-olmadığını yoxlayaq
            bool isExist = await _context.BlogCategories.AnyAsync(c => c.Name.ToLower() == vm.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda bloq kateqoriyası artıq mövcuddur!");
                return View(vm);
            }

            // VM-dən gələn məlumatları Əsl Modelə (BlogCategory) köçürürük
            BlogCategory category = new BlogCategory
            {
                Name = vm.Name,
                Slug = vm.Name.Trim().ToLower().Replace(" ", "-") // Slug avtomatik yaranır
            };

            await _context.BlogCategories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        
        }

        // Silmək (Delete)
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.BlogCategories.FindAsync(id);
            if (category == null) return NotFound();

            _context.BlogCategories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
