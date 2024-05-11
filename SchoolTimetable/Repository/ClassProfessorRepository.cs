using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Utilities;

namespace School_Timetable.Repository
{
    public class ClassProfessorRepository : IClassProfessorRepository
	{
		private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClassProfessorRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
		{
			_dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

		//assign one professor to one class
		public async Task<bool> AddProfessorToAClass(SchoolClass schoolClass, Professor professor)
		{
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            //searching the name of the subject based on the id
            SchoolSubject subject = await _dbContext.SchoolSubjects
				.Where(s => s.Id == professor.SchoolSubjectId)
				.FirstAsync();
			string subjectName = subject.Name;

			//creating a new connection between a class and a professor
			ClassProfessor classProfessor = new ClassProfessor
			{
				SchoolClassId = schoolClass.Id,
				SubjectName = subjectName,
				ProfessorId = professor.Id,
				AppUserId = currentUserId
			};

			await _dbContext.ClassProfessors.AddAsync(classProfessor);
			return Save();
		}

		//check if a connection exists in the ClassProfessor table
		public async Task<bool> ConnectionExists(SchoolClass schoolClass, SchoolSubject schoolSubject)
		{
			return await _dbContext.ClassProfessors
				.AnyAsync(s => s.SchoolClassId == schoolClass.Id && s.SubjectName == schoolSubject.Name);
		}

        public async Task<bool> ConnectionExists(Professor professor)
        {
            return await _dbContext.ClassProfessors
                .AnyAsync(p => p.ProfessorId == professor.Id);
        }

        //get the professor of one subject of a class
        public async Task<Professor> GetProfessorOfASubjectOfOneClass(SchoolClass schoolClass, SchoolSubject schoolSubject)
		{
			if (await ConnectionExists(schoolClass, schoolSubject))
			{
				ClassProfessor professor = await _dbContext.ClassProfessors
					.Where(s => s.SchoolClassId == schoolClass.Id && s.SubjectName == schoolSubject.Name)
					.FirstAsync();
				int professorId = professor.ProfessorId;

				return await _dbContext.Professors.Where(p => p.Id == professorId).FirstAsync();
			}
			else
			{
				return null;
			}
		}

        //get all the class ids for one professor
        public async Task<ICollection<int>> GetClassIds(Professor professor)
        {
            if (await ConnectionExists(professor))
            {
                //saving all the ids of the classes that a professor has
                return await _dbContext.ClassProfessors
				.Where(p => p.ProfessorId == professor.Id)
				.Select(s => s.SchoolClassId)
				.ToListAsync();
			}
            else
            {
                return [];
            }
        }

        //get the list of classes for one professor
        public async Task<List<SchoolClass>> GetClassesOfAProfessor(Professor professor)
		{
            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            ICollection<int> schoolClassesIds = await GetClassIds(professor);

            //creating the list of School Classes based in the collection of ids
			if (schoolClassesIds != null)
			{
                foreach (int id in schoolClassesIds)
                {
                    SchoolClass s = await _dbContext.SchoolClasses.Where(c => c.Id == id).FirstAsync();
                    schoolClasses.Add(s);
                }

                //ordering the school classes
                schoolClasses = schoolClasses.OrderBy(s => s.YearOfStudy).ThenBy(s => s.ClassLetter).ToList();
            }

            return schoolClasses;
        }

        //delete a collection of ClassProfessor from database
        public bool DeleteClassProfessor(ICollection<ClassProfessor> classProfessors)
		{
			foreach (ClassProfessor cp in classProfessors)
            {
                _dbContext.ClassProfessors.Remove(cp);
            }
			return Save();
        }

		//unassign a professor from all classes
		public async Task<bool> UnassignAProfessorFromAllClasses(Professor professor)
		{
			ICollection<ClassProfessor> cp = await _dbContext.ClassProfessors.Where(p => p.ProfessorId == professor.Id).ToListAsync();
			return DeleteClassProfessor(cp);
		}

		//unassign all professors from all classes
		public async Task<bool> UnassignAllProfessorsFromAllClasses()
		{
            string? currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            ICollection<ClassProfessor> cp = await _dbContext.ClassProfessors
				.Where(cp => cp.AppUserId == currentUserId)
				.ToListAsync();
			return DeleteClassProfessor(cp);
        }

		//delete a class from ClassProfessor table
		public async Task<bool> UnassignAClass(SchoolClass schoolClass)
		{
			ICollection<ClassProfessor> cp = await _dbContext.ClassProfessors.Where(c => c.SchoolClassId == schoolClass.Id).ToListAsync();
			return DeleteClassProfessor(cp);
		}

		//save changes to database
		public bool Save()
		{
			int saved = _dbContext.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
