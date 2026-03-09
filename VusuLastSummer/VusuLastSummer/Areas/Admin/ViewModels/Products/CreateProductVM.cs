using System.ComponentModel.DataAnnotations;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.ViewModels.Products
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Məhsulun adı mütləqdir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Qiymət mütləqdir")]
        public decimal? Price { get; set; }

        public string Description { get; set; }
       

        [Required(ErrorMessage = "Kateqoriya seçilməlidir")]
        public int? CategoryId { get; set; }

        public List<int>? TagIds { get; set; }

        // Əsas dəyişiklik burdadır: Tək şəkil tələb edirik
        [Required(ErrorMessage = "Şəkil yükləmək məcburidir")]
        public IFormFile Photo { get; set; }

        // Dropdown üçün listlər
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}
