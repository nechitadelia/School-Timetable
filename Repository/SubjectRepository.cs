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
        public ICollection<SchoolSubject> GetSchoolSubjects()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return _dbContext.SchoolSubjects
                .Where(s => s.AppUserId == currentUser.ToString())
                .OrderBy(s => s.Id)
                .ToList();
        }

        //get one subject by id
        public SchoolSubject GetSchoolSubject(int subjectId)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return _dbContext.SchoolSubjects
                .Where(s => s.AppUserId == currentUser.ToString() && s.Id == subjectId)
                .First();
        }

        //get a list of professors for one subject
        public List<Professor> GetProfessorsOfASubject(int subjectId)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return _dbContext.Professors
                .Where(p => p.AppUserId == currentUser.ToString() && p.SchoolSubjectId == subjectId)
                .ToList();
        }

        //get the subjects for one class depending on its year
        public List<SchoolSubject> GetClassSubjects(int yearOfStudy)
        {
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            switch (yearOfStudy)
            {
                case 5:
                    return _dbContext.SchoolSubjects
                                .Where(s => s.AppUserId == currentUserId.ToString() && s.FifthYearOfStudy == 'Y')
                                .Select(s => s)
                                .ToList();
                case 6:
                    return _dbContext.SchoolSubjects
                                .Where(s => s.AppUserId == currentUserId.ToString() && s.SixthYearOfStudy == 'Y')
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToList();
                case 7:
                    return _dbContext.SchoolSubjects
                                .Where(s => s.AppUserId == currentUserId.ToString() && s.SeventhYearOfStudy == 'Y')
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToList();
                case 8:
                    return _dbContext.SchoolSubjects
                                .Where(s => s.AppUserId == currentUserId.ToString() && s.EighthYearOfStudy == 'Y')
                                .OrderBy(s => s.Id)
                                .Select(s => s)
                                .ToList();
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
		public async void AddSubject(CreateSchoolSubjectViewModel viewModel)
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
        public void DeleteSchoolSubject(SchoolSubject viewModel)
        {
			SchoolSubject subject = GetSchoolSubject(viewModel.Id);

			_dbContext.SchoolSubjects.Remove(subject);
			Save();
		}

		//save changes to database
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
