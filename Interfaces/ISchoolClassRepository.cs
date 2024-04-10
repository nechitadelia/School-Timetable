using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.Models.Entities;

namespace School_Timetable.Interfaces
{
    public interface ISchoolClassRepository
    {
        //get list of all classes, in order
        ICollection<SchoolClass> GetAllClasses();

        //get list of all fifth grade classes
        Stack<SchoolClass> GetFifthGradeClasses();

        //get list of all sixth grade classes
        Stack<SchoolClass> GetSixthGradeClasses();

        //get list of all seventh grade classes
        Stack<SchoolClass> GetSeventhGradeClasses();

        //get list of all eighth grade classes
        Stack<SchoolClass> GetEighthGradeClasses();

        //get class subjects depending on its year
        List<SchoolSubject> GetClassSubjects(int classYear);

        //get the classes of one year, depending on the year of study as input
        Stack<SchoolClass> GetClassesofOneYear(int yearOfStudy);

        //get the last letter that exists in a year
        char GetLastLetter(int yearOfStudy);

		//get the next available letter for a new class
		char GetAvailableLetter(int yearOfStudy);

        //get a class object from view model object
        SchoolClass GetClassFromViewModel(SchoolClassViewModel viewModel);

		//add new class to database
		void AddClass(int yearOfStudy);

        //delete a class from the database
        void DeleteClass(int yearOfStudy);

        //save changes to database
        Task<IActionResult> Save();
	}
}
