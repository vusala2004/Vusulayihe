using System.ComponentModel.DataAnnotations;

namespace VusuLastSummer.Models
{
    public class Category
    {
        //[Required(ErrorMessage ="bos olmaz")]
        [MaxLength(20, ErrorMessage = "max 20 herf olmalidir!!")]
        public string? Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
