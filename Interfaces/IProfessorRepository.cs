using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.ViewModels;

namespace School_Timetable.Interfaces
{
    public interface IProfessorRepository
    {
		//get list of all professors, in ascending order
		Task<ICollection<Professor>> GetProfessors();

		//get one professor by id
		Task<Professor> GetProfessor(int professorId);

        //get a professor's subject by his/her id
        Task<SchoolSubject> GetSubjectOfProfessor(int professorId);

		//check if you can assign hours to a professor
		Task<bool> CanAssignHours(int professorId);

		//check if a professor was already assigned to a class
		Task<bool> CanAssignClass(SchoolClass schoolClass, SchoolSubject schoolSubject);

        //assign hours to a professor
        Task AssignHours(int professorId);

		//get a professor's unassigned hours
		Task<int> GetUnassignedHours(int professorId);

        //unassign all hours from a professor
        void UnassignAllHoursFromProfessor(Professor professor);

        //unassign all hours from all professors
        Task UnassignAllHoursFromEveryone();

        //unassign hours from a professor (when a class is deleted)
        void UnassignHoursFromProfessor(Professor professor);

		//create a new professor
		Task AddProfessor(CreateProfessorViewModel viewModel);

        //edit a professors's data
        Task EditProfessor(EditProfessorViewModel viewModel);

		//delete a professor from the database
		Task DeleteProfessor(Professor viewModel);

        //save changes to database
        bool Save();
    }
}
