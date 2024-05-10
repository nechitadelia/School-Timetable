using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.ViewModels;

namespace School_Timetable.Interfaces
{
    public interface ISchoolServices
    {
		//-----------------------------------> READ METHODS <-----------------------------------

		//getting a list of all classes from the repository
		Task<ICollection<SchoolClass>> GetAllClasses();

        //getting a list of all professors from the repository
        Task<ICollection<Professor>> GetAllProfessors();

		//getting a list of all subjects from the repository
		Task<ICollection<SchoolSubject>> GetAllSchoolSubjects();

        //get list of all fifth grade classes
        Task<Stack<SchoolClass>> GetFifthGradeClasses();

        //get list of all sixth grade classes
        Task<Stack<SchoolClass>> GetSixthGradeClasses();

        //get list of all seventh grade classes
        Task<Stack<SchoolClass>> GetSeventhGradeClasses();

        //get list of all eighth grade classes
        Task<Stack<SchoolClass>> GetEighthGradeClasses();

		//get one subject by id
		Task<SchoolSubject> GetSchoolSubject(int subjectId);

        //get one professor by id
        Task<Professor> GetProfessor(int professorId);

        //get a professor's unassigned hours
        Task<int> GetUnassignedHours(int professorId);

        //get a professor's subject by his/her id
        Task<SchoolSubject> GetSubjectOfProfessor(int professorId);

        //get the list of classes for one professor
        Task<List<SchoolClass>> GetClassesOfAProfessor(Professor professor);

		//get a list of all professors for each school subject  - for Subjects View
		Task<List<List<Professor>>> GetProfessorsForSubjects(ICollection<SchoolSubject> subjects);

        //getting the list of all subjects for fifth grade
        Task<List<SchoolSubject>> GetSubjectsForFifthGrade();

		//getting the list of all subjects for fifth grade
		Task<List<SchoolSubject>> GetSubjectsForSixthGrade();

		//getting the list of all subjects for fifth grade
		Task<List<SchoolSubject>> GetSubjectsForSeventhGrade();

		//getting the list of all subjects for fifth grade
		Task<List<SchoolSubject>> GetSubjectsForEighthGrade();

        //get a list of professors for one class
        Task<List<Professor>> GetProfessorsOfAClass(SchoolClass schoolClass);

        //get the list of all professors for one year of study
        Task<List<List<Professor>>> GetProfessorsForOneYearOfStudy(Stack<SchoolClass> schoolClasses);

        //get all the available letters for all school years
        Task<List<char>> GetAllAvailableLetters();

        //get all the existing last letters for all school years
        Task<List<char>> GetAllExistingLetters();

        //check if there are any subjects in database
        Task<bool> CheckExistingSubjects();

        //get all class collections (classes, subjects, professors)
        Task<SchoolClassCollectionsViewModel> GetClassCollections();

        //get a collection of all professors
        Task<List<ProfessorViewModel>> GetProfessorCollections(string currentUserId);

        //get a collection of all subjects
        Task<List<SchoolSubjectViewModel>> GetSubjectsCollections(string currentUserId);

		//get all users
		Task<List<AppUserViewModel>> GetAllUsers();

        //get one user by id
        Task<AppUser> GetUser();

        //get one user by id
        Task<AppUser> GetUser(string id);

        //get one user view model
        Task<AppUserViewModel> GetUserViewModel();

        //-----------------------------------> CREATE METHODS <-----------------------------------

        //adding a new subject to database
        Task AddSubject(CreateSchoolSubjectViewModel viewModel);

		//adding a new professor to database
		Task AddProfessor(CreateProfessorViewModel viewModel);

        //adding a new class to database
        Task AddClass(CreateSchoolClassViewModel viewModel);

        //assign one professor to one class
        Task AssignOneProfessorToOneClass(SchoolClass schoolClass, Professor professor);

        //assign all professors to all classes
        Task AssignAllProfessorsToAllClasses();

        //-----------------------------------> UPDATE METHODS <-----------------------------------

        //edit a professors's data
        Task EditProfessor(EditProfessorViewModel viewModel);

		//edit a user data
		Task EditUser(EditAppUserViewModel viewModel);

        //graduate all classes - change classes to the next school year
        Task GraduateClasses();


		//-----------------------------------> DELETE METHODS <-----------------------------------

		//delete a user from database
		Task DeleteUser(AppUserViewModel viewModel);

        //delete a subject from database
        Task<bool> DeleteSchoolSubject(SchoolSubject subject);

		//delete a professor from database
		Task DeleteProfessor(Professor professor);

        //delete a class from database
        Task DeleteClass(DeleteSchoolClassViewModel viewModel);

        //unassign all professors from all classes
        Task UnAssignAllProfessorsFromClasses();

    }
}
