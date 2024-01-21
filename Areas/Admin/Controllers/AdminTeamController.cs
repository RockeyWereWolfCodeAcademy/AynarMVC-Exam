using AynarMVC_Exam.Areas.ViewModels.AdminTeamVMs;
using AynarMVC_Exam.Contexts;
using AynarMVC_Exam.Helpers;
using AynarMVC_Exam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AynarMVC_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminTeamController : Controller
    {
        readonly AynarDbContext _context;

        public AdminTeamController(AynarDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index()
        {
            var vm = await _context.Teams.Select(t => new AdminTeamListVM
            {
                Id = t.Id,
                Name = t.Name,
                Surname = t.Surname,
                Position = t.Position,
                ImgUrl = t.ImgUrl,
            }).ToListAsync();
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminTeamCreateVM vm)
        {
            if (vm.Image != null)
            {
                if (!vm.Image.IsValidSize(1000))
                    ModelState.AddModelError("Image", "Size of file must be less than 1 mb!");
                if (!vm.Image.CheckType("image"))
                    ModelState.AddModelError("Image", "File must be an image!");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dataToCreate = new Team
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Position = vm.Position,
                Description = vm.Description,
                ImgUrl = vm.Image.SaveFileAsync(PathConstants.TeamImagePath).Result
            };
            await _context.Teams.AddAsync(dataToCreate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>  Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Teams.FindAsync(id);
            if (data == null) return NotFound();
            System.IO.File.Delete(Path.Combine(PathConstants.RootPath, data.ImgUrl));
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Teams.FindAsync(id);
            if (data == null) return NotFound();
            return View(new AdminTeamUpdateVM
            {
                Name = data.Name,
                Surname = data.Surname,
                Position = data.Position,
                ImgUrl = data.ImgUrl,
                Description = data.Description,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, AdminTeamUpdateVM vm)
        {
            if (id == null) return BadRequest();
            var data = await _context.Teams.FindAsync(id);
            if (data == null) return NotFound();
            if (vm.Image != null)
            {
                if (!vm.Image.IsValidSize(1000))
                    ModelState.AddModelError("Image", "Size of file must be less than 1 mb!");
                if (!vm.Image.CheckType("image"))
                    ModelState.AddModelError("Image", "File must be an image!");
            }
            if (!ModelState.IsValid)
            {
                vm.ImgUrl = data.ImgUrl;
                return View(vm);
            }
            data.Name = vm.Name;
            data.Surname = vm.Surname;
            data.Position = vm.Position;
            data.Description = vm.Description;
            if (vm.Image != null)
            {
                System.IO.File.Delete(Path.Combine(PathConstants.RootPath, data.ImgUrl));
                data.ImgUrl = await vm.Image.SaveFileAsync(PathConstants.TeamImagePath);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
