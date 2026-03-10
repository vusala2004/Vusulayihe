using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.Areas.Admin.ViewModels.Blogs;
using VusuLastSummer.DAL;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogPostController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // 1. Məqalələrin Siyahısı
        public async Task<IActionResult> Index()
        {
            // Include(b => b.BlogCategory) yazırıq ki, cədvəldə kateqoriyanın adı da görünsün
            var posts = await _context.BlogPosts.Include(b => b.BlogCategory).OrderByDescending(p => p.Id).ToListAsync();
            return View(posts);
        }

        // 2. Yeni Məqalə Yaratmaq Səhifəsi (GET)
        public async Task<IActionResult> Create()
        {
            // Səhifə açılanda Select (dropdown) üçün kateqoriyaları bazadan göndəririk
            ViewBag.Categories = new SelectList(await _context.BlogCategories.ToListAsync(), "Id", "Name");
            return View();
        }

        // 3. Məqaləni Bazaya Yazmaq və Şəkli Yükləmək (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostCreateVM vm)
        {
            // Əgər nəsə səhvdirsə, kateqoriyaları yenidən göndərib səhifəni qaytarırıq
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _context.BlogCategories.ToListAsync(), "Id", "Name");
                return View(vm);
            }

            // Şəkil yükləmə prosesi (Kompüterdən wwwroot/assets/images qovluğuna kopyalayırıq)
            string fileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", "blog", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await vm.Image.CopyToAsync(stream);
            }

            // VM-dən gələn məlumatları əsl BlogPost modelinə keçiririk
            BlogPost post = new BlogPost
            {
                Title = vm.Title,
                Slug = vm.Title.Trim().ToLower().Replace(" ", "-"),
                Content = vm.Content,
                Excerpt = vm.Excerpt,
                BlogCategoryId = vm.BlogCategoryId,
                ImageUrl = fileName, // Yalnız şəklin adını bazaya yazırıq
                IsPublished = vm.IsPublished,
                PublishedAt = vm.IsPublished ? DateTime.Now : default // Əgər paylaşıldısa vaxtı qeyd edir
            };

            await _context.BlogPosts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
