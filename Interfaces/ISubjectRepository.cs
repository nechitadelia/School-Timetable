using Microsoft.EntityFrameworkCore;
using School_Timetable.Models.Entities;
using System.Text;

namespace School_Timetable.Interfaces
{
    public interface ISubjectRepository
    {
        //get list of all subjects
        public ICollection<SchoolSubject> GetSchoolSubjects();

        //get one subject by id
        public SchoolSubject GetSchoolSubject(int id);

        //get one subject by name
        public SchoolSubject GetSchoolSubject(string name);

        //get a list of professors of one subject
        public List<Professor> GetProfessorsOfASubject(int subjectId);

        //turn the list of professors for a subject into a string
        public string ProfessorsListToString(int subjectId);
    }
}
