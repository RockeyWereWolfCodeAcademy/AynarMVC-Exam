using System.ComponentModel.DataAnnotations;

namespace AynarMVC_Exam.Areas.ViewModels.AdminTeamVMs
{
    public class AdminTeamUpdateVM
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required, MaxLength(32)]
        public string Surname { get; set; }
        [Required, MaxLength(32)]
        public string Position { get; set; }
        [Required, MaxLength(128)]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImgUrl { get; set; }
    }
}
