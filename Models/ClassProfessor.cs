namespace School_Timetable.Models
{
    public class ClassProfessor
    {
        public int Id { get; set; }
        public int SchoolClassId { get; set; }
        public string SubjectName { get; set; }
        public int ProfessorId { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
