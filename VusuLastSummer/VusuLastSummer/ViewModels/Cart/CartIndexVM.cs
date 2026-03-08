namespace VusuLastSummer.ViewModels.Cart
{
    public class CartIndexVM
    {
        public List<CartVM> CartItems { get; set; } = new List<CartVM>();

        // Alt-toplam (Bütün məhsulların cəmi)
        public decimal Subtotal => CartItems.Sum(x => x.Total);

        public decimal DeliveryFee { get; set; } = 3.00m;

        // Yekun məbləğ
        public decimal TotalAmount => Subtotal + DeliveryFee;
    }
}
