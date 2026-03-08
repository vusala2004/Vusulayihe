using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.Cart;
using VusuLastSummer.ViewModels.Checkout;
using VusuLastSummer.ViewModels.Confirmation;
using VusuLastSummer.ViewModels.Payment;

namespace VusuLastSummer.Controllers
{
    public class CheckoutController : Controller
    {
        // 1. Səhifə açılanda bu işləyir
        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new CheckoutVM();
            return View(model);
        }

        // 2. Form doldurulub göndəriləndə bu işləyir
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            // Əgər formdakı məlumatlar düzgündürsə:
            if (ModelState.IsValid)
            {
                // Gələcəkdə bura: "Məlumatları Verilənlər Bazasında 'Orders' cədvəlinə yaz" məntiqi gələcək.

                // Hələlik sifariş bitdi deyə, istifadəçini Uğurlu səhifəsinə yönləndiririk
                return RedirectToAction("Success");
            }

            // Əgər nəsə səhvdirsə, eyni səhifəni xətalarla geri qaytar
            return View(model);
        }

        // Sadəcə təbrik səhifəsi
        public IActionResult Success()
        {
            var orderModel = new ConfirmationVM
            {
                OrderNumber = "ORD-" + new Random().Next(10000, 99999),
                EstimatedTime = "20-35 minutes",

                // Sifarişin içindəki məhsullar
                Items = new List<CartVM>
                {
                    new CartVM { ProductName = "Classic Espresso", Size = "Medium", Quantity = 2, Price = 3.50m },
                    new CartVM { ProductName = "Vanilla Latte", Size = "Large", Quantity = 1, Price = 4.50m }
                },

                Subtotal = 11.50m,
                DeliveryFee = 3.00m
            };

            return View(orderModel); // Əgər qovluq yaratmamısansa return View("~/Views/Success.cshtml", orderModel); yaz
        }
        // Ödəniş səhifəsini açır
        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }

        // 'Pay Now' düyməsinə basılanda işləyir
        [HttpPost]
        public IActionResult Payment(PaymentVM model)
        {
            if (ModelState.IsValid)
            {
                // Burada gələcəkdə Stripe və ya Kapital Bank/Paşa Bank API-na qoşulub ödənişi yoxlayacağıq.
                // Hələlik ödəniş uğurlu keçmiş kimi qəbul edib, birbaşa "Success" (Təsdiq) səhifəsinə yönləndiririk.

                return RedirectToAction("Success");
            }

            // Əgər formda xəta varsa, səhifəni yenidən eyni məlumatlarla geri qaytarırıq
            return View(model);
        }
    }
}
