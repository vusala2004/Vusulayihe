using VusuLastSummer.ViewModels.Cart;

namespace VusuLastSummer.ViewModels.Confirmation
{
    public class ConfirmationVM
    {
        public string OrderNumber { get; set; } = string.Empty;
        public string EstimatedTime { get; set; } = "20-35 minutes";

        // Sifariş edilən məhsullar (Bayaq yaratdığımız CartItemVM-dən istifadə edirik)
        public List<CartVM> Items { get; set; } = new List<CartVM>();

        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total => Subtotal + DeliveryFee;
    }
}
