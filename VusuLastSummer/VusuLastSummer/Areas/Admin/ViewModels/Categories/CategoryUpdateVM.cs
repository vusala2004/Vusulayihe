using System.ComponentModel.DataAnnotations;

namespace VusuLastSummer.Areas.Admin.ViewModels.Categories
{
    public class CategoryUpdateVM
    {
        [Required(ErrorMessage = "Kateqoriya adı boş ola bilməz!")]
        [MaxLength(50, ErrorMessage = "Ad maksimum 50 simvol ola bilər!")]
        public string Name { get; set; }
    }
}
