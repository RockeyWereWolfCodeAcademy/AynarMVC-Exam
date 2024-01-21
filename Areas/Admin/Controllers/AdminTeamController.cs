using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AynarMVC_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminTeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
