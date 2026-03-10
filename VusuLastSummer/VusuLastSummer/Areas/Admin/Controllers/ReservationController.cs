using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.DAL;
using VusuLastSummer.Enums;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Gələn Rezervasiyaların Siyahısı
        public async Task<IActionResult> Index()
        {
            // Ən son gələn rezervasiyalar ən üstdə görünsün
            var reservations = await _context.Reservations.OrderByDescending(r => r.Created).ToListAsync();
            return View(reservations);
        }
        // Yeni rezervasiya səhifəsini açmaq üçün (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Yeni rezervasiyanı bazaya yazmaq üçün (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
                return View(reservation);

            reservation.Created = DateTime.Now;

            // Admin özü yaradanda istəsə statusu dərhal "Təsdiqləndi" edə bilər, 
            // amma biz hələlik modeldəki standart (Pending) dəyərini saxlayırıq.

            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 2. Statusu Dəyişmək (Təsdiqlə və ya Ləğv et)
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, ReservationStatus status)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            reservation.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 3. Rezervasiyanı Silmək
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
