using Microsoft.EntityFrameworkCore;
using School_Timetable.Models.Entities;
using System.Text;

namespace School_Timetable.Interfaces
{
    public interface ISubjectRepository
    {
		//get list of all subjects
		Task<ICollection<SchoolSubject>> GetSchoolSubjects();

		//get one subject by id
		Task<SchoolSubject> GetSchoolSubject(int subjectId);

		//get a list of professors of one subject
		List<Professor> GetProfessorsOfASubject(int subjectId);
    }
}
