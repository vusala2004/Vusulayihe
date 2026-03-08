namespace VusuLastSummer.ViewModels.Profile
{
    public class ProfileVM
    {
        // Ekranda adın baş hərfini göstərmək üçün (Məsələn: Vüsal -> V)
        public string Initials => string.IsNullOrWhiteSpace(Name) ? "U" : Name.Substring(0, 1).ToUpper();

        public int LoyaltyPoints { get; set; }

        // Form üçün lazımlı sahələr
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime? Birthday { get; set; }

        // Sifariş tarixçəsi
        public List<UserOrderVM> Orders { get; set; } = new List<UserOrderVM>();
    }
}

