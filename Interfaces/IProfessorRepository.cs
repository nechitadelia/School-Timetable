using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.Models.Entities;

namespace School_Timetable.Interfaces
{
    public interface IProfessorRepository
    {
		//get list of all professors, in ascending order
		ICollection<Professor> GetProfessors();

		//get one professor by id
		Professor GetProfessor(int professorId);

		//get a professor's subject by his/her id
		SchoolSubject GetSubjectOfProfessor(int professorId);

		//check if you can assign hours to a professor
		bool CanAssignHours(int professorId);

		//check if a professor was already assigned to a class
		bool CanAssignClass(SchoolClass schoolClass, SchoolSubject schoolSubject);

        //assign hours to a professor
        void AssignHours(int professorId);

		//get a professor's unassigned hours
		int GetUnassignedHours(int professorId);

        //unassign all hours from a professor
        void UnassignAllHoursFromProfessor(Professor professor);

        //unassign all hours from all professors
        void UnassignAllHoursFromEveryone();

        //unassign hours from a professor (when a class is deleted)
        void UnassignHoursFromProfessor(Professor professor);

		//create a new professor
		void AddProfessor(ProfessorViewModel viewModel);

        //edit a professors's data
        void EditProfessor(Professor viewModel);

		//delete a professor from the database
		void DeleteProfessor(Professor viewModel);

        //save changes to database
        void Save();
    }
}
