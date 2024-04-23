using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
	public class RegisterViewModel
	{
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
