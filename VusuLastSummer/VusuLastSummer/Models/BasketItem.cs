using VusuLastSummer.Models.Base;

namespace VusuLastSummer.Models
{
    public class BasketItem:BaseEntity
    {
        // Hansı səbətin içindədir?
        public int BasketId { get; set; }
        public Basket? Basket { get; set; }

        // Səbətə hansı məhsul atılıb?
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // Neçə ədəd istəyir?
        public int Quantity { get; set; }

        // Məhsulun ölçüsü (Məs: Small, Medium, Large)
        public string Size { get; set; } = "Medium";
    }
}
