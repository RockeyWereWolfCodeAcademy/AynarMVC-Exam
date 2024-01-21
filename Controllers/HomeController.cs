using AynarMVC_Exam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AynarMVC_Exam.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}