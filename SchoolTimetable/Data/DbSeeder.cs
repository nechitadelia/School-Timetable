using School_Timetable.Models;

namespace School_Timetable.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.SchoolSubjects.Any())
                return;

            List<SchoolSubject> subjects = new List<SchoolSubject>
            {
                new SchoolSubject { Id = 1,  Name = "Mathematics",       HoursPerWeek = 4, FifthYearOfStudy = 'Y', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
                new SchoolSubject { Id = 2,  Name = "Romanian Language",  HoursPerWeek = 4, FifthYearOfStudy = 'Y', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
                new SchoolSubject { Id = 3,  Name = "English Language",   HoursPerWeek = 3, FifthYearOfStudy = 'Y', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
                new SchoolSubject { Id = 4,  Name = "Physics",            HoursPerWeek = 2, FifthYearOfStudy = 'N', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
                new SchoolSubject { Id = 5,  Name = "Chemistry",          HoursPerWeek = 2, FifthYearOfStudy = 'N', SixthYearOfStudy = 'N', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
                new SchoolSubject { Id = 6,  Name = "Biology",            HoursPerWeek = 2, FifthYearOfStudy = 'Y', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
                new SchoolSubject { Id = 7,  Name = "History",            HoursPerWeek = 2, FifthYearOfStudy = 'Y', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
                new SchoolSubject { Id = 8,  Name = "Geography",          HoursPerWeek = 2, FifthYearOfStudy = 'Y', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
                new SchoolSubject { Id = 9,  Name = "Art",                HoursPerWeek = 1, FifthYearOfStudy = 'Y', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'N', EighthYearOfStudy = 'N', AppUserId = "" },
                new SchoolSubject { Id = 10, Name = "Physical Education", HoursPerWeek = 2, FifthYearOfStudy = 'Y', SixthYearOfStudy = 'Y', SeventhYearOfStudy = 'Y', EighthYearOfStudy = 'Y', AppUserId = "" },
            };

            db.SchoolSubjects.AddRange(subjects);

            List<Professor> professors = new List<Professor>
            {
                new Professor { Id = 1,  FirstName = "Ana",      LastName = "Ionescu",    AssignedHours = 0, SchoolSubjectId = 1,  AppUserId = "" },
                new Professor { Id = 2,  FirstName = "Mihai",    LastName = "Popescu",    AssignedHours = 0, SchoolSubjectId = 1,  AppUserId = "" },
                new Professor { Id = 3,  FirstName = "Elena",    LastName = "Constantin", AssignedHours = 0, SchoolSubjectId = 2,  AppUserId = "" },
                new Professor { Id = 4,  FirstName = "Ion",      LastName = "Georgescu",  AssignedHours = 0, SchoolSubjectId = 2,  AppUserId = "" },
                new Professor { Id = 5,  FirstName = "Maria",    LastName = "Dumitrescu", AssignedHours = 0, SchoolSubjectId = 3,  AppUserId = "" },
                new Professor { Id = 6,  FirstName = "Alexandru", LastName = "Stan",      AssignedHours = 0, SchoolSubjectId = 4,  AppUserId = "" },
                new Professor { Id = 7,  FirstName = "Cristina", LastName = "Marin",      AssignedHours = 0, SchoolSubjectId = 5,  AppUserId = "" },
                new Professor { Id = 8,  FirstName = "Bogdan",   LastName = "Dima",       AssignedHours = 0, SchoolSubjectId = 6,  AppUserId = "" },
                new Professor { Id = 9,  FirstName = "Ioana",    LastName = "Nistor",     AssignedHours = 0, SchoolSubjectId = 7,  AppUserId = "" },
                new Professor { Id = 10, FirstName = "Radu",     LastName = "Popa",       AssignedHours = 0, SchoolSubjectId = 8,  AppUserId = "" },
                new Professor { Id = 11, FirstName = "Laura",    LastName = "Florescu",   AssignedHours = 0, SchoolSubjectId = 9,  AppUserId = "" },
                new Professor { Id = 12, FirstName = "Andrei",   LastName = "Stoica",     AssignedHours = 0, SchoolSubjectId = 10, AppUserId = "" },
            };

            db.Professors.AddRange(professors);

            List<SchoolClass> classes = new List<SchoolClass>
            {
                new SchoolClass { Id = 1,  YearOfStudy = 5, ClassLetter = 'A', AppUserId = "" },
                new SchoolClass { Id = 2,  YearOfStudy = 5, ClassLetter = 'B', AppUserId = "" },
                new SchoolClass { Id = 3,  YearOfStudy = 5, ClassLetter = 'C', AppUserId = "" },
                new SchoolClass { Id = 4,  YearOfStudy = 6, ClassLetter = 'A', AppUserId = "" },
                new SchoolClass { Id = 5,  YearOfStudy = 6, ClassLetter = 'B', AppUserId = "" },
                new SchoolClass { Id = 6,  YearOfStudy = 6, ClassLetter = 'C', AppUserId = "" },
                new SchoolClass { Id = 7,  YearOfStudy = 7, ClassLetter = 'A', AppUserId = "" },
                new SchoolClass { Id = 8,  YearOfStudy = 7, ClassLetter = 'B', AppUserId = "" },
                new SchoolClass { Id = 9,  YearOfStudy = 8, ClassLetter = 'A', AppUserId = "" },
                new SchoolClass { Id = 10, YearOfStudy = 8, ClassLetter = 'B', AppUserId = "" },
            };

            db.SchoolClasses.AddRange(classes);

            db.SaveChanges();
        }
    }
}
