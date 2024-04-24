using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Utilities;
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
        public ICollection<SchoolSubject> GetSchoolSubjects()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return _dbContext.SchoolSubjects
                .Where(s => s.AppUserId == currentUser.ToString())
                .OrderBy(s => s.Id)
                .ToList();
        }

        //get one subject by id
        public async Task<SchoolSubject> GetSchoolSubject(int subjectId)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return await _dbContext.SchoolSubjects
                .Where(s => s.AppUserId == currentUser.ToString() && s.Id == subjectId)
                .FirstAsync();
        }

        //get a list of professors for one subject
        public List<Professor> GetProfessorsOfASubject(int subjectId)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return _dbContext.Professors
                .Where(p => p.AppUserId == currentUser.ToString() && p.SchoolSubjectId == subjectId)
                .ToList();
        }
    }
}
