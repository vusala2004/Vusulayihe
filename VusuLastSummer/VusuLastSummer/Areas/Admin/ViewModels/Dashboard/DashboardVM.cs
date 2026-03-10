using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.ViewModels.Dashboard
{
    public class DashboardVM
    {
        public int TotalOrders { get; set; } // Ümumi sifarişlərin sayı
        public decimal TotalEarnings { get; set; } // Ümumi qazanc (Yalnız Çatdırılanlardan)
        public int PendingReservations { get; set; } // Gözləyən rezervasiyalar
        public int TotalProducts { get; set; } // Baza daxilindəki məhsul sayı

        // Ana səhifədə ən son gələn 5 sifarişi cədvəldə göstərmək üçün
        public List<Order> RecentOrders { get; set; } = new List<Order>();
        public int ActiveUsers { get; set; }
    }
}
