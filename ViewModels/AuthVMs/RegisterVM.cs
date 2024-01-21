using System.ComponentModel.DataAnnotations;

namespace AynarMVC_Exam.ViewModels.AuthVMs
{
    public class RegisterVM
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required, MaxLength(32)]
        public string Surname { get; set; }
        [Required, MaxLength(32)]
        public string Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
