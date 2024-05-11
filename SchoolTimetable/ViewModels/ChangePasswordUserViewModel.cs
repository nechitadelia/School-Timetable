using School_Timetable.Utilities;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
    public class ChangePasswordUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "The password must have at least 8 characters")]
        [DataType(DataType.Password)]
        [RequiredDigit(ErrorMessage = "Password must have at least one digit")]
        [RequiredNonalphanumeric(ErrorMessage = "Password must have at least one non-alphanumeric character")]
        [RequiredLowerUpper(ErrorMessage = "Password must have at least one lower letter and one upper letter")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
