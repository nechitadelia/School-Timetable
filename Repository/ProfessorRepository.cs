using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;
using System.Linq;

namespace School_Timetable.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfessorRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        //get list of all professors, in ascending order
        public async Task<ICollection<Professor>> GetProfessors()
        {
            string? currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return await _dbContext.Professors
                .Where(p => p.AppUserId == currentUser.ToString())
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();        
        }

        //get one professor by id
        public async Task<Professor> GetProfessor(int professorId)
        {
            return await _dbContext.Professors
                .Where(p => p.Id == professorId)
                .Include(p => p.ProfessorSubject)
                .FirstAsync();
        }

        //get a professor's subject by his/her id
        public async Task<SchoolSubject> GetSubjectOfProfessor(int professorId)
        {
            Professor professor = await GetProfessor(professorId);
            return professor.ProfessorSubject;
        }

		//check if you can assign hours to a professor
		public async Task<bool> CanAssignHours(int professorId)
		{
            Professor professor = await GetProfessor(professorId);

			if ((professor.AssignedHours + professor.ProfessorSubject.HoursPerWeek) <= professor.MaxHours)
			{
                return true;
			}
            else
            {
                return false;
            }
		}

        //check if a professor was already assigned to a class
        public async Task<bool> CanAssignClass(SchoolClass schoolClass, SchoolSubject schoolSubject)
        {
            //check if there is already a professor for the same class and subject
            bool result = await _dbContext.ClassProfessors.AnyAsync(c => c.SchoolClassId == schoolClass.Id && c.SubjectName == schoolSubject.Name);
            
            if (result)
            {
                return false;
            }
            return true;
        }

		//assign hours to a professor
		public async Task AssignHours(int professorId)
        {
            Professor professor = await GetProfessor(professorId);
			SchoolSubject subject = await GetSubjectOfProfessor(professorId);

			if (await CanAssignHours(professorId))
            {
				professor.AssignedHours += subject.HoursPerWeek;
                Save();
			}
        }

        //get a professor's unassigned hours
        public async Task<int> GetUnassignedHours(int professorId)
        {
            Professor professor = await GetProfessor(professorId);
            int unassignedHours = professor.MaxHours - professor.AssignedHours;

            return unassignedHours;
        }

		//unassign all hours from a professor
		public void UnassignAllHoursFromProfessor(Professor professor)
		{
            professor.AssignedHours = 0;
            Save();
		}

		//unassign all hours from all professors
		public async Task UnassignAllHoursFromEveryone()
        {
            ICollection<Professor> professors = await GetProfessors();

            foreach (Professor p in professors)
            {
				UnassignAllHoursFromProfessor(p);
            }
		}

        //unassign hours from a professor (when a class is deleted)
        public void UnassignHoursFromProfessor(Professor professor)
        {
			professor.AssignedHours -= professor.ProfessorSubject.HoursPerWeek;
		}

		//create a new professor
		public async Task AddProfessor(CreateProfessorViewModel viewModel)
        {
            SchoolSubject subject = await _dbContext.SchoolSubjects.Where(s => s.Id == viewModel.SchoolSubjectId).FirstAsync();

            Professor professor = new Professor
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                AssignedHours = 0,
                MaxHours = viewModel.MaxHours,
                ProfessorSubject = subject,
                SchoolSubjectId = viewModel.SchoolSubjectId,
                AppUserId = viewModel.AppUserId
            };

            await _dbContext.Professors.AddAsync(professor);
            Save();
		}

		//edit a professors's data
		public async Task EditProfessor(EditProfessorViewModel viewModel)
		{
            Professor professor = await GetProfessor(viewModel.Id);

            if (professor != null)
			{
				professor.FirstName = viewModel.FirstName;
				professor.LastName = viewModel.LastName;

				Save();
			}
		}

		//delete a professor from the database
		public async Task DeleteProfessor(Professor viewModel)
        {
			Professor professor = await GetProfessor(viewModel.Id);

			_dbContext.Professors.Remove(professor);
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
