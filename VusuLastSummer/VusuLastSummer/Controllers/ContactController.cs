using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.ContactVM;

namespace VusuLastSummer.Controllers
{
    public class ContactController : Controller
    {
        // Səhifə açılanda bu işləyir (Metodun adı Index edildi)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Düyməyə basıb form göndəriləndə bu işləyir (Metodun adı Index edildi)
        [HttpPost]
        public IActionResult Index(ContactVM model)
        {
            if (ModelState.IsValid)
            {
                // Gələcəkdə bu mesajı Verilənlər Bazasında (məs. ContactMessages cədvəlinə) yazacağıq.
                // Hələlik mesajın uğurla getdiyini bildirmək üçün TempData istifadə edirik:

                TempData["SuccessMessage"] = "Mesajınız uğurla göndərildi! Tezliklə sizinlə əlaqə saxlayacağıq.";

                // Formu təmizləmək üçün səhifəni yenidən yükləyirik
                return RedirectToAction("Index");
            }

            // Əgər nəsə səhvdirsə (məsələn boş xana varsa), eyni səhifəni xətalarla geri qaytar
            return View(model);
        }
    }
}
