using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;

namespace School_Timetable.Services
{
	public interface ISchoolServices
	{
		//-----------------------------------> READ METHODS <-----------------------------------

		//getting a list of all classes from the repository
		public ICollection<SchoolClass> GetAllClasses();

		//getting a list of all professors from the repository
		public ICollection<Professor> GetAllProfessors();

		//getting a list of all subjects from the repository
		public ICollection<SchoolSubject> GetAllSchoolSubjects();

		//get one professor by id
		public Professor GetProfessor(int professorId);

		//getting a list of subjects for all professors
		public List<string> GetAllProfessorsSubjects(ICollection<Professor> professors);

		//getting a list of unassigned hours for all professors
		public List<int> GetAllProfessorsUnassignedHours(ICollection<Professor> professors);

		//getting a list of the classes for all professors
		public List<List<string>> GetAllProfessorsClasses(ICollection<Professor> professors);

		//get a professor's subject by his/her id
		public SchoolSubject GetProfessorSubject(int professorId);

		//getting a list of strings with all professors, in the order of school subjects
		public List<string> GetProfessorsForSubjects(ICollection<SchoolSubject> subjects);

		//getting the list of all subjects for all classes
		public List<List<SchoolSubject>> GetSubjectsForAllClasses(ICollection<SchoolClass> schoolClasses);

		//getting the list of all professors for all classes
		public List<List<string>> GetProfessorsForAllClasses(ICollection<SchoolClass> schoolClasses);

		//get the next available letter for a new class, depending on the user input for the year of study
		public char GetAvailableLetter(SchoolClassViewModel viewModel);

		//get all the available letters for all school years
		public List<char> GetAllAvailableLetters();

		//get all the existing last letters for all school years
		public List<char> GetAllExistingLetters();

        //-----------------------------------> CREATE METHODS <-----------------------------------

        //adding a new professor to database
        public void AddProfessor(ProfessorViewModel viewModel, ICollection<SchoolSubject> schoolSubjects);

		//adding a new class to database
		public void AddClass(SchoolClassViewModel viewModel);

		//assign all professors to all classes
		public void AssignAllProfessorsToAllClasses();

		//-----------------------------------> UPDATE METHODS <-----------------------------------

		//edit a professors's data
		public void EditProfessor(Professor professor);

		//-----------------------------------> DELETE METHODS <-----------------------------------

		//delete a professor from database
		public void DeleteProfessor(Professor professor);

		//delete a class from database
		public void DeleteClass(SchoolClassViewModel viewModel);

		//unassign all professors from all classes
		public void UnAssignAllProfessorsFromClasses();

	}
}
