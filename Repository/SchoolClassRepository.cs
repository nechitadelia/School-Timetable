﻿using Microsoft.AspNetCore.Mvc;
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
        public SchoolClass GetLastClassFromOneYear(int yearOfStudy)
        {
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            return _dbContext.SchoolClasses
                .Where(c => c.AppUserId == currentUserId.ToString() && c.YearOfStudy == yearOfStudy)
                .OrderBy(c => c.ClassLetter)
                .Last();
        }

        //get list of all classes, in order
        public ICollection<SchoolClass> GetAllClasses()
        {
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            return _dbContext.SchoolClasses
                .Where(c => c.AppUserId == currentUserId.ToString())
                .OrderBy(c => c.YearOfStudy)
                .ThenBy(c => c.ClassLetter)
                .ToList();
        }

        //get the classes of one year, depending on the year of study as input
        public Stack<SchoolClass> GetClassesofOneYear(int yearOfStudy)
        {
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            ICollection<SchoolClass> schoolClasses = _dbContext.SchoolClasses
                .Where(c => c.AppUserId == currentUserId.ToString() && c.YearOfStudy == yearOfStudy)
                .OrderBy(c => c.ClassLetter)
                .ToList();
            
            return new Stack<SchoolClass>(schoolClasses);
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

        //graduate all classes - change classes to the next school year
        public void GraduateClasses()
        {
            //get all the eighth grade classes and delete them
            Stack<SchoolClass> eighthGradeClasses = GetClassesofOneYear(8);
            if (eighthGradeClasses.Count > 0)
            {
                foreach (SchoolClass schoolClass in eighthGradeClasses)
                {
                    DeleteClass(schoolClass);
                }
            }

            //get the remaining classes (5-7) and increment the year of study
            ICollection<SchoolClass> allClasses = GetAllClasses();
            if (allClasses.Count > 0)
            {
                for (int i = allClasses.Count - 1; i >= 0; i--)
                {
                    allClasses.ElementAt(i).YearOfStudy += 1;
                    Save();
                }
            }
        }

        //add new class to database
        public void AddClass(CreateSchoolClassViewModel viewModel)
        {
			SchoolClass newClass = new SchoolClass
            {
                YearOfStudy = viewModel.YearOfStudy,
                ClassLetter = GetAvailableLetter(viewModel.YearOfStudy),
                AppUserId = viewModel.AppUserId
            };

            _dbContext.SchoolClasses.Add(newClass);
			Save();
		}

        //delete a class from the database
        public void DeleteClass(SchoolClass schoolClass)
        {
            char lastLetter = GetLastLetter(schoolClass.YearOfStudy);

            if (lastLetter != '/') 
            {
                SchoolClass existingClass = _dbContext.SchoolClasses
                    .First(c => c.YearOfStudy == schoolClass.YearOfStudy && c.ClassLetter == lastLetter);

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
