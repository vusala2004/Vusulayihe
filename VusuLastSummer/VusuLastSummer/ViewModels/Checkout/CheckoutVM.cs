namespace VusuLastSummer.ViewModels.Checkout
{
    public class CheckoutVM
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string DeliveryMethod { get; set; } = "pickup"; // pickup və ya delivery
        public string? Address { get; set; }
        public string? City { get; set; }

        // Sifariş xülasəsini (Order Summary) göstərmək üçün hələlik dummy rəqəmlər veririk
        public decimal Subtotal { get; set; } = 11.50m;
        public decimal DeliveryFee { get; set; } = 3.00m;
        public decimal Total => DeliveryMethod == "delivery" ? Subtotal + DeliveryFee : Subtotal;
    }
}
