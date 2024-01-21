using System.ComponentModel.DataAnnotations;

namespace AynarMVC_Exam.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required, MaxLength(32)]
        public string Surname { get; set; }
        [Required, MaxLength(32)]
        public string Position { get; set; }
        [Required, MaxLength(128)]
        public string Description { get; set; }
        [Required]
        public string ImgUrl { get; set; }
    }
}
