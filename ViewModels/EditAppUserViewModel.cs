using School_Timetable.Utilities;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
    public class EditAppUserViewModel
    {
        public string Id { get; set; }

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
    }
}
