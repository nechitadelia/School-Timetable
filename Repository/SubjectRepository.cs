using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using System.Text;

namespace School_Timetable.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _dbContext;

        public SubjectRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get list of all subjects
        public ICollection<SchoolSubject> GetSchoolSubjects()
        {
            return _dbContext.SchoolSubjects
                .OrderBy(s => s.Id)
                .ToList();
        }

        //get one subject by id
        public SchoolSubject GetSchoolSubject(int id)
        {
            return _dbContext.SchoolSubjects
                .Where(s => s.Id == id)
                .First();
        }

        //get one subject by name
        public SchoolSubject GetSchoolSubject(string name)
        {
            return _dbContext.SchoolSubjects
                .Where(s => s.Name == name)
                .First();
        }

        //get a list of professors of one subject
        public List<Professor> GetProfessorsOfASubject(int subjectId)
        {
            return _dbContext.Professors
                .Where(p => p.SchoolSubjectId == subjectId)
                .OrderBy(p => p.Id)
                .ToList();
        }

        //turn the list of professors for a subject into a string
        public string ProfessorsListToString(int subjectId)
        {
            List<Professor> professors = GetProfessorsOfASubject(subjectId);

            StringBuilder text = new StringBuilder();

            foreach (Professor p in professors)
            {
                text.Append($" {p.FirstName} {p.LastName},");
            }

            return text.ToString();
        }
    }
}
