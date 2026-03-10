using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.Models;

namespace VusuLastSummer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        // Identity-nin bizə verdiyi xüsusi UserManager servisini istifadə edirik
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Bütün istifadəçiləri bazadan çəkirik
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
    }
}
