using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.Areas.Admin.ViewModels.Dashboard;
using VusuLastSummer.DAL;

namespace VusuLastSummer.Area.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Bütün statistikaları bazadan çəkirik
            DashboardVM vm = new DashboardVM
            {
                ActiveUsers = await _context.Users.CountAsync(),
                TotalOrders = await _context.Orders.CountAsync(),

                // Qazancı yalnız statusu "Delivered" (Çatdırıldı) olanlardan hesablayırıq
                TotalEarnings = await _context.Orders
                                    .Where(o => o.Status == "Delivered")
                                    .SumAsync(o => o.TotalAmount),

                // Yalnız "Gözləyir" statusunda olan rezervasiyaları sayırıq
                PendingReservations = await _context.Reservations
    .Where(r => r.Status == VusuLastSummer.Enums.ReservationStatus.Pending)
    .CountAsync(),

                TotalProducts = await _context.Products.CountAsync(),

                // Ən son gələn 5 sifarişi gətiririk
                RecentOrders = await _context.Orders
                                   .OrderByDescending(o => o.Id)
                                   .Take(5)
                                   .ToListAsync()
            };

            return View(vm);
        }
    }
}
