using System.ComponentModel.DataAnnotations;
using VusuLastSummer.Models.Base;

namespace VusuLastSummer.Models
{
    public class Category:BaseEntity
    {
        //[Required(ErrorMessage ="bos olmaz")]
        [MaxLength(20, ErrorMessage = "max 20 herf olmalidir!!")]
        public string? Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
