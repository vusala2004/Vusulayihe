using VusuLastSummer.Models.Base;

namespace VusuLastSummer.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public bool IsFeatured { get; set; }

        // Kategoriya əlaqəsi
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // SƏNİN YAZDIĞIN YENİ ƏLAQƏLƏR:
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public List<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

    }
}
