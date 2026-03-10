using System.ComponentModel.DataAnnotations;

namespace VusuLastSummer.Areas.Admin.ViewModels.Blogs
{
    public class BlogPostCreateVM
    {
        [Required(ErrorMessage = "Başlıq boş ola bilməz!")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Məqalənin mətni boş ola bilməz!")]
        public string Content { get; set; } = string.Empty;

        public string? Excerpt { get; set; } // Qısa məzmun (məcburi deyil)

        [Required(ErrorMessage = "Zəhmət olmasa kateqoriya seçin!")]
        public int BlogCategoryId { get; set; }

        [Required(ErrorMessage = "Şəkil mütləq yüklənməlidir!")]
        public IFormFile Image { get; set; } = null!; // Şəkli qəbul etmək üçün

        public bool IsPublished { get; set; } // Birbaşa saytda paylaşılsın, yoxsa qaralama qalsın?
    }
}
