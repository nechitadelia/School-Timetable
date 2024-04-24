using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
	public class RegisterViewModel
	{
        [Required(ErrorMessage = "School name is required")]
        public string SchoolName { get; set; }

		[Required(ErrorMessage = "County is required")]
		public string County { get; set; }

		[Required(ErrorMessage = "City is required")]
		public string City { get; set; }

		[Display(Name = "Email Address")]
		[Required(ErrorMessage = "Email address is required")]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "The password must have at least 8 characters")]
        public string Password { get; set; }

		[Display(Name = "Confirm Password")]
		[Required(ErrorMessage = "Confirm password is required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
	}
}
