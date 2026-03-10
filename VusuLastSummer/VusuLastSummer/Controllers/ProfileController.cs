using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.Models;
using VusuLastSummer.ViewModels.Profile;

namespace VusuLastSummer.Controllers
{
    [Authorize] // Profilə yalnız giriş edənlər baxa bilsin
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index() // Metodun adını Index etmək daha standartdır
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var model = new ProfileVM
            {
                Name = $"{user.Name} {user.Surname}",
                Email = user.Email,
                // Digər datalar hələlik statik qala bilər, bazanı qurduqca dəyişəcəyik
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
            if (ModelState.IsValid)
            {
                // Bura gələcəkdə bazada yeniləmə kodunu yazacağıq
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
