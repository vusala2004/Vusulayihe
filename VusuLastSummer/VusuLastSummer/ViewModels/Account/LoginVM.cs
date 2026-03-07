using System.ComponentModel.DataAnnotations;

namespace VusuLastSummer.ViewModels.Account
{
    public class LoginVM
    {
        [MinLength(4)]
        [MaxLength(100)]
        public string UsernameorEmail { get; set; }
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
