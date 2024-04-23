using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Models.Entities;

namespace School_Timetable.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SchoolSubject> SchoolSubjects { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<ClassProfessor> ClassProfessors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //creating a list of identity roles
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            //adding the identity roles list
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
