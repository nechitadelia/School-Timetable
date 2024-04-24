using School_Timetable.Models;

namespace School_Timetable.ViewModels
{
	public class EditProfessorViewModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public SchoolSubject ProfessorSubject { get; set; }
		public int AssignedHours { get; set; }
	}
}
