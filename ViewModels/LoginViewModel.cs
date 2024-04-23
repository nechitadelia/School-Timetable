using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
	public class LoginViewModel
	{
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Wrong credentials. Please try again")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Wrong credentials. Please try again")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
