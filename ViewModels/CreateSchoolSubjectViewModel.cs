using School_Timetable.Models;

namespace School_Timetable.ViewModels
{
    public class CreateSchoolSubjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HoursPerWeek { get; set; }
		public bool FifthYearOfStudy { get; set; }
		public bool SixthYearOfStudy { get; set; }
		public bool SeventhYearOfStudy { get; set; }
		public bool EighthYearOfStudy { get; set; }
		public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
