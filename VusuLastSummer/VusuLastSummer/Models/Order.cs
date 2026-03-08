using VusuLastSummer.Models.Base;

namespace VusuLastSummer.Models
{
    public class Order:BaseEntity
    {
        public string OrderNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string DeliveryMethod { get; set; } = "pickup";
        public string? Address { get; set; }
        public string? City { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Preparing, OnTheWay, Delivered

        // Sifarişdəki məhsulların siyahısı
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
