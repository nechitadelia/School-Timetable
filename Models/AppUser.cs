using Microsoft.AspNetCore.Identity;

namespace School_Timetable.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<SchoolSubject> SchoolSubjects { get; set; }
        public ICollection<Professor> Professors { get; set; }
        public ICollection<SchoolClass> SchoolClasses { get; set; }
        public ICollection<ClassProfessor> ClassProfessors { get; set; }
    }
}
