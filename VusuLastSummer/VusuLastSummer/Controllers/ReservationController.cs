using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.Reservation;

namespace VusuLastSummer.Controllers
{
    public class ReservationController : Controller
    {
        [HttpGet]
        public IActionResult Index() // Metodun adı Index oldu
        {
            var model = new ReservationVM
            {
                // Səhifə yüklənəndə default olaraq bu günün tarixini təyin edirik
                Date = DateTime.Today,
                History = new List<ReservationHistoryVM>
                {
                    new ReservationHistoryVM { Date = DateTime.Today.AddDays(-5), Time = "18:00", Guests = 2, Status = "Completed" },
                    new ReservationHistoryVM { Date = DateTime.Today.AddDays(2), Time = "19:30", Guests = 4, Status = "Upcoming" }
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ReservationVM model) // Metodun adı Index oldu
        {
            // 1-ci Müdafiə: Server tərəfdə tarix yoxlanışı (Keçmiş tarix seçilə bilməz)
            if (model.Date.Date < DateTime.Today)
            {
                ModelState.AddModelError("Date", "You cannot select a past date for reservation.");
            }

            if (ModelState.IsValid)
            {
                // Gələcəkdə burada məlumatları SQL bazasına yazacağıq.
                TempData["SuccessMessage"] = "Table successfully reserved! We look forward to seeing you.";
                return RedirectToAction("Index");
            }

            // Xəta varsa, səhifəni yenidən qaytar
            model.History = new List<ReservationHistoryVM>();
            return View(model);
        }
    }
}