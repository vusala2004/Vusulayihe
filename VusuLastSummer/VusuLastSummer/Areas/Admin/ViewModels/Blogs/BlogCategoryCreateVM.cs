using System.ComponentModel.DataAnnotations;

namespace VusuLastSummer.Areas.Admin.ViewModels.Blogs
{
    public class BlogCategoryCreateVM
    {
        [Required(ErrorMessage = "Kateqoriya adı boş ola bilməz!")]
        [MaxLength(50, ErrorMessage = "Kateqoriya adı maksimum 50 simvol ola bilər!")]
        public string Name { get; set; }
    }
}
