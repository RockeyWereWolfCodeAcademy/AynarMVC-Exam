using System.ComponentModel.DataAnnotations;

namespace AynarMVC_Exam.ViewModels.TeamVMs
{
    public class TeamListVM
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
    }
}
