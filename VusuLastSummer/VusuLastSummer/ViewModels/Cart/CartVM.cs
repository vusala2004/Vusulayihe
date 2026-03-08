namespace VusuLastSummer.ViewModels.Cart
{
    public class CartVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Size { get; set; } = "Medium";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }

        // Məhsulun cəmi qiyməti (Qiymət * Say)
        public decimal Total => Price * Quantity;
    }
}
