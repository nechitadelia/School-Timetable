namespace School_Timetable.Models.Entities
{
    public class Professor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AssignedHours { get; set; }
        public SchoolSubject SchoolSubject { get; set; }
        public int SchoolSubjectId { get; set; }
    }
}
