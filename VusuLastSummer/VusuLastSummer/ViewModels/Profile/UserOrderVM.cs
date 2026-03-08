namespace VusuLastSummer.ViewModels.Profile
{
    public class UserOrderVM
    {
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
    }
}
