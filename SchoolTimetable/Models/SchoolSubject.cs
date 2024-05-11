namespace School_Timetable.Models
{
    public class SchoolSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HoursPerWeek { get; set; }
        public char FifthYearOfStudy { get; set; }
        public char SixthYearOfStudy { get; set; }
        public char SeventhYearOfStudy { get; set; }
        public char EighthYearOfStudy { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
