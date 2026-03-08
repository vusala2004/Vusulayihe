using Microsoft.AspNetCore.Mvc;
using VusuLastSummer.ViewModels.Track;

namespace VusuLastSummer.Controllers
{
    public class TrackController : Controller
    {
        // URL'den sipariş numarası alabiliriz: /Track?orderNumber=ORD-54892
        public IActionResult Track(string? orderNumber)
        {
            // Eğer sipariş numarası yoksa, kullanıcıya son aktif siparişini gösterebiliriz.
            // Şimdilik test amaçlı rastgele bir sipariş numarası üretiyoruz:
            if (string.IsNullOrEmpty(orderNumber))
            {
                orderNumber = "ORD-" + new Random().Next(10000, 99999);
            }

            var model = new TrackOrderVM
            {
                OrderNumber = orderNumber,
                Status = "pending", // Başlangıç durumu
                EstimatedMinutes = 25
            };

            return View(model);
        }
    }
}
