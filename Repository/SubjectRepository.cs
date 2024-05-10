using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;
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
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return await _dbContext.SchoolSubjects
                .Where(s => s.AppUserId == currentUser.ToString())
                .OrderBy(s => s.Id)
                .ToListAsync();
        }

        //get one subject by id
        public async Task<SchoolSubject> GetSchoolSubject(int subjectId)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return await _dbContext.SchoolSubjects
                .Where(s => s.AppUserId == currentUser.ToString() && s.Id == subjectId)
                .FirstAsync();
        }

        //check if there are any subjects in database
        public async Task<bool> CheckExistingSubjects()
        {
            ICollection<SchoolSubject> schoolSubjects = await GetSchoolSubjects();
            if (schoolSubjects.Count == 0)
            {
                return false;
            }
            return true;
        }

        //get a list of professors for one subject
        public async Task<List<Professor>> GetProfessorsOfASubject(int subjectId)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return await _dbContext.Professors
                .Where(p => p.AppUserId == currentUser.ToString() && p.SchoolSubjectId == subjectId)
                .ToListAsync();
        }

        //get the subjects for one class depending on its year
        public async Task<List<SchoolSubject>> GetClassSubjects(int yearOfStudy)
        {
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            switch (yearOfStudy)
            {
                case 5:
                    return await _dbContext.SchoolSubjects
                                .Where(s => s.AppUserId == currentUserId.ToString() && s.FifthYearOfStudy == 'Y')
                                .Select(s => s)
                                .ToListAsync();
                case 6:
                    return await _dbContext.SchoolSubjects
                                .Where(s => s.AppUserId == currentUserId.ToString() && s.SixthYearOfStudy == 'Y')
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToListAsync();
                case 7:
                    return await _dbContext.SchoolSubjects
                                .Where(s => s.AppUserId == currentUserId.ToString() && s.SeventhYearOfStudy == 'Y')
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToListAsync();
                case 8:
                    return await _dbContext.SchoolSubjects
                                .Where(s => s.AppUserId == currentUserId.ToString() && s.EighthYearOfStudy == 'Y')
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToListAsync();
                default: return new List<SchoolSubject>();
            }
        }

		//check if year of study was selected by user or not (convert bool to char)
		public char CheckSelectedYear(bool yearOfStudy)
		{
			if (yearOfStudy == true)
			{
				return 'Y';
			}
			return 'N';
		}

		//adding a new subject to database
		public async Task AddSubject(CreateSchoolSubjectViewModel viewModel)
        {
            SchoolSubject subject = new SchoolSubject
            {
                Name = viewModel.Name,
                HoursPerWeek = viewModel.HoursPerWeek,
				FifthYearOfStudy = CheckSelectedYear(viewModel.FifthYearOfStudy),
				SixthYearOfStudy = CheckSelectedYear(viewModel.SixthYearOfStudy),
				SeventhYearOfStudy = CheckSelectedYear(viewModel.SeventhYearOfStudy),
				EighthYearOfStudy = CheckSelectedYear(viewModel.EighthYearOfStudy),
                AppUserId = viewModel.AppUserId
			};

            await _dbContext.SchoolSubjects.AddAsync(subject);
            Save();
        }

        //delete a subject from database
        public async Task DeleteSchoolSubject(SchoolSubject viewModel)
        {
			SchoolSubject subject = await GetSchoolSubject(viewModel.Id);

			_dbContext.SchoolSubjects.Remove(subject);
			Save();
		}

		//save changes to database
		public bool Save()
		{
			int saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
		}
	}
}
