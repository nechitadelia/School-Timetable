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
		ICollection<SchoolClass> GetAllClasses();

		//getting a list of all professors from the repository
		ICollection<Professor> GetAllProfessors();

		//getting a list of all subjects from the repository
		ICollection<SchoolSubject> GetAllSchoolSubjects();

		//get list of all fifth grade classes
		Stack<SchoolClass> GetFifthGradeClasses();

		//get list of all sixth grade classes
		Stack<SchoolClass> GetSixthGradeClasses();

		//get list of all seventh grade classes
		Stack<SchoolClass> GetSeventhGradeClasses();

		//get list of all eighth grade classes
		Stack<SchoolClass> GetEighthGradeClasses();

        //get one professor by id
        Professor GetProfessor(int professorId);

		//get a professor's unassigned hours
		int GetUnassignedHours(int professorId);

		//get a professor's subject by his/her id
		SchoolSubject GetSubjectOfProfessor(int professorId);

		//get the list of classes for one professor
		List<SchoolClass> GetClassesOfAProfessor(Professor professor);

        //get a list of all professors for each school subject  - for Subjects View
        List<List<Professor>> GetProfessorsForSubjects(ICollection<SchoolSubject> subjects);

		//getting the list of all subjects for fifth grade
		List<SchoolSubject> GetSubjectsForFifthGrade();

		//getting the list of all subjects for fifth grade
		List<SchoolSubject> GetSubjectsForSixthGrade();

		//getting the list of all subjects for fifth grade
		List<SchoolSubject> GetSubjectsForSeventhGrade();

		//getting the list of all subjects for fifth grade
		List<SchoolSubject> GetSubjectsForEighthGrade();

		//get a list of professors for one class
		List<Professor> GetProfessorsOfAClass(SchoolClass schoolClass);

        //get the list of all professors for one year of study
        List<List<Professor>> GetProfessorsForOneYearOfStudy(Stack<SchoolClass> schoolClasses);

		//get all the available letters for all school years
		List<char> GetAllAvailableLetters();

		//get all the existing last letters for all school years
		List<char> GetAllExistingLetters();

		//get all class collections (classes, subjects, professors)
		ClassCollectionsViewModel GetClassCollections();

		//get a collection of all professors
		List<ProfessorCollectionsViewModel> GetProfessorCollections();

        //-----------------------------------> CREATE METHODS <-----------------------------------

        //adding a new professor to database
        void AddProfessor(ProfessorViewModel viewModel);

		//adding a new class to database
		void AddClass(SchoolClassViewModel viewModel);

		//assign all professors to all classes
		void AssignAllProfessorsToAllClasses();

		//-----------------------------------> UPDATE METHODS <-----------------------------------

		//edit a professors's data
		void EditProfessor(Professor professor);

		//-----------------------------------> DELETE METHODS <-----------------------------------

		//delete a professor from database
		void DeleteProfessor(Professor professor);

		//delete a class from database
		void DeleteClass(SchoolClassViewModel viewModel);

		//unassign all professors from all classes
		void UnAssignAllProfessorsFromClasses();

	}
}
