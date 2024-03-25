namespace School_Timetable.Models.Entities
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public int YearOfStudy { get; set; }
        public char ClassLetter { get; set; }

        //public List<SchoolSubject> SchoolSubjects { get; set; }

        //public List<Professor> ClassProfessors { get; set; }
    }
}
