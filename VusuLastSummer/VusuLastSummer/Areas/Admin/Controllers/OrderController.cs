using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.DAL;

namespace VusuLastSummer.Areas.Admin.Controllers
{
   
        [Area("Admin")]
        public class OrderController : Controller
        {
            private readonly AppDbContext _context;

            public OrderController(AppDbContext context)
            {
                _context = context;
            }

            // 1. Sifarişlərin Siyahısı
            public async Task<IActionResult> Index()
            {
                // Ən son gələn sifarişlər yuxarıda görünsün
                var orders = await _context.Orders.OrderByDescending(o => o.Id).ToListAsync();
                return View(orders);
            }

            // 2. Sifarişin Detallarına və İçindəkilərə Baxmaq
            public async Task<IActionResult> Details(int id)
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems) // Sifarişin içindəki detalları gətirir
                    .ThenInclude(oi => oi.Product) // O detalların içindən də məhsulun məlumatlarını (adını) gətirir
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null) return NotFound();

                return View(order);
            }

            // 3. Sifarişin Statusunu Dəyişmək
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ChangeStatus(int id, string status)
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null) return NotFound();

                order.Status = status; // "Pending", "Preparing", "OnTheWay", "Delivered"
                await _context.SaveChangesAsync();

                // Statusu dəyişəndən sonra yenə həmin sifarişin detal səhifəsinə qayıdırıq
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }
        }
    }

