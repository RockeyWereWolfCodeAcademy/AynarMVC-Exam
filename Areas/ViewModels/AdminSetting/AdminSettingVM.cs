using System.ComponentModel.DataAnnotations;

namespace AynarMVC_Exam.Areas.Admin.ViewModels.AdminSetting
{
	public class AdminSettingVM
	{
		[DataType(DataType.EmailAddress)]
		public string ContactEmail { get; set; }
		[MaxLength(128)]
		public string Street { get; set; }
        [MaxLength(128)]
        public string City { get; set; }
        [MaxLength(128)]
        public string State { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
		[MaxLength(256)]
		public string About { get; set; }
	}
}
