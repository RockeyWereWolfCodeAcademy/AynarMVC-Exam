using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AynarMVC_Exam.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(32)]
        public string? Name { get; set; }
        [MaxLength(32)]
        public string? Surname { get; set; }
    }
}
