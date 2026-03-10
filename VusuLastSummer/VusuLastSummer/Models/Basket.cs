using VusuLastSummer.Models.Base;

namespace VusuLastSummer.Models
{
    public class Basket:BaseEntity
    {
        // Səbət hansı istifadəçiyə (User-ə) aiddir?
        public string AppUserId { get; set; } = string.Empty;
        public AppUser? AppUser { get; set; }

        // Səbətin içindəki məhsulların siyahısı
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
