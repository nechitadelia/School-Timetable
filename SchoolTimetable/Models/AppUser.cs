using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.Models
{
    public class AppUser : IdentityUser
    {
		[MaxLength(100)]
		public string SchoolName { get; set; }
		[MaxLength(50)]
		public string County { get; set; }
		[MaxLength(50)]
		public string City { get; set; }
        public ICollection<SchoolSubject> SchoolSubjects { get; set; }
        public ICollection<Professor> Professors { get; set; }
        public ICollection<SchoolClass> SchoolClasses { get; set; }
        public ICollection<ClassProfessor> ClassProfessors { get; set; }
    }
}
