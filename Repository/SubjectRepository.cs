using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using System.Collections.Generic;
using System.Text;

namespace School_Timetable.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubjectRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        //get list of all subjects
        public async Task<ICollection<SchoolSubject>> GetSchoolSubjects()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;

            ICollection<SchoolSubject> schoolSubjects = await _dbContext.SchoolSubjects
                .Where(s => s.AppUserId == currentUser.ToString())
                .OrderBy(s => s.Id)
                .ToListAsync();

            return schoolSubjects;
        }

        //get one subject by id
        public async Task<SchoolSubject> GetSchoolSubject(int subjectId)
        {
            return await _dbContext.SchoolSubjects
                .Where(s => s.Id == subjectId)
                .FirstAsync();
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
