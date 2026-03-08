namespace VusuLastSummer.ViewModels.Payment
{
    public class PaymentVM
    {
        public string CardNumber { get; set; } = string.Empty;
        public string CardHolder { get; set; } = string.Empty;
        public string Expiry { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
    }
}
