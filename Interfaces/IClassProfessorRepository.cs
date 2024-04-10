using Microsoft.EntityFrameworkCore;
using School_Timetable.Models.Entities;

namespace School_Timetable.Interfaces
{
	public interface IClassProfessorRepository
	{
		//creating a new professor
		void AddProfessorToAClass(SchoolClass schoolClass, Professor professor);

		//check if a connection exists in the ClassProfessor table
		bool ConnectionExists(SchoolClass schoolClass, SchoolSubject schoolSubject);
		bool ConnectionExists(Professor professor);

		//get the professor of one subject of a class
		Professor GetProfessorOfASubjectOfOneClass(SchoolClass schoolClass, SchoolSubject schoolSubject);

		//get all the class ids for one professor
		ICollection<int> GetClassIds(Professor professor);

        //get the list of classes for one professor
        List<SchoolClass> GetClassesOfAProfessor(Professor professor);
		
		//delete a collection of ClassProfessor from database
		void DeleteClassProfessor(ICollection<ClassProfessor> classProfessors);

        //unassign a professor from classes
        void UnassignAProfessor(Professor professor);

        //unassign all professors from all classes
        void UnassignAllProfessors();

		//delete a class from ClassProfessor table
		void UnassignAClass(SchoolClass schoolClass);

		//save changes to database
		void Save();
	}
}
