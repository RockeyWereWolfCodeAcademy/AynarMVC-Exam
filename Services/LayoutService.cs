using AynarMVC_Exam.Contexts;
using AynarMVC_Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace AynarMVC_Exam.Services
{
    public class LayoutService
    {
        readonly AynarDbContext _context;

        public LayoutService(AynarDbContext context)
        {
            _context = context;
        }

        public async Task<Setting> GetSettingAsync()
            => await _context.Settings.FirstAsync();
    }
}
