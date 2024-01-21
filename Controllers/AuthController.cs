using AynarMVC_Exam.Helpers.Enums;
using AynarMVC_Exam.Models;
using AynarMVC_Exam.ViewModels.AuthVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AynarMVC_Exam.Controllers
{
    public class AuthController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = new AppUser
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Email = vm.Email,
                UserName = vm.Username,
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.Member));
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail) ?? await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username/Email or Password is wrong!");
                return View(vm);
            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username/Email or Password is wrong!");
                return View(vm);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
