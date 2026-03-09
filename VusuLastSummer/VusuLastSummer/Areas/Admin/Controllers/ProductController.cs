using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.Areas.Admin.ViewModels.Products;
using VusuLastSummer.DAL;
using VusuLastSummer.Enums;
using VusuLastSummer.Extensions;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.Controllers
{
    namespace VusuLastSummer.Areas.Admin.Controllers
    {
        [Area("Admin")]
        // [Authorize(Roles = "Admin,Moderator,Member")]
        public class ProductController : Controller
        {
            private readonly AppDbContext _context;
            private readonly IWebHostEnvironment _env;

            public ProductController(AppDbContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }

            // ==========================================
            // 1. SİYAHI (INDEX)
            // ==========================================
            public async Task<IActionResult> Index()
            {
                List<GetProductVM> getProductVMs = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Select(p => new GetProductVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        CategoryName = p.Category.Name,
                        // Şəkil null ola bilər ehtimalını yoxlayırıq
                        ImageURL = p.ProductImages.FirstOrDefault() != null ? p.ProductImages.FirstOrDefault().ImageURL : "no-image.jpg"
                    })
                    .ToListAsync();

                return View(getProductVMs);
            }

            // ==========================================
            // 2. ƏTRAFLI BAXIŞ (DETAILS)
            // ==========================================
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || id < 1) return BadRequest();

                Product product = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null) return NotFound();

                return View(product);
            }

            // ==========================================
            // 3. YARATMA (GET)
            // ==========================================
            public async Task<IActionResult> Create()
            {
                CreateProductVM createProductVM = new()
                {
                    Categories = await _context.Categories.ToListAsync(),
                    Tags = await _context.Tags.ToListAsync()
                };
                return View(createProductVM);
            }

            // ==========================================
            // 4. YARATMA (POST)
            // ==========================================
            [HttpPost]
            public async Task<IActionResult> Create(CreateProductVM createProductVM)
            {
                createProductVM.Categories = await _context.Categories.ToListAsync();
                createProductVM.Tags = await _context.Tags.ToListAsync();

                if (createProductVM.Price < 0)
                {
                    ModelState.AddModelError(nameof(createProductVM.Price), "Qiymət mənfi ola bilməz!");
                    return View(createProductVM);
                }

                if (!ModelState.IsValid) return View(createProductVM);

                // Tək şəklin (Photo) yoxlanışı
                if (!createProductVM.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError(nameof(createProductVM.Photo), "Fayl tipi yalnız şəkil olmalıdır!");
                    return View(createProductVM);
                }
                if (!createProductVM.Photo.CheckFileSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(createProductVM.Photo), "Şəkil maksimum 2MB ola bilər!");
                    return View(createProductVM);
                }

                // Kateqoriya yoxlanışı
                bool existCategory = createProductVM.Categories.Any(c => c.Id == createProductVM.CategoryId);
                if (!existCategory)
                {
                    ModelState.AddModelError(nameof(createProductVM.CategoryId), "Belə bir kateqoriya mövcud deyil!");
                    return View(createProductVM);
                }

                // Şəklin Yüklənməsi 
                ProductImage image = new()
                {
                    ImageURL = await createProductVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "products"),
                    IsPrimary = true
                };

                // Məhsulun yaradılması (SKU Çıxarıldı)
                Product product = new()
                {
                    Name = createProductVM.Name,
                    Price = createProductVM.Price.Value,
                    Description = createProductVM.Description,
                    CategoryId = createProductVM.CategoryId.Value,
                    ProductImages = new List<ProductImage> { image }
                };

                if (createProductVM.TagIds is not null)
                {
                    product.ProductTags = createProductVM.TagIds.Select(tid => new ProductTag { TagId = tid }).ToList();
                }

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // ==========================================
            // 5. YENİLƏMƏ (GET)
            // ==========================================
            public async Task<IActionResult> Update(int? id)
            {
                if (id == null || id < 1) return BadRequest();
                Product product = await _context.Products
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductTags)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null) return NotFound();

                UpdateProductVM updateProduct = new()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Categories = await _context.Categories.ToListAsync(),
                    Tags = await _context.Tags.ToListAsync(),
                    // Null gələ bilmə ehtimalına qarşı yoxlanış
                    TagIds = product.ProductTags?.Select(pt => pt.TagId).ToList() ?? new List<int>(),
                    ProductImages = product.ProductImages
                };
                return View(updateProduct);
            }

            // ==========================================
            // 6. YENİLƏMƏ (POST)
            // ==========================================
            [HttpPost]
            public async Task<IActionResult> Update(int? id, UpdateProductVM updateProductVM)
            {
                if (id == null || id < 1) return BadRequest();
                Product existProduct = await _context.Products
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductTags)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existProduct == null) return NotFound();

                updateProductVM.Categories = await _context.Categories.ToListAsync();
                updateProductVM.Tags = await _context.Tags.ToListAsync();
                updateProductVM.ProductImages = existProduct.ProductImages;

                if (!ModelState.IsValid) return View(updateProductVM);

                // Şəkil yeniləmə yoxlanışı (Əgər yeni şəkil yüklənibsə)
                if (updateProductVM.Photo is not null)
                {
                    if (!updateProductVM.Photo.CheckFileType("image/")) { ModelState.AddModelError(nameof(updateProductVM.Photo), "Fayl tipi səhvdir!"); return View(updateProductVM); }
                    if (!updateProductVM.Photo.CheckFileSize(FileSize.MB, 2)) { ModelState.AddModelError(nameof(updateProductVM.Photo), "Şəkil çox böyükdür!"); return View(updateProductVM); }
                }

                if (existProduct.CategoryId != updateProductVM.CategoryId)
                {
                    bool existCategory = updateProductVM.Categories.Any(c => c.Id == updateProductVM.CategoryId);
                    if (!existCategory) { ModelState.AddModelError(nameof(updateProductVM.CategoryId), "Kateqoriya mövcud deyil!"); return View(updateProductVM); }
                }

                // ==========================================
                // TEQ YENİLƏNMƏSİ - XƏTA DÜZƏLDİ (.Exists əvəzinə .Contains və .Any yazıldı)
                // ==========================================
                updateProductVM.TagIds ??= new List<int>();
                updateProductVM.TagIds = updateProductVM.TagIds.Distinct().ToList();

                // Bazada olub, seçilənlər arasında OLMAYANLARI silirik
                var tagsToRemove = existProduct.ProductTags.Where(pt => !updateProductVM.TagIds.Contains(pt.TagId)).ToList();
                _context.ProductTags.RemoveRange(tagsToRemove);

                // Seçilənlər arasında olub, bazada OLMAYANLARI əlavə edirik
                var tagsToAdd = updateProductVM.TagIds
                    .Where(tid => !existProduct.ProductTags.Any(pt => pt.TagId == tid))
                    .Select(tid => new ProductTag { TagId = tid, ProductId = existProduct.Id })
                    .ToList();
                _context.ProductTags.AddRange(tagsToAdd);


                // Tək şəklin dəyişdirilməsi
                if (updateProductVM.Photo is not null)
                {
                    string fileName = await updateProductVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "products");

                    ProductImage oldImage = existProduct.ProductImages.FirstOrDefault();
                    if (oldImage != null)
                    {
                        oldImage.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "products");
                        existProduct.ProductImages.Remove(oldImage);
                    }

                    existProduct.ProductImages.Add(new ProductImage { ImageURL = fileName, IsPrimary = true });
                }

                existProduct.Name = updateProductVM.Name;
                existProduct.Price = updateProductVM.Price.Value;
                existProduct.Description = updateProductVM.Description;
                existProduct.CategoryId = updateProductVM.CategoryId.Value;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // ==========================================
            // 7. SİLMƏ 
            // ==========================================
            public async Task<IActionResult> Delete(int? id)
            {
                if (id is null || id < 1) return BadRequest();

                Product existProduct = await _context.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (existProduct is null) return NotFound();

                // Şəkli qovluqdan silirik
                var image = existProduct.ProductImages.FirstOrDefault();
                if (image != null)
                {
                    image.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "products");
                }

                _context.Products.Remove(existProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
