using AynarMVC_Exam.Contexts;
using AynarMVC_Exam.Models;
using AynarMVC_Exam.ViewModels.TeamVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AynarMVC_Exam.Controllers
{
    public class HomeController : Controller
    {
        readonly AynarDbContext _context;

        public HomeController(AynarDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index()
        {
            var vm = await _context.Teams.Select(t => new TeamListVM
            {
                FullName = t.Name + " " + t.Surname,
                Description = t.Description,
                ImgUrl = t.ImgUrl,
                Position = t.Position,
            }).ToListAsync();
            return View(vm);
        }
    }
}