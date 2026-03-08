namespace VusuLastSummer.ViewModels.Track
{
    public class TrackOrderVM
    {
        public string OrderNumber { get; set; } = string.Empty;
        // Status: "pending", "preparing", "on-the-way", "delivered"
        public string Status { get; set; } = "pending";
        public int EstimatedMinutes { get; set; } = 25;
        public bool IsValidOrder => !string.IsNullOrEmpty(OrderNumber);
    }
}
