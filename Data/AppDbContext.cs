using Microsoft.EntityFrameworkCore;
using School_Timetable.Models.Entities;

namespace School_Timetable.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SchoolSubject> SchoolSubjects { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<ClassProfessor> ClassProfessors { get; set; }

    }
}
