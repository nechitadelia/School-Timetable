using School_Timetable.Models;
using School_Timetable.Utilities;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
    public class CreateSchoolSubjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the subject name")]
        [MinLength(2, ErrorMessage = "The name must have at least 2 letters")]
        [MaxLength(100, ErrorMessage = "The name must have maximum 100 letters")]
        [AllowedOnlyLetters(ErrorMessage = "The name must contain only letters")]
        public string Name { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Enter a number between 1 - 20")]
        [AllowedOnlyInt(ErrorMessage = "You must enter an integer")]
        public int HoursPerWeek { get; set; }
		public bool FifthYearOfStudy { get; set; }
		public bool SixthYearOfStudy { get; set; }
		public bool SeventhYearOfStudy { get; set; }
		public bool EighthYearOfStudy { get; set; }
		public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
