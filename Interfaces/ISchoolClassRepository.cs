using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.Models.Entities;

namespace School_Timetable.Interfaces
{
    public interface ISchoolClassRepository
    {
        //get list of all classes, in order
        public ICollection<SchoolClass> GetAllClasses();

        //get list of all fifth grade classes
        public Stack<SchoolClass> GetFifthGradeClasses();

        //get list of all sixth grade classes
        public Stack<SchoolClass> GetSixthGradeClasses();

        //get list of all seventh grade classes
        public Stack<SchoolClass> GetSeventhGradeClasses();

        //get list of all eighth grade classes
        public Stack<SchoolClass> GetEighthGradeClasses();

        //get class subjects depending on its year
        public List<SchoolSubject> GetClassSubjects(int classYear);

        //get the classes of one year, depending on the year of study as input
        public Stack<SchoolClass> GetClassesofOneYear(int yearOfStudy);

        //get the last letter that exists in a year
        public char GetLastLetter(int yearOfStudy);

		//get the next available letter for a new class
		public char GetAvailableLetter(int yearOfStudy);

        //get a class object from view model object
        public SchoolClass GetClassFromViewModel(SchoolClassViewModel viewModel);

		//add new class to database
		public void AddClass(int yearOfStudy);

        //delete a class from the database
        public void DeleteClass(int yearOfStudy);

        //save changes to database
        public void Save();

        //assign professors to one class
        //public void AssignProfessorsToAClass(SchoolClass schoolClass);
	}
}
