using School_Timetable.Models;

namespace School_Timetable.ViewModels
{
    public class SchoolSubjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HoursPerWeek { get; set; }
        public List<int>? YearsOfStudy { get; set; }
        public List<Professor>? Professors { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
