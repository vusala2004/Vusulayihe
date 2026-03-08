namespace VusuLastSummer.ViewModels.Reservation
{
    public class ReservationHistoryVM
    {
        public DateTime Date { get; set; }
        public string Time { get; set; } = string.Empty;
        public int Guests { get; set; }
        public string Status { get; set; } = "Confirmed";
    }
}
