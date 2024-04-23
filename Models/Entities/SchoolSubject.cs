namespace School_Timetable.Models.Entities
{
    public class SchoolSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HoursPerWeek { get; set; }
        public int YearOfStudy { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
