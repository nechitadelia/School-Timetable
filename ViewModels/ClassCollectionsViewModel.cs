using School_Timetable.Models;

namespace School_Timetable.ViewModels
{
    public class ClassCollectionsViewModel
    {
        //all the classes for each year of study
        public Stack<SchoolClass>? fifthGradeClasses { get; set; }
        public Stack<SchoolClass>? sixthGradeClasses { get; set; }
        public Stack<SchoolClass>? seventhGradeClasses { get; set; }
        public Stack<SchoolClass>? eighthGradeClasses { get; set; }

        //all the subjects for each year of study
        public List<SchoolSubject>? fifthGradeSubjects { get; set; }
        public List<SchoolSubject>? sixthGradeSubjects { get; set; }
        public List<SchoolSubject>? seventhGradeSubjects { get; set; }
        public List<SchoolSubject>? eighthGradeSubjects { get; set; }

        //all the professors for each year of study
        public List<List<Professor>>? fifthGradeProfessors { get; set; }
        public List<List<Professor>>? sixthGradeProfessors { get; set; }
        public List<List<Professor>>? seventhGradeProfessors { get; set; }
        public List<List<Professor>>? eighthGradeProfessors { get; set; }

        //user
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
