using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.DAL;
using VusuLastSummer.ViewModels;
using VusuLastSummer.ViewModels.Blog;

namespace VusuLastSummer.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // Bütün kateqoriyaları və ancaq paylaşılan postları çəkirik
            var model = new BlogIndexVM
            {
                Categories = await _context.BlogCategories.ToListAsync(),

                Posts = await _context.BlogPosts
                    .Include(p => p.BlogCategory) // Kateqoriya adını göstərmək üçün Include edirik
                    .Where(p => p.IsPublished)
                    .OrderByDescending(p => p.PublishedAt)
                    .ToListAsync()
            };

            return View();
        }
    }
}
