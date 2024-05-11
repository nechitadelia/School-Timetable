using School_Timetable.Models;
using School_Timetable.Utilities;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
	public class EditProfessorViewModel
	{
		public int Id { get; set; }

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
		public int MaxHours { get; set; }
		public SchoolSubject ProfessorSubject { get; set; }
		public int AssignedHours { get; set; }
	}
}
