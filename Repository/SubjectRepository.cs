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
        public SchoolSubject GetSchoolSubject(int subjectId)
        {
            return _dbContext.SchoolSubjects
                .Where(s => s.Id == subjectId)
                .First();
        }

        //get a list of professors for one subject
        public List<Professor> GetProfessorsOfASubject(int subjectId)
        {
            return _dbContext.Professors
                .Where(p => p.SchoolSubjectId == subjectId)
                .ToList();
        }
    }
}
