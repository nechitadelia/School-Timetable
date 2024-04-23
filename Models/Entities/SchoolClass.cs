namespace School_Timetable.Models.Entities
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public int YearOfStudy { get; set; }
        public char ClassLetter { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
