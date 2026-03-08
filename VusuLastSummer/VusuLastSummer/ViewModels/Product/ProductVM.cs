namespace VusuLastSummer.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        // Bənzər məhsullar üçün eyni modeldən ibarət bir list yaradırıq
        public List<ProductVM> RelatedProducts { get; set; } = new List<ProductVM>();
    }
}
