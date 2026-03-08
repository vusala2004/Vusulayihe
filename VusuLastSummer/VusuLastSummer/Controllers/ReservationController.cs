using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.Reservation;

namespace VusuLastSummer.Controllers
{
    public class ReservationController : Controller
    {
        [HttpGet]
        public IActionResult Reservation()
        {
            // Gələcəkdə bu tarixçə bazadan gələcək.
            // Hələlik istifadəçiyə əvvəlki rezervasiyalarını göstərmək üçün saxta data:
            var model = new ReservationVM
            {
                History = new List<ReservationHistoryVM>
                {
                    new ReservationHistoryVM { Date = DateTime.Today.AddDays(-5), Time = "18:00", Guests = 2, Status = "Completed" },
                    new ReservationHistoryVM { Date = DateTime.Today.AddDays(2), Time = "19:30", Guests = 4, Status = "Upcoming" }
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Reservation(ReservationVM model)
        {
            if (ModelState.IsValid)
            {
                // Gələcəkdə burada məlumatları SQL bazasına yazacağıq.
                TempData["SuccessMessage"] = "Table successfully reserved! We look forward to seeing you.";
                return RedirectToAction("Index");
            }

            // Xəta varsa, səhifəni yenidən qaytar (tarixçəni boş qoymamaq üçün təkrar yükləmək lazımdır)
            model.History = new List<ReservationHistoryVM>();
            return View(model);
        }
    }
}
