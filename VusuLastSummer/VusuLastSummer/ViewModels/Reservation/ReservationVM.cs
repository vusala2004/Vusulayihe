namespace VusuLastSummer.ViewModels.Reservation
{
    public class ReservationVM
    {
        // Form sahələri
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Today;
        public string Time { get; set; } = string.Empty;
        public int Guests { get; set; } = 2;
        public string? Requests { get; set; }

        // Keçmiş rezervasiyaların siyahısı
        public List<ReservationHistoryVM> History { get; set; } = new List<ReservationHistoryVM>();
    }
}
