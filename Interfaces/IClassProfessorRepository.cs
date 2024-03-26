using Microsoft.EntityFrameworkCore;
using School_Timetable.Models.Entities;

namespace School_Timetable.Interfaces
{
	public interface IClassProfessorRepository
	{
		//creating a new professor
		public void AddProfessorToAClass(SchoolClass schoolClass, Professor professor);

		//check if a connection exists in the ClassProfessor table
		public bool ConnectionExists(SchoolClass schoolClass, SchoolSubject schoolSubject);
		public bool ConnectionExists(Professor professor);

        //get the professor of one subject of a class
        public string GetProfessorOfASubject(SchoolClass schoolClass, SchoolSubject schoolSubject);

		//get a list of professors for one class
		public List<string> GetProfessorsOfAClass(SchoolClass schoolClass, List<SchoolSubject> classSubjects);

		//get all the class ids for one professor
		public ICollection<int> GetClassIds(Professor professor);

		//get the list of classes for one professor
		public List<string> GetClassesOfAProfessor(Professor professor);

		//delete a collection of ClassProfessor from database
		public void DeleteClassProfessor(ICollection<ClassProfessor> classProfessors);

        //unassign a professor from classes
        public void UnassignAProfessor(Professor professor);

        //unassign all professors from all classes
        public void UnassignAllProfessors();

		//delete a class from ClassProfessor table
		public void UnassignAClass(SchoolClass schoolClass);

		//save changes to database
		public void Save();
	}
}
