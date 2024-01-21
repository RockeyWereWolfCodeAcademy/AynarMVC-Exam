using AynarMVC_Exam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AynarMVC_Exam.Contexts
{
    public class AynarDbContext : IdentityDbContext<AppUser>
    {
        public AynarDbContext(DbContextOptions<AynarDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
    }
}
