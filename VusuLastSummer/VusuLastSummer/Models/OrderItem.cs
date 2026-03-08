using VusuLastSummer.Models.Base;

namespace VusuLastSummer.Models
{
    public class OrderItem:BaseEntity
    {
        // Hansı sifarişə aiddir?
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        // Hansı məhsuldur?
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Məhsulun ölçüsü: "Small", "Medium", "Large"
        public string Size { get; set; } = "Medium";
    }
}
