namespace VusuLastSummer.Enums
{
    public enum OrderStatus
    {
        Pending,    // 0 - Gözləyir
        Processing, // 1 - Hazırlanır
        Shipped,    // 2 - Yoldadır (Kuryerdə)
        Delivered,  // 3 - Çatdırıldı
        Cancelled   // 4 - Ləğv edildi
    }
}
