using Microsoft.AspNetCore.Mvc;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Models.Entities;

namespace School_Timetable.Repository
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly AppDbContext _dbContext;

        public SchoolClassRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get list of all classes, in order
        public ICollection<SchoolClass> GetAllClasses()
        {
            ICollection<SchoolClass> schoolClasses = _dbContext.SchoolClasses
                .OrderBy(c => c.YearOfStudy)
                .ThenBy(c => c.ClassLetter)
                .ToList();

            return schoolClasses;
        }

        //get list of all fifth grade classes
        public Stack<SchoolClass> GetFifthGradeClasses()
        {
            ICollection<SchoolClass> fifthGradeClasses = _dbContext.SchoolClasses
                .Where(c => c.YearOfStudy == 5)
                .OrderBy(c => c.ClassLetter)
                .ToList();

            return new Stack<SchoolClass>(fifthGradeClasses);
        }

        //get list of all sixth grade classes
        public Stack<SchoolClass> GetSixthGradeClasses()
        {
            var sixthGradeClasses = _dbContext.SchoolClasses
                .Where(c => c.YearOfStudy == 6)
                .OrderBy(c => c.ClassLetter)
                .ToList();

            return new Stack<SchoolClass>(sixthGradeClasses);
        }

        //get list of all seventh grade classes
        public Stack<SchoolClass> GetSeventhGradeClasses()
        {
            ICollection<SchoolClass> seventhGradeClasses = _dbContext.SchoolClasses
                .Where(c => c.YearOfStudy == 7)
                .OrderBy(c => c.ClassLetter)
                .ToList();

            return new Stack<SchoolClass>(seventhGradeClasses);
        }

        //get list of all eighth grade classes
        public Stack<SchoolClass> GetEighthGradeClasses()
        {
            ICollection<SchoolClass> eighthGradeClasses = _dbContext.SchoolClasses
                .Where(c => c.YearOfStudy == 8)
                .OrderBy(c => c.ClassLetter)
                .ToList();

            return new Stack<SchoolClass>(eighthGradeClasses);
        }

        //get the subjects for one class depending on its year
        public List<SchoolSubject> GetClassSubjects(int classYear)
        {
            switch(classYear)
            {
                case 5: return _dbContext.SchoolSubjects
                                .Where(s => s.YearOfStudy == 5)
                                .Select(s => s)
                                .ToList();
                case 6: return _dbContext.SchoolSubjects
                                .Where(s => s.YearOfStudy == 5 || s.YearOfStudy == 6)
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToList();
                case 7: return _dbContext.SchoolSubjects
                                .Where(s => s.YearOfStudy == 5 || s.YearOfStudy == 6 || s.YearOfStudy == 7)
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToList();
                case 8: return _dbContext.SchoolSubjects
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToList();
                default: return new List<SchoolSubject>();
            }
        }

        //get the classes of one year, depending on the year of study as input
        public Stack<SchoolClass> GetClassesofOneYear(int yearOfStudy)
        {
            switch (yearOfStudy)
            {
                case 5:
                    return GetFifthGradeClasses();
                case 6:
                    return GetSixthGradeClasses();
                case 7:
                    return GetSeventhGradeClasses();
                case 8:
                    return GetEighthGradeClasses();
                default:
                    return new Stack<SchoolClass>();
            }
        }

        //get the last letter that exists in a year
        public char GetLastLetter(int yearOfStudy)
        {
            Stack<SchoolClass> schoolClasses = GetClassesofOneYear(yearOfStudy);
            if (schoolClasses.Count == 0)
            {
                return '/';
            }
            else
            {
                return schoolClasses.Peek().ClassLetter;
            }
		}

        //get the next available letter for a new class
        public char GetAvailableLetter(int yearOfStudy)
        {
            string letters = "ABCDEFGHIJKLMNOPRSTUVXYZ";

            char lastletter = GetLastLetter(yearOfStudy);
            if (lastletter == '/')
            {
                return letters[0];
            }
            else
            {
                int newIndex = letters.IndexOf(lastletter) + 1;
                return letters[newIndex];
            }
        }

        //get a class object from view model object
        public SchoolClass GetClassFromViewModel(SchoolClassViewModel viewModel)
        {
            return _dbContext.SchoolClasses
                .Where(c => c.YearOfStudy == viewModel.YearOfStudy)
                .OrderBy(c => c.ClassLetter)
                .Last();
        }

        //add new class to database
        public async void AddClass(int yearOfStudy)
        {
			SchoolClass newClass = new SchoolClass
            {
                YearOfStudy = yearOfStudy,
                ClassLetter = GetAvailableLetter(yearOfStudy)
			};

            await _dbContext.SchoolClasses.AddAsync(newClass);
			Save();
		}

        //delete a class from the database
        public void DeleteClass(int yearOfStudy)
        {
            char lastLetter = GetLastLetter(yearOfStudy);

            if (lastLetter != '/') 
            {
                SchoolClass existingClass = _dbContext.SchoolClasses
                    .First(c => c.YearOfStudy == yearOfStudy && c.ClassLetter == lastLetter);

                _dbContext.SchoolClasses.Remove(existingClass);
                Save();
            }
		}

		//save changes to database
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
