using VusuLastSummer.Models.Base;

namespace VusuLastSummer.Models
{
    public class Reservation:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime ReservationDate { get; set; }
        public string Time { get; set; } = string.Empty;
        public int Guests { get; set; }
        public string? SpecialRequests { get; set; }
        public VusuLastSummer.Enums.ReservationStatus Status { get; set; }
    }
}
