using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.Models.Entities;

namespace School_Timetable.Interfaces
{
    public interface ISchoolClassRepository
    {
        //get the last class from one year of study
        SchoolClass GetLastClassFromOneYear(int yearOfStudy);

        //get list of all classes, in order
        ICollection<SchoolClass> GetAllClasses();

        //get the classes of one year, depending on the year of study as input
        Stack<SchoolClass> GetClassesofOneYear(int yearOfStudy);

        //get the subjects for one class depending on its year
        List<SchoolSubject> GetClassSubjects(int yearOfStudy);

        //get the last letter that exists in a year
        char GetLastLetter(int yearOfStudy);

        //get the next available letter for a new class
        char GetAvailableLetter(int yearOfStudy);

        //graduate all classes - change classes to the next school year
        void GraduateClasses();

        //add new class to database
        void AddClass(int yearOfStudy);

        //delete a class from the database
        void DeleteClass(SchoolClass schoolClass);

        //save changes to database
        void Save();
    }
}
