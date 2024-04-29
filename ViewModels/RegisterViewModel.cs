using School_Timetable.Utilities;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
	public class RegisterViewModel
	{
        [Required(ErrorMessage = "School name is required")]
        [MinLength(2, ErrorMessage = "The name must have at least 2 letters")]
        [MaxLength(100, ErrorMessage = "The name must have maximum 100 letters")]
        public string SchoolName { get; set; }

		[Required(ErrorMessage = "County is required")]
        [MinLength(2, ErrorMessage = "The county name must have at least 2 letters")]
        [MaxLength(50, ErrorMessage = "The county name must have maximum 100 letters")]
        [AllowedOnlyLetters(ErrorMessage = "The county name must contain only letters")]
        public string County { get; set; }

		[Required(ErrorMessage = "City is required")]
        [MinLength(2, ErrorMessage = "The city name must have at least 2 letters")]
        [MaxLength(50, ErrorMessage = "The city name must have maximum 100 letters")]
        [AllowedOnlyLetters(ErrorMessage = "The city name must contain only letters")]
        public string City { get; set; }

		[Display(Name = "Email Address")]
		[Required(ErrorMessage = "Email address is required")]
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "The password must have at least 8 characters")]
        [DataType(DataType.Password)]
        [RequiredDigit(ErrorMessage = "Password must have at least one digit")]
        [RequiredNonalphanumeric(ErrorMessage = "Password must have at least one non-alphanumeric character")]
        [RequiredLowerUpper(ErrorMessage = "Password must have at least one lower letter and one upper letter")]
        public string Password { get; set; }

		[Display(Name = "Confirm Password")]
		[Required(ErrorMessage = "Confirm password is required")]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
	}
}
