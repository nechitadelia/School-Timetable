using School_Timetable.Models;
using School_Timetable.Utilities;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
    public class CreateProfessorViewModel
    {
        [Required(ErrorMessage = "Please enter the first name")]
        [MinLength(2, ErrorMessage = "The name must have at least 2 letters")]
        [MaxLength(100, ErrorMessage = "The name must have maximum 100 letters")]
        [AllowedOnlyLetters(ErrorMessage = "The name must contain only letters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the last name")]
        [MinLength(2, ErrorMessage = "The name must have at least 2 letters")]
        [MaxLength(50, ErrorMessage = "The name must have maximum 50 letters")]
        [AllowedOnlyLetters(ErrorMessage = "The name must contain only letters")]
        public string LastName { get; set; }

        public int SchoolSubjectId { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
