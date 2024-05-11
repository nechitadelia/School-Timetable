using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.ViewModels;
using System.Text;

namespace School_Timetable.Interfaces
{
    public interface ISubjectRepository
    {
		//get list of all subjects
		Task<ICollection<SchoolSubject>> GetSchoolSubjects();

		//get one subject by id
		Task<SchoolSubject> GetSchoolSubject(int subjectId);

		//check if there are any subjects in database
		Task<bool> CheckExistingSubjects();

        //get a list of professors of one subject
        Task<List<Professor>> GetProfessorsOfASubject(int subjectId);

		//get the subjects for one class depending on its year
		Task<List<SchoolSubject>> GetClassSubjects(int yearOfStudy);

		//check if year of study was selected by user or not (convert bool to char)
		char CheckSelectedYear(bool yearOfStudy);

        //adding a new subject to database
        Task<bool> AddSubject(CreateSchoolSubjectViewModel viewModel);

        //delete a subject from database
        Task<bool> DeleteSchoolSubject(SchoolSubject viewModel);

		//save changes to database
		bool Save();
	}
}
