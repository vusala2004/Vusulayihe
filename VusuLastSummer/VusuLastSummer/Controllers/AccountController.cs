using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VusuLastSummer.Models;
using VusuLastSummer.ViewModels.Account;

namespace VusuLastSummer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new()
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            //await _userManager.AddToRoleAsync(appUser, UserRole.Member.ToString());



            return RedirectToAction(nameof(HomeController.Index), "Home");


        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginVM.UsernameorEmail || u.Email == loginVM.UsernameorEmail);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Username , Password or Email is incorrect");
                return View();
            }


            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsPersistent, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your account is blocked ,Please try later");
                return View();
            }



            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username, Password or Email is incorrect");
                return View();
            }


            //AppUser user = await _userManager.FindByNameAsync(loginVM.UsernameorEmail);
            //if (user is null)
            //{
            //    user = await _userManager.FindByEmailAsync(loginVM.UsernameorEmail);
            //    if (user is null)
            //    {
            //        ModelState.AddModelError(string.Empty, "Username or Email is incorrect");
            //    }
            //}

            if (returnUrl is null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }


            return Redirect(returnUrl);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        //public async Task<IActionResult> CreateRoles()
        //{

        //    foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }

        //    }
        //    return RedirectToAction(nameof(HomeController.Index), "Home");
        //}

    }
}

