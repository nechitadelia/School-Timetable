using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;

namespace School_Timetable.Interfaces
{
    public interface IClassProfessorRepository
	{
        //assign one professor to one class
        Task<bool> AddProfessorToAClass(SchoolClass schoolClass, Professor professor);

		//check if a connection exists in the ClassProfessor table
		Task<bool> ConnectionExists(SchoolClass schoolClass, SchoolSubject schoolSubject);
		Task<bool> ConnectionExists(Professor professor);

		//get the professor of one subject of a class
		Task<Professor> GetProfessorOfASubjectOfOneClass(SchoolClass schoolClass, SchoolSubject schoolSubject);

		//get all the class ids for one professor
		Task<ICollection<int>> GetClassIds(Professor professor);

		//get the list of classes for one professor
		Task<List<SchoolClass>> GetClassesOfAProfessor(Professor professor);
		
		//delete a collection of ClassProfessor from database
		bool DeleteClassProfessor(ICollection<ClassProfessor> classProfessors);

        //unassign a professor from all classes
        Task<bool> UnassignAProfessorFromAllClasses(Professor professor);

        //unassign all professors from all classes
        Task<bool> UnassignAllProfessorsFromAllClasses();

        //delete a class from ClassProfessor table
        Task<bool> UnassignAClass(SchoolClass schoolClass);

		//save changes to database
		bool Save();
	}
}
