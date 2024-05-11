using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.ViewModels;

namespace School_Timetable.Interfaces
{
    public interface ISchoolClassRepository
    {
		//get the last class from one year of study
		Task<SchoolClass> GetLastClassFromOneYear(int yearOfStudy);

        //get list of all classes, in order
        Task<ICollection<SchoolClass>> GetAllClasses();

        //get the classes of one year, depending on the year of study as input
        Task<Stack<SchoolClass>> GetClassesofOneYear(int yearOfStudy);

        //get the last letter that exists in a year
        Task<char> GetLastLetter(int yearOfStudy);

        //get the next available letter for a new class
        Task<char> GetAvailableLetter(int yearOfStudy);

        //graduate all classes - change classes to the next school year
        Task<bool> GraduateClasses();

        //add new class to database
        Task<bool> AddClass(CreateSchoolClassViewModel viewModel);

        //delete a class from the database
        Task<bool> DeleteClass(SchoolClass schoolClass);

        //save changes to database
        bool Save();
    }
}
