using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.Profile;

namespace VusuLastSummer.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Gələcəkdə bu məlumatlar Login olmuş istifadəçinin bazadakı məlumatları olacaq.
            // Hələlik test etmək üçün saxta data göndəririk:
            var model = new ProfileVM
            {
                Name = "John Doe",
                Email = "john@example.com",
                Phone = "+1 234 567 890",
                Birthday = new DateTime(1995, 5, 15),
                LoyaltyPoints = 120,
                Orders = new List<UserOrderVM>
                {
                    new UserOrderVM { OrderNumber = "54892", Timestamp = DateTime.Now.AddDays(-2), Status = "Delivered", Total = 14.50m, ItemCount = 3 },
                    new UserOrderVM { OrderNumber = "12044", Timestamp = DateTime.Now.AddDays(-15), Status = "Delivered", Total = 8.00m, ItemCount = 2 }
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ProfileVM model)
        {
            // Formdan gələn məlumatları qəbul edib bazada yeniləyəcəyimiz yer
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
