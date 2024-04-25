using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.ViewModels;
using System.Text;

namespace School_Timetable.Interfaces
{
    public interface ISubjectRepository
    {
		//get list of all subjects
		ICollection<SchoolSubject> GetSchoolSubjects();

		//get one subject by id
		SchoolSubject GetSchoolSubject(int subjectId);

		//get a list of professors of one subject
		List<Professor> GetProfessorsOfASubject(int subjectId);

		//get the subjects for one class depending on its year
		List<SchoolSubject> GetClassSubjects(int yearOfStudy);

		//check if year of study was selected by user or not (convert bool to char)
		char CheckSelectedYear(bool yearOfStudy);

		//adding a new subject to database
		void AddSubject(CreateSchoolSubjectViewModel viewModel);

		//delete a subject from database
		void DeleteSchoolSubject(SchoolSubject viewModel);

		//save changes to database
		void Save();
	}
}
