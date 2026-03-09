using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.Areas.Admin.ViewModels.Categories;
using VusuLastSummer.DAL;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin,Moderator,Member")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. SİYAHI (INDEX)
        // ==========================================
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories
                .Include(c => c.Products)
                .Where(c => c.IsDeleted == false) // Yalnız silinməyənləri gətir
                .ToListAsync();

            return View(categories);
        }

        // ==========================================
        // 2. ƏTRAFLI BAXIŞ (DETAILS)
        // ==========================================
       // [Authorize(Roles = "Admin,Moderator,Member")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Category existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existCategory is null) return NotFound();

            return View(existCategory);
        }

        // ==========================================
        // 3. YARATMA GET
        // ==========================================
        [HttpGet]
       // [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        // ==========================================
        // 4. YARATMA POST
        // ==========================================
        [HttpPost]
        // [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Create(CategoryCreateVM vm) // <-- Artıq Category yox, VM qəbul edirik
        {
            if (!ModelState.IsValid) return View(vm);

            bool existCategory = await _context.Categories.AnyAsync(c => c.Name.Trim() == vm.Name.Trim());

            if (existCategory)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya artıq mövcuddur!");
                return View(vm);
            }

            // VM-dən gələn datanı əsl Category modelinə çeviririk (Mapping)
            Category category = new Category
            {
                Name = vm.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // ==========================================
        // 5. YENİLƏMƏ GET
        // ==========================================
        // UPDATE GET
        // [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Category existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existCategory is null) return NotFound();

            // Əsl modeli VM-ə çevirib View-a göndəririk
            CategoryUpdateVM vm = new CategoryUpdateVM
            {
                Name = existCategory.Name
            };

            return View(vm);
        }

        // UPDATE POST
        [HttpPost]
        // [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm) // <-- VM qəbul edirik
        {
            if (id is null || id < 1) return BadRequest();
            Category existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existCategory is null) return NotFound();

            if (!ModelState.IsValid) return View(vm);

            bool isExistCategory = await _context.Categories.AnyAsync(c => c.Name.Trim() == vm.Name.Trim() && c.Id != id);

            if (isExistCategory)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya artıq mövcuddur!");
                return View(vm);
            }

            // VM-dəki yeni adı əsl modelə mənimsədirik
            existCategory.Name = vm.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // ==========================================
        // 7. SİLMƏ (SOFT DELETE) - GET və POST əvəzinə birbaşa silmə
        // ==========================================
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Category existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existCategory is null) return NotFound();

            // Məntiqi silmə (Soft Delete)
            existCategory.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
