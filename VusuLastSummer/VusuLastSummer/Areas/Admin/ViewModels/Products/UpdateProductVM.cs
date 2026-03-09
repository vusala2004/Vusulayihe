using System.ComponentModel.DataAnnotations;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.ViewModels.Products
{
    public class UpdateProductVM
    {
        [Required(ErrorMessage = "Məhsulun adı mütləqdir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Qiymət mütləqdir")]
        public decimal? Price { get; set; }

        public string Description { get; set; }
 

        [Required(ErrorMessage = "Kateqoriya seçilməlidir")]
        public int? CategoryId { get; set; }

        public List<int>? TagIds { get; set; }

        // Yeniləmədə şəkil yükləmək məcburi deyil (null ola bilər)
        public IFormFile? Photo { get; set; }

        // Köhnə şəkli View-da göstərmək üçün
        public ICollection<ProductImage>? ProductImages { get; set; }

        // Dropdown üçün listlər
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}
