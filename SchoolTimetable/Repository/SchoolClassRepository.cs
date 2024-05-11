using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;

namespace School_Timetable.Repository
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SchoolClassRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        //get the last class from one year of study
        public async Task<SchoolClass> GetLastClassFromOneYear(int yearOfStudy)
        {
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            return await _dbContext.SchoolClasses
                .Where(c => c.AppUserId == currentUserId.ToString() && c.YearOfStudy == yearOfStudy)
                .OrderBy(c => c.ClassLetter)
                .LastAsync();
        }

        //get list of all classes, in order
        public async Task<ICollection<SchoolClass>> GetAllClasses()
        {
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            return await _dbContext.SchoolClasses
                .Where(c => c.AppUserId == currentUserId.ToString())
                .OrderBy(c => c.YearOfStudy)
                .ThenBy(c => c.ClassLetter)
                .ToListAsync();
        }

        //get the classes of one year, depending on the year of study as input
        public async Task<Stack<SchoolClass>> GetClassesofOneYear(int yearOfStudy)
        {
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            ICollection<SchoolClass> schoolClasses = await _dbContext.SchoolClasses
                .Where(c => c.AppUserId == currentUserId.ToString() && c.YearOfStudy == yearOfStudy)
                .OrderBy(c => c.ClassLetter)
                .ToListAsync();
            
            return new Stack<SchoolClass>(schoolClasses);
        }

        //get the last letter that exists in a year
        public async Task<char> GetLastLetter(int yearOfStudy)
        {
            Stack<SchoolClass> schoolClasses = await GetClassesofOneYear(yearOfStudy);
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
        public async Task<char> GetAvailableLetter(int yearOfStudy)
        {
            string letters = "ABCDEFGHIJKLMNOPRSTUVXYZ";

            char lastletter = await GetLastLetter(yearOfStudy);
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

        //graduate all classes - change classes to the next school year
        public async Task<bool> GraduateClasses()
        {
            //get all the eighth grade classes and delete them
            Stack<SchoolClass> eighthGradeClasses = await GetClassesofOneYear(8);
            if (eighthGradeClasses.Count > 0)
            {
                foreach (SchoolClass schoolClass in eighthGradeClasses)
                {
                    await DeleteClass(schoolClass);
                }
            }

            //get the remaining classes (5-7) and increment the year of study
            ICollection<SchoolClass> allClasses = await GetAllClasses();
            if (allClasses.Count > 0)
            {
                for (int i = allClasses.Count - 1; i >= 0; i--)
                {
                    allClasses.ElementAt(i).YearOfStudy += 1;
                    bool result = Save();

                    if (result == false) { return false; }
                }
            }

            return true;
        }

        //add new class to database
        public async Task<bool> AddClass(CreateSchoolClassViewModel viewModel)
        {
			SchoolClass newClass = new SchoolClass
            {
                YearOfStudy = viewModel.YearOfStudy,
                ClassLetter = await GetAvailableLetter(viewModel.YearOfStudy),
                AppUserId = viewModel.AppUserId
            };

            _dbContext.SchoolClasses.Add(newClass);
            return Save();
        }

        //delete a class from the database
        public async Task<bool> DeleteClass(SchoolClass schoolClass)
        {
            char lastLetter = await GetLastLetter(schoolClass.YearOfStudy);

            if (lastLetter != '/') 
            {
                SchoolClass existingClass = await _dbContext.SchoolClasses
                    .FirstAsync(c => c.YearOfStudy == schoolClass.YearOfStudy && c.ClassLetter == lastLetter);

                _dbContext.SchoolClasses.Remove(existingClass);
                bool result = Save();

                if (result == false) { return false; }
            }

            return true;
        }

		//save changes to database
		public bool Save()
		{
			int saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
		}
	}
}
