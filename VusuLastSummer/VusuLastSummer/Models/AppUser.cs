using Microsoft.AspNetCore.Identity;

namespace VusuLastSummer.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
