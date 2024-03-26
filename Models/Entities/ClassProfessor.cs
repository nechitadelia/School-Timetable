
namespace School_Timetable.Models.Entities
{
	public class ClassProfessor
	{
        public int Id { get; set; }
        public int SchoolClassId { get; set; }
        public string SubjectName { get; set; }
        public int ProfessorId { get; set; }
    }
}
