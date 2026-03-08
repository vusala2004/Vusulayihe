using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.ContactVM;

namespace VusuLastSummer.Controllers
{
    public class ContactController : Controller
    {
        // Səhifə açılanda bu işləyir
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // Düyməyə basıb form göndəriləndə bu işləyir
        [HttpPost]
        public IActionResult Contact(ContactVM model)
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
