using System.ComponentModel.DataAnnotations;

namespace AynarMVC_Exam.Areas.ViewModels.AdminTeamVMs
{
    public class AdminTeamListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string ImgUrl { get; set; }
    }
}
