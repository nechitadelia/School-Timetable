using School_Timetable.Models;

namespace School_Timetable.ViewModels
{
    public class CreateSchoolClassViewModel
    {
        public int Id { get; set; }
        public int YearOfStudy { get; set; }
        public char ClassLetter { get; set; }
        public List<char> AllAvailableLetters { get; set; }
        public List<int> AllSchoolyears { get; set; } = new List<int> { 5, 6, 7, 8 };
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
