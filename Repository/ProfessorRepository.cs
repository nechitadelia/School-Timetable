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
        public ICollection<Professor> GetProfessors()
        {
            string? currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            return _dbContext.Professors
                .Where(p => p.AppUserId == currentUser.ToString())
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();        
        }

        //get one professor by id
        public Professor GetProfessor(int professorId)
        {
            return _dbContext.Professors
                .Where(p => p.Id == professorId)
                .Include(p => p.ProfessorSubject)
                .First();
        }

        //get a professor's subject by his/her id
        public SchoolSubject GetSubjectOfProfessor(int professorId)
        {
            Professor professor = GetProfessor(professorId);
            return professor.ProfessorSubject;
        }

		//check if you can assign hours to a professor
		public bool CanAssignHours(int professorId)
		{
            Professor professor = GetProfessor(professorId);
			//SchoolSubject subject = await GetSubjectOfProfessor(professorId);

			if ((professor.AssignedHours + professor.ProfessorSubject.HoursPerWeek) <= 20)
			{
                return true;
			}
            else
            {
                return false;
            }
		}

        //check if a professor was already assigned to a class
        public bool CanAssignClass(SchoolClass schoolClass, SchoolSubject schoolSubject)
        {
            //check if there is already a professor for the same class and subject
            bool result = _dbContext.ClassProfessors.Any(c => c.SchoolClassId == schoolClass.Id && c.SubjectName == schoolSubject.Name);
            
            if (result)
            {
                return false;
            }
            return true;
        }

		//assign hours to a professor
		public void AssignHours(int professorId)
        {
            Professor professor = GetProfessor(professorId);
			SchoolSubject subject = GetSubjectOfProfessor(professorId);

			if (CanAssignHours(professorId))
            {
				professor.AssignedHours += subject.HoursPerWeek;
                Save();
			}
        }

        //get a professor's unassigned hours
        public int GetUnassignedHours(int professorId)
        {
            Professor professor = GetProfessor(professorId);
            int unassignedHours = 20 - professor.AssignedHours;

            return unassignedHours;
        }

		//unassign all hours from a professor
		public void UnassignAllHoursFromProfessor(Professor professor)
		{
            professor.AssignedHours = 0;
            Save();
		}

		//unassign all hours from all professors
		public void UnassignAllHoursFromEveryone()
        {
            ICollection<Professor> professors = GetProfessors();

            foreach (Professor p in professors)
            {
				UnassignAllHoursFromProfessor(p);
            }
		}

        //unassign hours from a professor (when a class is deleted)
        public void UnassignHoursFromProfessor(Professor professor)
        {
            //int subjectHours = GetSubjectOfProfessor(professor.Id).HoursPerWeek;
			professor.AssignedHours -= professor.ProfessorSubject.HoursPerWeek;
		}

		//create a new professor
		public async void AddProfessor(CreateProfessorViewModel viewModel)
        {
            SchoolSubject subject = _dbContext.SchoolSubjects.Where(s => s.Id == viewModel.SchoolSubjectId).First();

            Professor professor = new Professor
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                AssignedHours = 0,
                ProfessorSubject = subject,
                SchoolSubjectId = viewModel.SchoolSubjectId,
                AppUserId = viewModel.AppUserId
            };

            await _dbContext.Professors.AddAsync(professor);
            Save();
		}

		//edit a professors's data
		public void EditProfessor(EditProfessorViewModel viewModel)
		{
            Professor professor = GetProfessor(viewModel.Id);

            if (professor != null)
			{
				professor.FirstName = viewModel.FirstName;
				professor.LastName = viewModel.LastName;

				Save();
			}
		}

		//delete a professor from the database
		public void DeleteProfessor(Professor viewModel)
        {
			Professor professor = GetProfessor(viewModel.Id);

			_dbContext.Professors.Remove(professor);
			Save();
		}

		//save changes to database
		public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
