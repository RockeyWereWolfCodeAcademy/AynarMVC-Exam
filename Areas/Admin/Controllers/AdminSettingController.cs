using AynarMVC_Exam.Areas.Admin.ViewModels.AdminSetting;
using AynarMVC_Exam.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AynarMVC_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminSettingController : Controller
    {
        readonly AynarDbContext _context;

        public AdminSettingController(AynarDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _context.Settings.FirstAsync();
            var vm = new AdminSettingVM
            {
                ContactEmail = settings.ContactEmail,
                Street = settings.Street,
                City = settings.City,
                State = settings.State,
                PhoneNumber = settings.PhoneNumber,
                About = settings.About
            };
            return View(vm);
        }

        public async Task<IActionResult> Update()
        {
            var settings = await _context.Settings.FirstAsync();
            var vm = new AdminSettingVM
            {
                ContactEmail = settings.ContactEmail,
                Street = settings.Street,
                City = settings.City,
                State = settings.State,
                PhoneNumber = settings.PhoneNumber,
                About = settings.About
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminSettingVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var settings = await _context.Settings.FirstAsync();
            settings.Street = vm.Street;
            settings.City = vm.City;
            settings.State = vm.State;
            settings.PhoneNumber = vm.PhoneNumber;
            settings.About = vm.About;
            settings.ContactEmail = vm.ContactEmail;
            settings.About = vm.About;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
