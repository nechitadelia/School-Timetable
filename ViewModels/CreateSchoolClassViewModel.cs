using School_Timetable.Models;

namespace School_Timetable.ViewModels
{
    public class CreateSchoolClassViewModel
    {
        public int Id { get; set; }
        public int YearOfStudy { get; set; }
        public char ClassLetter { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
