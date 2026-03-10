using System.ComponentModel.DataAnnotations;

namespace VusuLastSummer.ViewModels.Account
{
    public class RegisterVM
    {
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(20)]
        public string Surname { get; set; }
        [MinLength(4)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
